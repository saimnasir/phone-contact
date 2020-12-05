﻿using DataModels;
using Microsoft.Extensions.Configuration;
using Queries.Commands;
using Queries.Executers;

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

    }
}
