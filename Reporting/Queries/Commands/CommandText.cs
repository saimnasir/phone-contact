namespace Queries.Commands
{
    public class CommandText : ICommandText
    {
        public string CurrentTableName { get; set; }        
       
        public string CreateCommand => $"RPT.INS_{CurrentTableName}_SP";

        public string ReadCommmand => $"RPT.SEL_{CurrentTableName}_SP";

        public string UpdateCommand => $"RPT.UPD_{CurrentTableName}_SP";

        public string DeleteCommand => $"RPT.DEL_{CurrentTableName}_SP";

        public string ListAllCommand => $"RPT.LST_{CurrentTableName}_SP";

        public string SearchCommand => $"RPT.SRC_{CurrentTableName}_SP";

        public string FindCommand => $"RPT.FND_{CurrentTableName}_SP";

    }
}
