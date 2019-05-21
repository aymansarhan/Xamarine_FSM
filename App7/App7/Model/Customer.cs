using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public class Customer
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string ForeignMame { get; set; }
        public string AliasName { get; set; }
        public Branch branch { get; set; }
        public string Address { get; set; }
    }
}
