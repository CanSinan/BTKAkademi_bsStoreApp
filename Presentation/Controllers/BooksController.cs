using AutoMapper;
using Entities.DataTranferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        public BooksController(IServiceManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _manager.BookService.GetAllBooksAsync(false);
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {

            var book = await _manager
           .BookService
           .GetOneBookAsync(id, false);

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
        {

            if (bookDto is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var book = await _manager.BookService.CreateOneBookAsync(bookDto);

            return StatusCode(201, book); // CreatedAtRoute() responseun headerına bir location koyabiliyoruz.

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDtobook)
        {

            if (bookDtobook is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            await _manager.BookService.UpdateOneBookAsync(id, bookDtobook, false);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {

            await _manager.BookService.DeleteOneBookAsync(id, false);
            return NoContent();

        }

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
    }
}
