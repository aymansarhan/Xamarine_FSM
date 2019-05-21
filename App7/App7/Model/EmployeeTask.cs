using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public  class EmployeeTask
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int TaskId { get; set; }
        public virtual Tasks Task { get; set; }
        public string Status { get; set; }
        public Nullable<decimal> GoingDistance { get; set; }
        public Nullable<decimal> ReturningDistance { get; set; }
        public DateTime? Time { get; set; }
    }
}
