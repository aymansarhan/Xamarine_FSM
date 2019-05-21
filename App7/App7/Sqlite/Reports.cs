using System;
using System.Collections.Generic;
using System.Text;
using App7.Model;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace App7.Sqlite
{
    public class Reports
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public decimal Cost { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        [ForeignKey(typeof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey(typeof(Tasks))]
        public int TaskId { get; set; }
        public Tasks Task { get; set; }
    }
}
