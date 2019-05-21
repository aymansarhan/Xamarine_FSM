using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public class PointOfContact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CustomerId { get; set; }
        public Customer site { get; set; }
        public string Type { get; set; }
    }
}
