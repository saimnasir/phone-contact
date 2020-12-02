using System;

namespace ViewModels
{
    public class ViewModel
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted  { get; set; }
    }
}
