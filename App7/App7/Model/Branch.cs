using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public class Branch
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int customerId { get; set; }
        public Customer customer { get; set; }
        public string Type { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
    }
}
