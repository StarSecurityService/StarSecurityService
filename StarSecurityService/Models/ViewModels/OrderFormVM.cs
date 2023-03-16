namespace StarSecurityService.Models.ViewModels
{
    public class OrderFormVM
    {
        public int serviceId { get; set; }
        public int accountId { get; set; }
        public int amount { get; set; }
        public int duration { get; set; }
        public DateTime startDate { get; set; }

    }
}
