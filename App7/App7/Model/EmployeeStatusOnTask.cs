using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public class EmployeeStatusOnTask
    {
        public int taskId { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Speed { get; set; }
        public decimal distanceInKM { get; set; }
        public int batteryStatus { get; set; }
        public bool gpsIsOpened { get; set; }
        public bool networkIsAvailable { get; set; }
        public bool IsVisible { get; set; }
    }
}
