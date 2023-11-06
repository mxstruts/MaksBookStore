using System;
using System.Collections.Generic;
using System.Text;
using MaksBooks.Models;

namespace MaksBooks.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository :IRepository<Category>
    {
        void Update(Category category);
    }
}
