using System;
namespace ASPCities.Models
{
    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Prev { get; set; }
        public bool Status { get; set; }
    }
}
