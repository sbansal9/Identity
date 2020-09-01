using Identity.Models;
using System.Collections.Generic;

namespace Identity.Contracts
{
    public interface IAppUserRepo
    {
        bool SaveChanges();

        IEnumerable<AppUser> GetAllCommands();
        AppUser GetCommandById(int id);
        void CreateCommand(AppUser cmd);
        void UpdateCommand(AppUser cmd);
        void DeleteCommand(AppUser cmd);


        //public Task<List<SpGetProductByID>> GetProductByIDAsync(int productId);
        //public Task<List<SpGetProductByPriceGreaterThan1000>> GetProductByPriceGreaterThan1000Async();
    }
}
