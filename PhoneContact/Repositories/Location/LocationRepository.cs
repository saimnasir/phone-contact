using Core.Enums;
using DataModels;
using Microsoft.Extensions.Configuration;
using Queries.Commands;
using Queries.Executers;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(IConfiguration configuration, ICommandText commandText, IExecuters executers, string tableName)
            : base(configuration, commandText, executers, tableName)
        {
        }

        public Location CreateOrUpdate(Location dataModel)
        {
            var locations =ListAllByMaster(dataModel.ContactInfo);
            var personLocationExists = ListAllByMaster(dataModel.ContactInfo).Any();
            if (!personLocationExists)
            {
                var parameters = new
                {
                    dataModel.ContactInfo,
                    dataModel.Latitude,
                    dataModel.Longitude
                };
                return base.Create(parameters);
            }
            else
            {
                var parameters = new
                {
                    dataModel.Id,
                    dataModel.ContactInfo,
                    dataModel.Latitude,
                    dataModel.Longitude
                };
                return base.Update(parameters);
            }
        }
        public Location Create(Location dataModel)
        {
            return CreateOrUpdate(dataModel);
        }

        public Location Update(Location dataModel)
        {
            return CreateOrUpdate(dataModel);
        }


    }
}
