using StarSecurityService.Data;
using StarSecurityService.Models;

namespace StarSecurityService.Components
{
    public class ServiceVM
    {
        StarSecurityServiceDbContext db = null;

        public ServiceVM() {
            
            db = new StarSecurityServiceDbContext();
        }

        public List<Service> ListAll() => db.Services.ToList();
    }
}
