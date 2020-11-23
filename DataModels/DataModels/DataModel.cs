using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class DataModel
    {
        public long Id { get; set; }
        public Guid UIID { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public EntityStates EntityState { get; set; }

    }
}
