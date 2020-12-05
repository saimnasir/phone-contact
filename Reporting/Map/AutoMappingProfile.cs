using AutoMapper;
using System;

namespace Reporting.Map
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            reportMapper();
            reportDetailMapper();
        }

        protected virtual void ViceVersa<T1, T2>()
        {
            CreateMap<T1, T2>();
            CreateMap<T2, T1>();
        }

        private void reportMapper()
        {
            ViceVersa<DataModels.Report, ViewModels.Report>();
            ViceVersa<DataModels.Report, ViewModels.Requests.CreateReportRequest>();
            ViceVersa<DataModels.Report, ViewModels.Requests.UpdateReportRequest>();
            
        }

        private void reportDetailMapper()
        {
            ViceVersa<DataModels.ReportDetail, ViewModels.ReportDetail>();
        }

    }
}
