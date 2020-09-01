using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserParameters : QueryStringParameters
    {
        public UserParameters()
        {
            OrderBy = "UserName";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
