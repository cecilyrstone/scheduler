using System;

namespace Scheduler.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int AddressId { get; set; }
        public bool Active { get; set; }
        public Address Address { get; set; }
    }
}
