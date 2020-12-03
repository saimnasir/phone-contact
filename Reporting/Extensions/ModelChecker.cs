using DataModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.Extensions
{
    public static class ModelChecker
    {
        public static void ModelCheck(this DataModel model, Guid uiid)
        {
            if( model.UIID != uiid)
            {
                throw new Exception("Girilen 'id' ile 'uiid' eşleşmemektedir.");
            }
        }
    }
}
