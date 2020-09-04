using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
		private RepositoryContext _repoContext;
		private IAppUserRepository _appUser;
		private ISortHelper<AppUser> _appUserSortHelper;

        public IAppUserRepository AppUser
        {
            get
            {
                if (_appUser == null)
                    _appUser = new AppUserRepository(_repoContext, _appUserSortHelper);

                return _appUser;
            }
        }

        public RepositoryWrapper(RepositoryContext repo, ISortHelper<AppUser> appUserSortHelper)
        {
            _repoContext = repo;
            _appUserSortHelper = appUserSortHelper;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
