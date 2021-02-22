using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace BookLibrary
{
    public class BookCollection
    {
        public static ObservableCollection<Book> Books { get; set; }
        public BookCollection()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Books = new ObservableCollection<Book>(db.Books
                    .Include(t => t.TypeBook)
                    .Include(t => t.ReadBooks)
                    .ToList());
            }
        }
    }
}
