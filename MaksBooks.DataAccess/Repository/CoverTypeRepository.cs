using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaksBooks.DataAccess.Repository.IRepository;
using MaksBooks.Models;
using MaksBookStore.DataAccess.Data;

namespace MaksBooks.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType coverType)
        {
            // use .NET Linq to retrieve the first or default category object,
            //then pass the id as a generic entity which matches the category ID
            var objFromDb = _db.CoverTypes.FirstOrDefault(s => s.Id == coverType.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = coverType.Name;
                //_db.SaveChanges();
            }
        }
    }
}