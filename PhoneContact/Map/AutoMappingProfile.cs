using AutoMapper;

namespace PhoneContact.Map
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            personMapper();
            contactInfoMapper();
        }
        protected virtual void ViceVersa<T1, T2>()
        {
            CreateMap<T1, T2>();
            CreateMap<T2, T1>();
        }

        private void personMapper()
        {
            ViceVersa<DataModels.Person, ViewModels.Person>();
            ViceVersa<DataModels.Person, ViewModels.Requests.CreatePersonRequest>();
            ViceVersa<DataModels.ContactInfo, ViewModels.Requests.PersonContactInfo>();
            ViceVersa<DataModels.Person, ViewModels.Requests.UpdatePersonRequest>();
        }

        private void contactInfoMapper()
        {
            ViceVersa<DataModels.ContactInfo, ViewModels.ContactInfo>();
            ViceVersa<DataModels.ContactInfo, ViewModels.Requests.CreateContactInfoRequest>();
            ViceVersa<DataModels.ContactInfo, ViewModels.Requests.UpdateContactInfoRequest>();
        }
    }
}
