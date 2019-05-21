using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public DateTime? Closed { get; set; }
        public string ServiceCall { get; set; }
        public string ServiceOpportunity { get; set; }
        public Nullable<decimal> ReimbursableExpenses { get; set; }
        public Employee Employee { get; set; }
        public Branch Branch { get; set; }
        public TaskStatus Status { get; set; }
        public Customer customer { get; set; }
        public PointOfContact PointOfContact { get; set; }
        public TaskType Type { get; set; }
        public TaskCategory Category { get; set; }
        public string ProjectNumber { get; set; }
        public string SAPCustomerNumber { get; set; }
        public TransportationMean TransportationMean { get; set; }
        public int HasReport { get; set; }
        public int? ReportId { get; set; }
        public List<Report> Reports { get; set; }
        public List<TaskAction> TaskActions { get; set; }
        public Car Car { get; set; }
        public bool IsVisible { get; set; }
        public string employeeStatusOnJob { get; set; }

        public string Priority { set; get; }
        public string DestinationName { set; get; }
        public string DestinationAddress { set; get; }
        public string ContactName { set; get; }
        public string ContactPhone { set; get; }
        public string CurrentUserId { set; get; }
        public string CreatedBy { set; get; }
        public string CreatedByUserId { set; get; }
        public string CarAssignedBy { set; get; }
        public decimal? CheckedInLongitude { get; set; }
        public decimal? CheckedInLatitude { get; set; }
        public string CheckedInDistance { get; set; }
        public JsonTaskWorkingReport TaskWorkingReport { get; set; }
        public List<string> Images { get; set; }
        public int CheckedInTolerance { get; set; }
    }

    public class JsonTaskWorkingReport
    {
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public string WorkingTime { get; set; }
        public Nullable<DateTime> StartGoingTripTime { get; set; }
        public Nullable<DateTime> EndGoingTripTime { get; set; }
        public Nullable<DateTime> StartReturnTripTime { get; set; }
        public Nullable<DateTime> EndReturnTripTime { get; set; }
        public string TotalTripTime { get; set; }
        public TimeSpan GoingTime { get; set; }
        public TimeSpan ReturningTime { get; set; }
        public string TotalTripDistance { get; set; }
        public string GoingDistance { get; set; }
        public string ReturningDistance { get; set; }

    }
}
