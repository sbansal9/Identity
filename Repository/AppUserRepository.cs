using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Repository
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        private ISortHelper<AppUser> _sortHelper;

        public AppUserRepository(RepositoryContext repo, ISortHelper<AppUser> sortHelper) : base(repo)
        {
            _sortHelper = sortHelper;
        }

        public PaginatedList<AppUser> GetAppUsers(AppUserParameters appUserParams)
        {
            var users = FindByCondition(o => o.Id == appUserParams.Id.ToString());

            SearchByName(ref users, appUserParams.UserName);

            var sortedUsers = _sortHelper.ApplySort(users, appUserParams.OrderBy);

            return PaginatedList<AppUser>.ToPagedList(sortedUsers, appUserParams.PageNumber, appUserParams.PageSize);
        }
        private void SearchByName(ref IQueryable<AppUser> users, string userName)
        {
            if (!users.Any() || string.IsNullOrWhiteSpace(userName))
                return;

            if (string.IsNullOrEmpty(userName))
                return;

            users = users.Where(o => o.UserName.ToLowerInvariant().Contains(userName.Trim().ToLowerInvariant()));
        }
        public AppUser GetAppUserById(Guid id)
        {
            return FindByCondition(o => o.Id.Equals(id)).DefaultIfEmpty(new AppUser()).FirstOrDefault();
        }


        public void CreateUser(AppUser user)
        {
            Create(user);
        }
        public void UpdateUser(AppUser dbUser, AppUser user)
        {
            //dbUser.Map(user);
            //Update(dbUser);
            throw new NotImplementedException();
        }
        public void DeleteUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public AppUser GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public PaginatedList<AppUser> GetUsers(AppUserParameters appUserParams)
        {
            throw new NotImplementedException();
        }

        PaginatedList<AppUser> IAppUserRepository.GetUsers(UserParameters ownerParameters)
        {
            throw new NotImplementedException();
        }
    }
}
