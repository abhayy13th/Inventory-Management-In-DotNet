using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Data.Model
{
    
        public class User
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string Username { get; set; }
            public string PasswordHash { get; set; }

            public RoleModel Role { get; set; }
    }

    }

