using System;
using System.Collections.Generic;
using Core;
using DataModels;
using Microsoft.Extensions.Configuration;
using Queries;

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
            return base.Create(dataModel, parameters);
        }

        public ContactInfo Update(ContactInfo dataModel)
        {
            var parameters = new
            {
                dataModel.Id,
                dataModel.Person,
                dataModel.InfoType,
                dataModel.Information
            };
            return base.Update(dataModel, parameters);
        }

    }
}
