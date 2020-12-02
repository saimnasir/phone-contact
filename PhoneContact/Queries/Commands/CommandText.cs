namespace Queries.Commands
{
    public class CommandText : ICommandText
    {
        public string CurrentTableName { get; set; }        
       
        public string CreateCommand => $"PHC.INS_{CurrentTableName}_SP";

        public string ReadCommmand => $"PHC.SEL_{CurrentTableName}_SP";

        public string UpdateCommand => $"PHC.UPD_{CurrentTableName}_SP";

        public string DeleteCommand => $"PHC.DEL_{CurrentTableName}_SP";

        public string ListAllCommand => $"PHC.LST_{CurrentTableName}_SP";

        public string SearchCommand => $"PHC.SRC_{CurrentTableName}_SP";

        public string FindCommand => $"PHC.FND_{CurrentTableName}_SP";

    }
}
