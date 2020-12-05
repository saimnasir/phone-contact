using Core.Enums;
using DataModels;
using Microsoft.Extensions.Configuration;
using Queries.Commands;
using Queries.Executers;
using System.Collections.Generic;

namespace Repositories
{
    public class ContactInfoRepository : Repository<ContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(IConfiguration configuration, ICommandText commandText, IExecuters executers, string tableName)
            : base(configuration, commandText, executers, tableName)
        {
        }

        public ContactInfo Create(ContactInfo dataModel)
        {
            var parameters = new
            {
                dataModel.Person,
                dataModel.InfoType,
                dataModel.Information
            };
            return base.Create(parameters);
        }

        public IEnumerable<ContactInfo> ListByType(InfoType type)
        {
            var parameters = new
            {
                InfoType = type
            };
            return base.ListByCommand("PHC.LST_CONTACTINFOBYTYPE_SP", parameters);
        }

        public ContactInfo Update(ContactInfo dataModel)
        {
            var parameters = new
            {
                dataModel.Id,
                dataModel.UIID,
                dataModel.Person,
                dataModel.InfoType,
                dataModel.Information
            };
            return base.Update(parameters);
        }

        public NearbyCountModel GetNearbyCounts(NearbyCountInputModel input)
        {
            var parameters = new
            {
                input.Radius,
                input.Latitude,
                input.Longitude
            };
            var query = base.Execute("PHC.SEL_NEARBYCOUNTS_SP", parameters);
            var fields = query as IDictionary<string, object>;
            
             var result =  new NearbyCountModel
             {
                 NearbyPersonCount = (int)fields["NearByPersonCount"],
                 NearbyPhoneNumberCount = (int)fields["NearByPhoneNumberCount"],
             };
            return result;
        }

    }
}
