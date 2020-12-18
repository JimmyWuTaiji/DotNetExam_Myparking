using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyParking.DTO;
using MyParking.DL;

namespace MyParking.BL
{
    public class ContentsBL
    {
        ContentDL contentDL = new ContentDL();
        public bool InsertUpdateContents(ContentsDTO content)
        {
            return contentDL.InsertUpdateContent(content) > 0;
        }
        public bool DeleteContents(int id)
        {
            return contentDL.DeleteContentByID(id) > 0;
        }
        public List<ContentsDTO> GetAllContents()
        {
            return contentDL.SearchAllContents();
        }
        public ContentsDTO GetContentsByID(int id)
        {
            return contentDL.SearchContentByID(id);
        }
    }
}
