using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGeneralFramework
{
    public class DABase
    {
        static string datasource = ConfigurationManager.AppSettings.Get("DataSource");
        static string uid = ConfigurationManager.AppSettings.Get("UserID");
        static string pwd = ConfigurationManager.AppSettings.Get("Password");        
        public static DABase NewContext()
        {
            return new DABase();            
        }
        public List<SqlParameter> Parameters { get; private set; }
        private string CommandText { get; set; }
        private CommandType CommandType { get; set; }
        private DynamicParameters DapperParameters { get; set; }
        private bool HasOutputParameter { get; set; }
        public DABase AddParameter(string name, object value = null, DbType? dbType = null, SqlDbType? sqlDbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null, string typeName = null)
        {
            if (Parameters == null)
                Parameters = new List<SqlParameter>();
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            if (dbType.HasValue)
                parameter.DbType = dbType.Value;
            if (sqlDbType.HasValue)
                parameter.SqlDbType = sqlDbType.Value;
            if (direction.HasValue)
                parameter.Direction = direction.Value;
            else
                parameter.Direction = ParameterDirection.Input;
            if (size.HasValue)
                parameter.Size = size.Value;
            if (precision.HasValue)
                parameter.Precision = precision.Value;
            if (scale.HasValue)
                parameter.Scale = scale.Value;
            if (!string.IsNullOrEmpty(typeName))
                parameter.TypeName = typeName;
            Parameters.Add(parameter);
            return this;
        }
        public DABase AddCommandText(string cmdText)
        {
            CommandText = cmdText;
            CommandType = CommandType.Text;
            return this;
        }
        public DABase AddStoredProcedure(string uspName)
        {
            CommandText = uspName;
            CommandType = CommandType.StoredProcedure;
            return this;
        }
        private void BuildDapperParameters()
        {
            if (Parameters != null && Parameters.Count > 0)
            {
                if (DapperParameters == null)
                    DapperParameters = new DynamicParameters();
                foreach (var parameter in Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        if (parameter.DbType == DbType.Decimal)
                            DapperParameters.Add(parameter.ParameterName, dbType: parameter.DbType, direction: parameter.Direction, size: parameter.Size, precision: parameter.Precision, scale: parameter.Scale);
                        else
                            DapperParameters.Add(parameter.ParameterName, dbType: parameter.DbType, size: parameter.Size, direction: parameter.Direction);
                        HasOutputParameter = true;
                    }
                    else
                    {
                        if (parameter.SqlDbType == SqlDbType.Structured && !string.IsNullOrEmpty(parameter.TypeName))
                        {
                            DapperParameters.Add(parameter.ParameterName, ((DataTable)parameter.Value).AsTableValuedParameter(parameter.TypeName), direction: parameter.Direction);
                        }
                        else
                            DapperParameters.Add(parameter.ParameterName, parameter.Value);
                    }   
                }
            }
            else
                DapperParameters = null;
        }
        private void ProcessOutputParameters()
        {
            if (HasOutputParameter)
            {
                foreach (var parameter in Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        parameter.Value = DapperParameters.Get<object>(parameter.ParameterName);
                    }
                }
            }
        }
        public int ExecuteNonquery()
        {            
            var efforCount = -1;
            HasOutputParameter = false;
            using (var connection = CreateSqlConnection())
            {
                BuildDapperParameters();
                efforCount = connection.Execute(CommandText, DapperParameters, commandType: CommandType);
                ProcessOutputParameters();
            }
            return efforCount;
        }
        public T ExecuteScalar<T>() 
        {
            var result = default(T);
            using (var connection = CreateSqlConnection())
            {
                BuildDapperParameters();
                result = connection.ExecuteScalar<T>(CommandText, DapperParameters, commandType: CommandType);
                ProcessOutputParameters();
            }
            return result;
        }
        public List<T> Query<T>()
        {
            List<T> result = new List<T>();
            using (var connection = CreateSqlConnection())
            {
                BuildDapperParameters();
                result = connection.Query<T>(CommandText, param: DapperParameters, commandType: CommandType).AsList();
                ProcessOutputParameters();
            }
            return result;
        }
        public T QueryOne<T>()
        {
            var result = default(T);
            using (var connection = CreateSqlConnection())
            {
                BuildDapperParameters();
                result = connection.QueryFirstOrDefault<T>(CommandText, param: DapperParameters, commandType: CommandType);
                ProcessOutputParameters();
            }
            return result;
        }
        public static SqlConnection CreateSqlConnection()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["partialConnectString"];
            if (null != settings)
            {
                // Retrieve the partial connection string.
                string connectString = settings.ConnectionString;

                // Create a new SqlConnectionStringBuilder based on the
                // partial connection string retrieved from the config file.
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectString);

                // Supply the additional values.
                builder.DataSource = datasource;
                builder.UserID = uid;
                builder.Password = pwd;
                return new SqlConnection(builder.ConnectionString);
            }
            return null;
        }
        public static string GetConnectionString()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["partialConnectString"];
            if (null != settings)
            {
                // Retrieve the partial connection string.
                string connectString = settings.ConnectionString;

                // Create a new SqlConnectionStringBuilder based on the
                // partial connection string retrieved from the config file.
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectString);

                // Supply the additional values.
                builder.DataSource = datasource;
                builder.UserID = uid;
                builder.Password = pwd;
                return builder.ConnectionString;
            }
            return null;
        }
    }
}
