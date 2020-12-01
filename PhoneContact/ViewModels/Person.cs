using System.Collections.Generic;

namespace ViewModels
{
    public class Person : ViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }      
        public List<ContactInfo> ContactInfos { get; set; }
    }
}
