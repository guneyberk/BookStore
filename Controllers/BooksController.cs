using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Data;
using BookStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Controllers{
    public class BooksController: Controller{
        private readonly BookContext _bookContext;
    
        public BooksController(BookContext bookContext){
            _bookContext = bookContext;
        }

        public async Task<IActionResult> Index(){
            var books = await _bookContext.Books.ToListAsync();
            return View(books);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book){
            if(ModelState.IsValid){
                _bookContext.Add(book);
                await _bookContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public async Task<IActionResult> Delete(int? id){
            if(id == null){
                return NotFound();
            }
            var book = await _bookContext.Books.FindAsync(id);
            if(book == null){
                return NotFound();
            }

            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id){
            var book = await _bookContext.Books.FindAsync(id);
            _bookContext.Books.Remove(book);
            await _bookContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id){
            return _bookContext.Books.Any(e => e.Id == id);
        }
    }

}