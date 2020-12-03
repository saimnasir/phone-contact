using Core.Enums;

namespace DataModels
{
    public class Report : DataModel
    {
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
        public ReportStatuses Status { get; set; }
    }
}
