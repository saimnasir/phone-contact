using AutoMapper;

namespace PhoneContact.Map
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            ViceVersa<DataModels.Report, ViewModels.Report>();
            ViceVersa<DataModels.ReportDetail, ViewModels.ReportDetail>();
        }
        protected virtual void ViceVersa<T1, T2>()
        {
            CreateMap<T1, T2>();
            CreateMap<T2, T1>();
        }
    }
}
