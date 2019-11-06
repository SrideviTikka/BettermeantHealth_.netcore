using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using BettermeantHealth.DataContract;

namespace BettermeantHealth.DAL
{
    public class DatabaseHelper
    {
        private string strConnectionString;
        private NpgsqlConnection objConnection;
        private NpgsqlCommand objCommand;
        //  private DbProviderFactory objFactory = null;
        private bool boolHandleErrors;
        private string strLastError;
        private bool boolLogError;
        private string strLogFile;        
        private NpgsqlFactory objFactory = null;      


        public DatabaseHelper()
        {
            var connString = DC_StaticConstants.StrConnectionString;            
            strConnectionString = connString;
            objFactory = NpgsqlFactory.Instance;
            objConnection = (NpgsqlConnection)objFactory.CreateConnection();
            objCommand = (NpgsqlCommand)objFactory.CreateCommand();

            objConnection.ConnectionString = strConnectionString;
            objCommand.Connection = objConnection;
            //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CommandTimeout"]))
            //{
            //    int CommandTimeout = 0;
            //    int.TryParse(ConfigurationManager.AppSettings["CommandTimeout"].ToString(), out CommandTimeout);
            //    if (CommandTimeout > 0)
            //    {
            //        objCommand.CommandTimeout = CommandTimeout;
            //    }
            //}
        }

        public bool HandleErrors
        {
            get
            {
                return boolHandleErrors;
            }
            set
            {
                boolHandleErrors = value;
            }
        }

        public string LastError
        {
            get
            {
                return strLastError;
            }
        }

        public bool LogErrors
        {
            get
            {
                return boolLogError;
            }
            set
            {
                boolLogError = value;
            }
        }

        public string LogFile
        {
            get
            {
                return strLogFile;
            }
            set
            {
                strLogFile = value;
            }
        }

        public int AddParameter(string name, object value)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
           
            p.Value = value;
            return objCommand.Parameters.Add(p);
        }

        public int AddParameter(string name, object value, ParameterDirection direction)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            p.Direction = direction;
            return objCommand.Parameters.Add(p);
        }

        public int AddParameter(string name, object value, ParameterDirection direction, DbType dbType, int intSize)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            p.Direction = direction;
            p.Size = intSize;
            p.DbType = dbType;
            return objCommand.Parameters.Add(p);
        }

        public int AddParameter(DbParameter parameter)
        {
            return objCommand.Parameters.Add(parameter);
        }

        public DbCommand Command
        {
            get
            {
                return objCommand;
            }
        }

        public void BeginTransaction()
        {
            if (objConnection.State == System.Data.ConnectionState.Closed)
            {
                objConnection.Open();
            }
            objCommand.Transaction = objConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            objCommand.Transaction.Commit();
            objConnection.Close();
        }

        public void RollbackTransaction()
        {
            objCommand.Transaction.Rollback();
            objConnection.Close();
        }

        public int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, CommandType.Text, ConnectionState.CloseOnExit);
        }

        public int ExecuteNonQuery(string query, CommandType commandtype)
        {
            return ExecuteNonQuery(query, commandtype, ConnectionState.CloseOnExit);
        }

        public int ExecuteNonQuery(string query, ConnectionState connectionstate)
        {
            return ExecuteNonQuery(query, CommandType.Text, connectionstate);
        }

        public int ExecuteNonQuery(string query, CommandType commandtype, ConnectionState connectionstate)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            int i = -1;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open)
                {
                    objConnection.Close();
                }
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = objCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    objConnection.Close();
                }
            }

            return i;
        }

        public object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, CommandType.Text, ConnectionState.CloseOnExit);
        }

        public object ExecuteScalar(string query, CommandType commandtype)
        {
            return ExecuteScalar(query, commandtype, ConnectionState.CloseOnExit);
        }

        public object ExecuteScalar(string query, ConnectionState connectionstate)
        {
            return ExecuteScalar(query, CommandType.Text, connectionstate);
        }

        public object ExecuteScalar(string query, CommandType commandtype, ConnectionState connectionstate)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            object o = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                o = objCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    objConnection.Close();
                }
            }

            return o;
        }

        public DbDataReader ExecuteReader(string query)
        {
            return ExecuteReader(query, CommandType.Text, ConnectionState.CloseOnExit);
        }

        public DbDataReader ExecuteReader(string query, CommandType commandtype)
        {
            return ExecuteReader(query, commandtype, ConnectionState.CloseOnExit);
        }

        public DbDataReader ExecuteReader(string query, ConnectionState connectionstate)
        {
            return ExecuteReader(query, CommandType.Text, connectionstate);
        }

        public DbDataReader ExecuteReader(string query, CommandType commandtype, ConnectionState connectionstate)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            DbDataReader reader = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
                }
                else
                {
                    reader = objCommand.ExecuteReader();
                }

            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
            }

            return reader;
        }

        public DataSet ExecuteDataSet(string query)
        {
            return ExecuteDataSet(query, CommandType.Text, ConnectionState.CloseOnExit);
        }

        public DataSet ExecuteDataSet(string query, CommandType commandtype)
        {
            return ExecuteDataSet(query, commandtype, ConnectionState.CloseOnExit);
        }

        public DataSet ExecuteDataSet(string query, ConnectionState connectionstate)
        {
            return ExecuteDataSet(query, CommandType.Text, connectionstate);
        }

        public DataSet ExecuteDataSet(string query, CommandType commandtype, ConnectionState connectionstate)
        {
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            adapter.SelectCommand = objCommand;
            DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                objCommand.Parameters.Clear();
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    if (objConnection.State == System.Data.ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                }
            }
            return ds;
        }

        private void HandleExceptions(Exception ex)
        {
            if (LogErrors)
            {
                WriteToLog(ex.Message);
            }
            if (HandleErrors)
            {
                strLastError = ex.Message;
            }
            else
            {
                throw ex;
            }
        }

        private void WriteToLog(string msg)
        {
            StreamWriter writer = File.AppendText(LogFile);
            writer.WriteLine(DateTime.Now.ToString() + " - " + msg);
            writer.Close();
        }

        public void Dispose()
        {
            objConnection.Close();
            objConnection.Dispose();
            objCommand.Dispose();
        }

        ~DatabaseHelper()
        {
            this.Dispose();
        }
    }
    public enum ConnectionState
    {
        KeepOpen, CloseOnExit
    }
}
