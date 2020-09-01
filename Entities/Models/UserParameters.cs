using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
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
