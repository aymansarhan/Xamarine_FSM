using System;
using System.Collections.Generic;
using System.Text;

namespace App7.Model
{
    public class Phone
    {
        public int Id { get; set; }
        public string IMEICode { get; set; }
        public string SerialSIMCard { get; set; }
        public string PhoneModel { get; set; }
        public string LineNnumber { get; set; }
        public string GCMKey { get; set; }
        public string ApkVersionNo { get; set; }
    }
}