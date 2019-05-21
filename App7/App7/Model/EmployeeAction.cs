using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public class EmployeeAction
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int? TaskId { get; set; }
        public virtual Tasks task { get; set; }
        public DateTime Time { get; set; }
        public string Status { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
