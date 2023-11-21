using System;
using System.Collections.Generic;
using System.Text;
using MaksBooks.Models;

namespace MaksBooks.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}