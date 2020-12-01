using Core.Enums;

namespace ViewModels
{
    public class ContactInfo : ViewModel
    {
        public long Person { get; set; }
        public InfoType InfoType { get; set; }
        public string Information { get; set; }
    }
}
