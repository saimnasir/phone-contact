using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
   public interface IContactInfoRepository : IRepository<ContactInfo>
    {
        public ContactInfo Create(ContactInfo person);
        public ContactInfo Update(ContactInfo person);
    }
}
