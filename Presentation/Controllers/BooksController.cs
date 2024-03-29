﻿using AutoMapper;
using Entities.DataTranferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    //[ApiVersion("1.0")]
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    //[ResponseCache(CacheProfileName ="5mins")]
    //[HttpCacheExpiration(CacheLocation =CacheLocation.Public,MaxAge =80)] // özelleştirme yapıyoruz.
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public BooksController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }


        [Authorize]
        [HttpHead]
        [HttpGet(Name = "GetAllBooksAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        //[ResponseCache(Duration =60)]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters) // Querystring üzerinden gelecek.
        {
            var linkParameters = new LinkParameters()
            {
                BookParameters = bookParameters,
                HttpContext = HttpContext
            };
            var result = await _manager
                .BookService
                .GetAllBooksAsync(linkParameters, false);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) : // link üretebildiysek link aksi taktirde şekillendirilmiş data döndüreceğiz.
                Ok(result.linkResponse.ShapedEntities);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {

            var book = await _manager
           .BookService
           .GetOneBookAsync(id, false);

            return Ok(book);
        }

        [Authorize]
        [HttpGet("details")]
        public async Task<IActionResult> GetAllBooksWithDetailsAsync()
        {
            return Ok(await _manager.BookService.GetAllBooksWithDetailsAsync(false));
        }


        [Authorize(Roles = "Editor, Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name = "CreateOneBookAsync")]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
        {

            var book = await _manager.BookService.CreateOneBookAsync(bookDto);

            return StatusCode(201, book); // CreatedAtRoute() responseun headerına bir location koyabiliyoruz.

        }


        [Authorize(Roles = "Editor,Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDtobook)
        {

            await _manager.BookService.UpdateOneBookAsync(id, bookDtobook, false);
            return NoContent();

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {

            await _manager.BookService.DeleteOneBookAsync(id, false);
            return NoContent();

        }

        [Authorize(Roles = "Editor, Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();

            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);

            return NoContent(); //204

        }

        [Authorize]
        [HttpOptions]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
