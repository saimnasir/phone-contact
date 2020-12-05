using Core.Enums;
using DataModels;
using System.Collections.Generic;

namespace Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        public Location CreateOrUpdate(Location person);
        public Location Create(Location person);
        public Location Update(Location person);
    }
}
