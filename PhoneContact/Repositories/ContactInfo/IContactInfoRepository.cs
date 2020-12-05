using Core.Enums;
using DataModels;
using System.Collections.Generic;

namespace Repositories
{
    public interface IContactInfoRepository : IRepository<ContactInfo>
    {
        public ContactInfo Create(ContactInfo person);
        public ContactInfo Update(ContactInfo person);
        public IEnumerable<ContactInfo> ListByType(InfoType type);
        public NearbyCountModel GetNearbyCounts(NearbyCountInputModel input);
    }
}
