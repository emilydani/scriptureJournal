using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScriptureJournal.Models;

namespace ScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly ScriptureJournal.Models.ScriptureJournalContext _context;
        public IndexModel(ScriptureJournal.Models.ScriptureJournalContext context)
        {
            _context = context;
        }

        
        public PaginatedList<Journal> Journal { get; set; }
        public string SearchString { get; set; }
        public string KeySearchString { get; set; }
        public SelectList Book { get; set; }
        public string Books { get; set; }
        public string CurrentSort { get;  set; }

        public string BookSort { get; set; }
        public string DateSort { get; set; }

        public async Task OnGetAsync(string Books, string keySearchString, string searchString, string sortOrder, int? pageIndex)


        {
            CurrentSort = sortOrder;
            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = SearchString;
            }
            
            IQueryable<Journal> bookQuery = from s in _context.Book
                                            select s;
            

            if (!String.IsNullOrEmpty(searchString))
            {
                bookQuery = bookQuery.Where(s => s.Book.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(keySearchString))
            {
                bookQuery = bookQuery.Where(x => x.Note.Contains(keySearchString));
            }
            switch (sortOrder)
            {
                case "book_desc":
                    bookQuery = bookQuery.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    bookQuery = bookQuery.OrderBy(s => s.DateAdded);
                    break;

                default:
                    bookQuery = bookQuery.OrderBy(s => s.Book);
                    break;
            }

                        
            SearchString = searchString;
            int pageSize = 5;
            Journal = await PaginatedList<Journal>.CreateAsync(bookQuery.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
        
    }
}
