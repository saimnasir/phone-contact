using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.PhoneContact
{
    public class PhoneContactOperations
    {
        public void GetPhoneContacts()
        {
            var client = new RestClient("https://localhost:44393/api/person/listall");
            var request = new RestRequest(Method.GET);
            //request.AddHeader("postman-token", "4aeca6e4-a5bc-d646-b60c-9b43e8bf5063");
            //request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
        }    
    }
}
