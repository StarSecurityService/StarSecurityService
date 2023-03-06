using PagedList.Core;
using StarSecurityService.Data;
using StarSecurityService.Models;
using System.Collections;
using System.Collections.Generic;

namespace StarSecurityService.Components
{
    public class RoleComponents
    {
        StarSecurityServiceDbContext db = null;

        public RoleComponents()
        {

            db = new StarSecurityServiceDbContext();
        }

        public List<Role> ListAll() => db.Roles.ToList();

    }
}
