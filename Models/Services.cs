namespace EverBill.Models
{
    public class Services
    {
        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public int ServicePrice { get; set; }
        public int ServiceQty { get; set; }
        public int ServiceTotal { get; set; }
        public int CustomerId { get; set; }
        // public string? ProjectListOfTasks { get; set; }
        //public string? ProjectCompany { get; set; }
    }
}