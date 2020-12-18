using MyParking.DTO;
using SimpleGeneralFramework;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyParking.DL
{
    public class ContentDL
    {
        public int InsertUpdateContent(ContentsDTO content)
        {
            return DABase.NewContext().AddStoredProcedure("usp_InsertUpdateContent")
                .AddParameter("Id", content.ID, DbType.Int32)
                .AddParameter("HTMLContent", content.HTMLContent, DbType.String)
                .AddParameter("Description", content.Description)
                .AddParameter("CanEdit", content.CanEdit, DbType.Boolean)
                .AddParameter("Seq", content.Seq, DbType.Int32)
                .ExecuteNonquery();
        }
        public List<ContentsDTO> SearchAllContents()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT Id,HTMLContent,Description,CanEdit,Seq FROM dbo.Contents";
            command.CommandType = CommandType.Text;
            return CacheHelper.GetOrSetCache<List<ContentsDTO>>("k_content", command, true, () =>
            {
                return DABase.NewContext().AddStoredProcedure("usp_SelectContentsAll").Query<ContentsDTO>();
            });
        }
        public ContentsDTO SearchContentByID(int id)
        {
            var list = SearchAllContents();
            return list != null && list.Count > 0 ? list.Find(content => content.ID == id) : null;
        }
        public int DeleteContentByID(int id)
        {
            return DABase.NewContext().AddStoredProcedure("usp_DeleteContent")
                .AddParameter("Id", id, DbType.Int32)
                .ExecuteNonquery();
        }
    }
}