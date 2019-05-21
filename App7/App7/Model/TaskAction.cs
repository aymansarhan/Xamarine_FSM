using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public class TaskAction
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime Time { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
