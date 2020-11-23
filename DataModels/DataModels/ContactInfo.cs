using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class ContactInfo : DataModel
    {
        public InfoType Location { get; set; }
        public string Information { get; set; }
    }
}
