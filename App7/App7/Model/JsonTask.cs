using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace App7.Model
{
    public class JsonTasks
    {
        public string date { get; set; }
        
        public ObservableCollection<Tasks> jobs { get; set; }

        public bool IsVisible { get; set; }
        
    }
}
