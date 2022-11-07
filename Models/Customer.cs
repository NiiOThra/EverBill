namespace EverBill.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerCVRnumber { get; set; }
        public int CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string ListOfProjects { get; set; }
    }
}