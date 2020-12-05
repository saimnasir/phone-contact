using Core.Enums;

namespace DataModels
{
    public class Report : DataModel
    {
        public double Radius { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ReportStatuses Status { get; set; }
    }
}
