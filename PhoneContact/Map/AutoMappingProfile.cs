using AutoMapper;

namespace PhoneContact.Map
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            ViceVersa<DataModels.Person, ViewModels.Person>();
            ViceVersa<DataModels.ContactInfo, ViewModels.ContactInfo>();
        }
        protected virtual void ViceVersa<T1, T2>()
        {
            CreateMap<T1, T2>();
            CreateMap<T2, T1>();
        }
    }
}
