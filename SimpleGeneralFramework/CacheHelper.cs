using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGeneralFramework
{
    public class CacheHelper
    {
        public static T GetOrSetCache<T>(string cacheKey, SqlCommand command, bool isDependency, Func<T> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                CacheItemPolicy policy = new CacheItemPolicy();
                
                //If the AbsoluteExpirationTime isn't set or is 0, the SlidingExpirationTime will be enabled by default.
                var absoluteExpirationTime = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("AbsoluteExpirationTime")) ? 0 : int.Parse(ConfigurationManager.AppSettings.Get("AbsoluteExpirationTime"));
                if (absoluteExpirationTime > 0)
                    policy.AbsoluteExpiration = DateTimeOffset.Now.Add(new TimeSpan(0, absoluteExpirationTime, 0));
                else
                {
                    var slidingExpirationTime = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("SlidingExpirationTime")) ? 10 : int.Parse(ConfigurationManager.AppSettings.Get("SlidingExpirationTime"));
                    policy.SlidingExpiration = new TimeSpan(0, slidingExpirationTime, 0);
                }

                if (isDependency)
                {                                  
                    try
                    {
                        var connString = DABase.GetConnectionString();
                        using (var connection = new SqlConnection(connString))
                        {
                            connection.Open();
                            SqlDependency.Start(connString);
                            command.Connection = connection;                            
                            var dependency = new SqlDependency(command);
                            SqlChangeMonitor monitor = new SqlChangeMonitor(dependency);
                            policy.ChangeMonitors.Add(monitor);
                            DataTable dt = new DataTable();
                            var reader = command.ExecuteReader();
                            dt.Load(reader);
                            reader.Close();
                            MemoryCache.Default.Add(cacheKey, item, policy);
                        }
                    }
                    catch(Exception ex)
                    {
                        throw;
                    }
                }                
            }
            return item;
        }
        public DataTable GetCache(string cacheKey, string connectionString, string cmdText)
        {
            ObjectCache cache = MemoryCache.Default;
            var result = cache.Get(cacheKey) as DataTable;
            if(result == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
                SqlDependency.Start(connectionString);
                using(var connection=new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(cmdText, connection);
                    var depency = new SqlDependency(command);
                    SqlChangeMonitor monitor = new SqlChangeMonitor(depency);
                    policy.ChangeMonitors.Add(monitor);                    

                    connection.Open();
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        result.Load(reader);
                    }
    
                    if (result != null)
                        cache.Add(cacheKey, result, policy);
                }
            }
            return result;
        }
    }
}
