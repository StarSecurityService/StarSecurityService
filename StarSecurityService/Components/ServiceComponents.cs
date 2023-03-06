using StarSecurityService.Data;
using StarSecurityService.Models;

namespace StarSecurityService.Components
{
    public class ServiceComponents
    {
        StarSecurityServiceDbContext db = null;

        public ServiceComponents() {
            
            db = new StarSecurityServiceDbContext();
        }

        public List<Service> ListAll() => db.Services.ToList();
    }
}
