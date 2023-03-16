namespace StarSecurityService.Models.ViewModels
{
    public class OrderFormVM
    {
        public int serviceId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

        public string address { get; set; }
        public int amount { get; set; }
        public int duration { get; set; }
        public DateTime startDate { get; set; }

    }
}
