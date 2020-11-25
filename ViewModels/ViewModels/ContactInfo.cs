using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class ContactInfo : ViewModel
    {
        public long Person { get; set; }
        public InfoType InfoType { get; set; }
        public string Information { get; set; }
    }
}
