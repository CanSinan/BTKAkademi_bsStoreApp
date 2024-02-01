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
        public IActionResult GetAllBooks()
        {
            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);
        }
        [HttpGet("{id}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {

            var book = _manager
           .BookService
           .GetOneBook(id, false);

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] BookDtoForInsertion bookDto)
        {

            if (bookDto is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var book = _manager.BookService.CreateOneBook(bookDto);

            return StatusCode(201, book); // CreatedAtRoute() responseun headerına bir location koyabiliyoruz.

        }

        [HttpPut("{id}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDtobook)
        {

            if (bookDtobook is null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _manager.BookService.UpdateOneBook(id, bookDtobook, false);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {

            _manager.BookService.DeleteOneBook(id, false);
            return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();

            var result = _manager.BookService.GetOneBookForPatch(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _manager.BookService.SaveChangesForPatch(result.bookDtoForUpdate,result.book);

            return NoContent(); //204

        }
    }
}
