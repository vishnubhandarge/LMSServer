using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.DTOs;
using LMS.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext _bookDbContext;
        private readonly IConfiguration configuration;
        public BookController(BookDbContext bookDbContext, IConfiguration configuration)
        {
            this._bookDbContext = bookDbContext;
            this.configuration = configuration;
        }

        // Get All Books
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            // Fetch all books from the database
            var books = await _bookDbContext.TblBooks.ToListAsync();

            // Map the books to GetAllResponseDTO
            var bookDtos = books.Select(book => new GetAllResponseDTO
            {
                Title = book.Title,
                CategoryType = book.CategoryType,
                AuthorName = book.AuthorName,
                PublicationName = book.PublicationName,
                ISBN = book.ISBN,
                CreatedDate = book.CreatedDate
            }).ToList();

            // Return the mapped list of books with a 200 OK response
            return Ok(bookDtos);
        }

        // Add Book method
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(AddBookRequestDTO addBookRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid || addBookRequestDTO == null)
                {
                    var response = new ResponseDTO
                    {
                        StatusCode = 404,
                        Message = "Wrong input"
                    };
                    return BadRequest(response);
                }
                var isISBNAvail = await _bookDbContext.TblBooks.FirstOrDefaultAsync(c => c.ISBN == addBookRequestDTO.ISBN);
                if (isISBNAvail != null)
                {
                    var response = new ResponseDTO
                    {
                        StatusCode = 404,
                        Message = "Book already available!"
                    };
                    return BadRequest(response);
                }
                var newBook = new tblBooks
                {
                    Title = addBookRequestDTO.Title,
                    CategoryType = addBookRequestDTO.Category,
                    AuthorName = addBookRequestDTO.AuthorName,
                    PublicationName = addBookRequestDTO.PublicationName,
                    ISBN = addBookRequestDTO.ISBN,
                    CreatedBy = addBookRequestDTO.CreatedBy,
                    CreatedDate = addBookRequestDTO.CreatedDate// Only assign CreatedDate
                };

                await _bookDbContext.TblBooks.AddAsync(newBook);
                await _bookDbContext.SaveChangesAsync();
            }

            catch
            {

                var responseError = new ResponseDTO
                {
                    StatusCode = 500,
                    Message = "Internal server Error!"
                };
                return BadRequest(responseError);
            }

            var Successresponse = new ResponseDTO
            {
                StatusCode = 201,
                Message = "Book Added!"
            };
            return Ok(Successresponse);
        }

        // Remove Book method
        [HttpDelete("removeBook")]
        public async Task<IActionResult> RemoveBook([FromBody] RemoveBookRequestDTO removeBookRequestDTO)
        {
            // Check if the request is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO
                {
                    StatusCode = 400,
                    Message = "Invalid request"
                });
            }

            // Find the book by Id
            var book = await _bookDbContext.TblBooks.FindAsync(removeBookRequestDTO.Id);

            if (book == null)
            {
                return NotFound(new ResponseDTO
                {
                    StatusCode = 404,
                    Message = "Book not found"
                });
            }

            // Remove the book from the context
            _bookDbContext.TblBooks.Remove(book);

            // Save changes to the database
            await _bookDbContext.SaveChangesAsync();

            // Return a success response
            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "Book removed successfully"
            });
        }

        //Search book
        [HttpPost(("SearchBook"))]
        public async Task<IActionResult> SearchBook( SearchBookRequestDTO searchBookRequestDTO)
        {

            if (!ModelState.IsValid || searchBookRequestDTO == null)
            {
                var response = new ResponseDTO
                {
                    StatusCode = 404,
                    Message = "Wrong input"
                };
                return BadRequest(response);
            }

            var book = _bookDbContext.TblBooks.FirstOrDefault(c => 
                c.AuthorName == searchBookRequestDTO.Input || 
                c.ISBN == searchBookRequestDTO.Input || 
                c.Title == searchBookRequestDTO.Input);

            if (book != null)
            {
                return Ok(new GetAllResponseDTO
                {
                    Title = book.Title,
                    CategoryType = book.CategoryType,
                    AuthorName = book.AuthorName,
                    PublicationName = book.PublicationName,
                    ISBN = book.ISBN
                });
            }

            return NotFound(new ResponseDTO
            {
                StatusCode = 404,
                Message = "Book not found"
            });
        }
    }
}
