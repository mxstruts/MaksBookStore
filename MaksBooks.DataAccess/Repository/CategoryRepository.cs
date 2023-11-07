using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaksBooks.DataAccess.Repository.IRepository;
using MaksBooks.Models;
using MaksBookStore.DataAccess.Data;

namespace MaksBooks.DataAccess.Repository
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db)
        {
           _db=db;
        }

        public void Update(Category category)
        {
            // use .NET Linq to retrieve the first or default category object,
            //then pass the id as a generic entity which matches the category ID
            var objFromDb = _db.Categories.FirstOrDefault(s => s.Id == category.Id);
            if (objFromDb != null)
            {
                objFromDb.Name= category.Name;
                //_db.SaveChanges();
            }
        }
    }
}
