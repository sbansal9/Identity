using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class AppUserParameters : QueryStringParameters
    {
        public AppUserParameters()
        {
            OrderBy = "UserName";
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
