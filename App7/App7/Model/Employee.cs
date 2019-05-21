using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string Gender { get; set; }
        public string SAPEmployeeNumber { get; set; }
        public string Address { get; set; }
        public decimal? AddressLongitude { get; set; }
        public decimal? AddressLatitude { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<Tasks> Tasks { get; set; }
        public string ParentJobStatus { get; set; }
        public int? ParentJobReportId { get; set; }
        public bool HasReportOnParentJob { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string IMEI { get; set; }
        public string SerialSIMCard { get; set; }
        public string LineNnumber { get; set; }
        public string PhoneModel { get; set; }
        public string GCMKey { get; set; }
        public string LastStatus { get; set; }
        public DateTime? LastStatusTime { get; set; }
        public bool IsActive { get; set; }
        public virtual Phone phone { get; set; }

        public List<EmployeeAction> EmployeeActions { get; set; }
    }
}
