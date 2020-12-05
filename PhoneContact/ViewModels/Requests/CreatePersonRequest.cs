using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneContact.ViewModels.Requests
{
    public class CreatePersonRequest
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }

        public IEnumerable<PersonContactInfo> ContactInfos { get; set; }
    }
    public class PersonContactInfo
    {
        public InfoType InfoType { get; set; }
        public string Information { get; set; }
    }
}
