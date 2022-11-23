using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTB.Eshop.Web.Models.Identity
{
    public class Role : IdentityRole<int>
    {
        public Role() : base() { }
        public Role(string userName) : base(userName) { }
    }
}
