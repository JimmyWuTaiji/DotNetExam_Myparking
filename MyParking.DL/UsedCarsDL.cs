using SimpleGeneralFramework;
using System.Data;

namespace MyParking.DL
{
    public class UsedCarsDL
    {
        public string GetFirstSteetingName(string brandName)
        {
            var cmdText = "SELECT TOP 1 SettingName FROM V_AllVechicles WHERE BrandName=@BrandName";
            return DABase.NewContext().AddCommandText(cmdText)
                .AddParameter("BrandName", brandName, DbType.String)
                .ExecuteScalar<string>();
        }
    }
}
