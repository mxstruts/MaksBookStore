using MaksBooks.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using MaksBookStore.DataAccess.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MaksBooks.DataAccess.Repository
{
    internal class SP_Call : ISP_Call
    {
        // access to database 
        private readonly ApplicationDbContext _db;
        private static string ConnectionString = ""; // needed to call a procedure 

        // constructor to open sql connection
        public SP_Call(ApplicationDbContext db)
        {
            _db = db;
            ConnectionString=db.Database.GetDbConnection().ConnectionString;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public T single<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                return (T)Convert.ChangeType(SqlCon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
            }
        }

        public void Execute(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                SqlCon.Execute(procedureName, param,commandType:System.Data.CommandType.StoredProcedure);
            }
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var value = SqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
                return (T)Convert.ChangeType(value.FirstOrDefault(),typeof(T));
            }
        }

        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                return SqlCon.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
               
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection SqlCon = new SqlConnection(ConnectionString))
            {
                SqlCon.Open();
                var result = SqlMapper.QueryMultiple(SqlCon, procedureName, param,
                    commandType: System.Data.CommandType.StoredProcedure);
                var item1 = result.Read<T1>().ToList();
                var item2 = result.Read<T2>().ToList();
                if (item1!=null && item2!=null)
                {
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1,item2);
                }
            }
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
        }
    }
}
