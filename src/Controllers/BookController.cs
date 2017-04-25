// ----------------------------------------------------------------------------
// <copyright file="BookController.cs" company="NanoTaboada">
// Copyright (c) 2017 Nano Taboada, http://openid.nanotaboada.com.ar 
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
// -----------------------------------------------------------------------------

using Dotnet.Samples.Core.Models;
using Dotnet.Samples.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Samples.Core.Controllers
{
    [Produces("application/json")]
    public class BookController : Controller
    {
        private IBookService bookService;
        
        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        /// <summary>
        /// The GET method means retrieve whatever information
        /// (in the form of an entity) is identified by the Request-URI.
        /// </summary>
        /// <returns>
        /// A collection of Books and status code 200 (OK),
        /// or status code 204 (No Content).
        /// </returns>
        [Route("api/books")][HttpGet]
        public IActionResult Get()
        {
            var books = this.bookService.RetrieveAll();

            if (books == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(books);
            }
        }

        /// <summary>
        /// The POST method is used to request that the origin server accept
        /// the entity enclosed in the request as a new subordinate of the
        /// resource identified by the Request-URI in the Request-Line.
        /// </summary>
        /// <param name="book">A Book.</param>
        /// <returns>
        /// A Location header with link to new Book and status code 
        /// 201 (Created), or status code 409 (Conflict) if the Book already
        /// exists, or status code 400 (Bad Request) if the ISBN is invalid.
        /// </returns>
        [Route("api/books")][HttpPost]
        public IActionResult Post([FromBody]Book book)
        {
            if (!this.bookService.IsValidIsbn(book.Isbn))
            {
                return BadRequest();
            }
            else
            {
                var existing = this.bookService.RetrieveByIsbn(book.Isbn);

                if (existing != null)
                {
                    // TODO: Improve this
                    return StatusCode(409);  
                }
                else
                {
                    this.bookService.Create(book);
                    var location = string.Format("/book/{0}", book.Isbn);
                    
                    return Created(location, book);
                }
            }
        }

        /// <summary>
        /// The GET method means retrieve whatever information
        /// (in the form of an entity) is identified by the Request-URI.
        /// </summary>
        /// <param name="isbn">The ISBN of a Book.</param>
        /// <returns>
        /// A Book matching the provided ISBN and status code 200 (OK),
        /// or 404 (Not Found) if the ISBN was not found or is invalid.
        /// </returns>
        [Route("api/book/{isbn}")][HttpGet]
        public IActionResult Get(string isbn)
        {
            if (this.bookService.IsValidIsbn(isbn))
            {
                return Ok(this.bookService.RetrieveByIsbn(isbn));
            }
            else
            {
                return NotFound();
            }
        }
                
        /// <summary>
        /// The PUT method requests that the enclosed entity be stored under 
        /// the supplied Request-URI. If the Request-URI refers to an already
        /// existing resource, the enclosed entity SHOULD be considered as a
        /// modified version of the one residing on the origin server. If the
        /// resource could not be created or modified with the Request-URI,
        /// an appropriate error response SHOULD be given that reflects the
        /// nature of the problem. 
        /// </summary>
        /// <param name="isbn">The ISBN of a Book.</param>
        /// <param name="book">A Book.</param>
        /// <returns>
        /// A status code 204 (No Content) if the Books is successfully updated,
        /// or 404 (Not Found) if the ISBN was not found,
        /// or 400 (Bad Request) if the ISBN was invalid.
        /// </returns>
        [Route("api/book/{isbn}")][HttpPut]
        public IActionResult Put(string isbn, [FromBody]Book book)
        {
            if (!this.bookService.IsValidIsbn(isbn)
                || !this.bookService.IsValidIsbn(book.Isbn)
                || (isbn != book.Isbn))
            {
                return BadRequest();
            }
            else
            {
                var existing = this.bookService.RetrieveByIsbn(isbn);

                if (existing == null)
                {
                    return NotFound();
                }
                else
                {
                    this.bookService.Update(book);
                    return NoContent();
                }
            }
        }
        
        /// <summary>
        /// The DELETE method requests that the origin server delete the 
        /// resource identified by the Request-URI.This method MAY be 
        /// overridden by human intervention(or other means) on the origin 
        /// server.The client cannot be guaranteed that the operation has been
        /// carried out, even if the status code returned from the origin server
        /// indicates that the action has been completed successfully. However,
        /// the server SHOULD NOT indicate success unless, at the time the
        /// response is given, it intends to delete the resource or move it to
        /// an inaccessible location.
        /// </summary>
        /// <param name="isbn">The ISBN of a Book.</param>
        [Route("api/book/{isbn}")][HttpDelete]
        public IActionResult Delete(string isbn)
        {
            if (!this.bookService.IsValidIsbn(isbn))
            {
                return NotFound();
            }
            else
            {
                this.bookService.Delete(isbn);
                return NoContent();
            }
        }
    }
}
