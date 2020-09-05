using Identity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Data
{
    public class PersonRepo : IPersonRepo
    {
        private readonly CommanderContext _context;

        public PersonRepo(CommanderContext context)
        {
            _context = context;
        }

        //public IEnumerable<Person> GetAll()
        //{
        //    return _context.Person.ToList();
        //}
        public async Task<List<Person>> GetAllAsync()
        {
            return await _context.Person.ToListAsync();
        }


        public Person GetById(int id)
        {
            return _context.Person.FirstOrDefault(p => p.Id == id);
        }


        //public IEnumerable<Person> GetByManagerId(string mgrId)
        //{
        //    var result = _context.Person.Where(x => x.ManagerId == mgrId);
        //    return result;
        //}


        public async Task<List<Person>> GetByManagerIdAsync(string mgrId)
        {
            var result = await _context.Person.Where(x => x.ManagerId == mgrId).ToListAsync();
            return (result);
        }

        ///// <summary>  
        ///// Get product by ID store procedure method.  
        ///// </summary>  
        ///// <param name="productId">Product ID value parameter</param>  
        ///// <returns>Returns - List of product by ID</returns>  
        //public async Task<List<SpGetProductByID>> GetProductByIDAsync(int productId)
        //{
        //    // Initialization.  
        //    List<SpGetProductByID> lst = new List<SpGetProductByID>();

        //    try
        //    {
        //        // Settings.  
        //        SqlParameter usernameParam = new SqlParameter("@product_ID", productId.ToString() ?? (object)DBNull.Value);

        //        // Processing.  
        //        string sqlQuery = "EXEC [dbo].[GetProductByID] " +
        //                            "@product_ID";

        //        lst = await _context.Query<SpGetProductByID>().FromSqlRaw(sqlQuery, usernameParam).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    // Info.  
        //    return lst;
        //}

        ///// <summary
        ///// Get Products whose price is greater than equal to 1000 store procedure method.
        ///// </summary>
        ///// <returns>Returns - List of products whose price is greater than equal to 1000
        ///// </returns>
        //public async Task<List<SpGetProductByPriceGreaterThan1000>> GetProductByPriceGreaterThan1000Async()
        //{
        //    // Initialization.  
        //    List<SpGetProductByPriceGreaterThan1000> lst = new List<SpGetProductByPriceGreaterThan1000>();

        //    try
        //    {
        //        // Processing.  
        //        string sqlQuery = "EXEC [dbo].[GetProductByPriceGreaterThan1000] ";

        //        lst = await _context.Query<SpGetProductByPriceGreaterThan1000>().FromSqlRaw(sqlQuery).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    // Info.  
        //    return lst;
        //}

        public bool SaveChanges()
        { 
            return (_context.SaveChanges() > 0);
        }

        public void Create(Person p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            _context.Person.Add(p);
        }

        public void Update(Person cmd)
        {
            // Nothing
        }

        public void Delete(Person p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            _context.Person.Remove(p);
        }


    }
}
