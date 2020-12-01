using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class ContactInfo : DataModel
    {
        public long Person { get; set; }
        public InfoType InfoType { get; set; }
        public string Information { get; set; }
    }
}
