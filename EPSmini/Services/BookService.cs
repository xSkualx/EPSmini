using EPSmini.Contexts;
using EPSmini.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace EPSmini.Services
{
    public class BookService : IBookService
    {
        private readonly BooksContext _context;

        public BookService(BooksContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Book>>> GetAllBooks()
        {
            ServiceResponse<List<Book>> serviceResponse = new ServiceResponse<List<Book>>();
            await CheckIfEmpty();
            try
            {
                serviceResponse.Data = await _context.Books.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task CheckIfEmpty()
        {
            
                if (!_context.Books.Any())
                {
                    _context.Books.Add(new Book { autorius = "J.K Rowling", pavadinimas = "Harry Potter 1", ISBN = "534-5345-54356543"});
                    _context.Books.Add(new Book { autorius = "J.K Rowling", pavadinimas = "Harry Potter 2", ISBN = "534-5345-54254348"});
                    _context.Books.Add(new Book { autorius = "J.K Rowling", pavadinimas = "Harry Potter 3", ISBN = "534-5345-54388543"});
                    _context.Books.Add(new Book { autorius = "J.K Rowling", pavadinimas = "Harry Potter 4", ISBN = "534-5345-54359943"});
                    _context.Books.Add(new Book { autorius = "J.K Rowling", pavadinimas = "Harry Potter 5", ISBN = "534-5345-54354300"});
                    await _context.SaveChangesAsync();
                }
            
        }
    }
}
