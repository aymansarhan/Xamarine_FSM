using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public class Report
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public decimal Cost { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public int EmployeeId { get; set; }

        public int TaskId { get; set; }

        public string CurrentUserId { get; set; }

        public string EmployeeName { get; set; }
    }
}