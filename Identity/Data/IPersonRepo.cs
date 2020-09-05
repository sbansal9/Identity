using Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Data
{
    public interface IPersonRepo
    {
        bool SaveChanges();

        IEnumerable<Person> GetAll();
        Person GetById(int id);
        void Create(Person cmd);
        void Update(Person cmd);
        void Delete(Person cmd);

    }
}
