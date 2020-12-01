using DataModels;

namespace Repositories
{
    public interface IContactInfoRepository : IRepository<ContactInfo>
    {
        public ContactInfo Create(ContactInfo person);
        public ContactInfo Update(ContactInfo person);
    }
}
