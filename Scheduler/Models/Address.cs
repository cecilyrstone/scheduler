using System;

namespace Scheduler.Models
{
    public class Address
    {
        // There is no state id in the ERD so nowhere to persist that data.
        // Omitting state id.
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
    }
}
