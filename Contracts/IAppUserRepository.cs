using Entities;
using Entities.Helpers;
using Entities.Models;
using System;

namespace Contracts
{
    public interface IAppUserRepository : IRepositoryBase<AppUser>
    {
		PaginatedList<AppUser> GetUsers(UserParameters ownerParameters);
		AppUser GetUserById(Guid ownerId);
		void CreateUser(AppUser owner);
		void UpdateUser(AppUser dbOwner, AppUser owner);
		void DeleteUser(AppUser owner);
	}
}
