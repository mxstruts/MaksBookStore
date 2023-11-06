using System;
using System.Collections.Generic;
using System.Text;
using MaksBooks.Models;

namespace MaksBooks.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository
    {
        void Update(Category category);
    }
}
