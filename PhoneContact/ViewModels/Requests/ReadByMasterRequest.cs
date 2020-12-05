using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneContact.ViewModels.Requests
{
    public class ReadByMasterRequest
    {
        public long MasterId { get; set; }
        public Guid MasterUIID { get; set; }
    }
}
