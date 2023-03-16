using StarSecurityService.Data;
using StarSecurityService.Models;
using System.Linq;

namespace StarSecurityService.Components
{
    public class ServiceComponents
    {
        StarSecurityServiceDbContext db = null;

        public ServiceComponents() {
            
            db = new StarSecurityServiceDbContext();
        }

        public List<Service> ListAll() => db.Services.ToList();
        public List<Service> ListAllReverse() => db.Services.OrderByDescending(s => s.ServiceId).ToList();
    }
}
