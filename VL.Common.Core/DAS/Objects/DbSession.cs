using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace VL.Common.Core.DAS
{
    /// <summary>
    /// 会话对象
    /// </summary>
    public class DbSession :  IDisposable
    {
        #region NestedTypes
        /// <summary>
        /// 数据提供器
        /// </summary>
        public EDatabaseType DatabaseType { set; get; }
        /// <summary>
        /// 是否输出文件日志
        /// </summary>
        public bool IsLogQuery { set; get; } = false;
        #endregion

        #region Parameter
        public string GetParameterPrefix()
        {
            switch (DatabaseType)
            {
                case EDatabaseType.MSSQL:
                case EDatabaseType.MySQL:
                    return "@";
                case EDatabaseType.Oracle:
                    return ":";
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion

        #region Constructors
        protected DbSession()
        {
            string connectString = new System.Configuration.AppSettingsReader().GetValue("DefaultConnectString", typeof(string)).ToString();
            this.Connection = CreateConnection(EDatabaseType.MSSQL, connectString);
        }
        //未能加载文件或程序集“Oracle.DataAccess, Version=2.111.7.20, Culture=neutral, PublicKeyToken=89b483f429c47342”或它的某一个依赖项。系统找不到指定的文件。
        //把程序目标平台设置为x86
        public DbSession(EDatabaseType databaseType, string connectString = null)
        {
            if (connectString == null)
                connectString = new System.Configuration.AppSettingsReader().GetValue("DefaultConnectString", typeof(string)).ToString();
            this.DatabaseType = databaseType;
            this.Connection = CreateConnection(databaseType, connectString);
            //this.Open();
        }
        ~DbSession()
        {
            Close();
        }
        public void Dispose()
        {
            Close();
        }
        #endregion

        #region Connection and Command
        /// <summary>
        /// 连接对象
        /// </summary>
        public DbConnection Connection { private set; get; }
        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="databaseType">数据提供程序</param> 
        private DbConnection CreateConnection(EDatabaseType databaseType, string connectionString = "")
        {
            DbConnection conn = null;
            switch (databaseType)
            {
                case EDatabaseType.MSSQL:
                    conn = new SqlConnection(connectionString);
                    break;
                case EDatabaseType.MySQL:
                    conn = new MySqlConnection(connectionString);
                    break;
                case EDatabaseType.Oracle:
                    conn = new OracleConnection(connectionString);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return conn;
        }
        /// <summary>
        /// 获取命令对象
        /// </summary>
        public DbCommand CreateCommand()
        {
            var command = Connection.CreateCommand();
            command.Transaction = this.Transaction;
            return command;
        }
        /// <summary>
        /// 获取命令对象
        /// </summary>
        public DbCommand CreateCommand(string sql)
        {
            var command = Connection.CreateCommand();
            command.Transaction = this.Transaction;
            command.CommandText = sql;
            return command;
        }
        /// <summary>
        /// 获取命令对象
        /// </summary>
        public DbCommand CreateCommand(string sqlPattern, params string[] args)
        {
            var command = Connection.CreateCommand();
            command.Transaction = this.Transaction;
            command.CommandText = string.Format(sqlPattern, args);
            return command;
        }
        /// <summary>
        /// 开启连接
        /// </summary>
        public void Open()
        {
            Connection.Open();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
        }
        #endregion

        #region Transaction
        /// <summary>
        /// 事务对象
        /// </summary>
        public DbTransaction Transaction { set; get; }
        /// <summary>
        /// 启用事务
        /// </summary>
        public void BeginTransaction()
        {
            //if (Connection.State != ConnectionState.Open)
            //    Connection.Open();
            BeginTransaction(IsolationLevel.ReadCommitted);
        }
        /// <summary>
        /// 启用事务
        /// </summary>
        public void BeginTransaction(IsolationLevel level)
        {
            //if (Connection.State != ConnectionState.Open)
            //    Connection.Open();
            Transaction = Connection.BeginTransaction(level);
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            Transaction.Commit();
            //if (Connection.State != ConnectionState.Closed)
            //    Connection.Close();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollBackTransaction()
        {
            Transaction.Rollback();
            //if (Connection.State != ConnectionState.Closed)
            //    Connection.Close();
        }
        #endregion

        #region Execute Methods
        #region ExecuteNonQuery
        public int ExecuteNonQuery(string sql)
        {
            DbCommand command = CreateCommand(sql);
            return ExecuteNonQuery(command);
        }
        public int ExecuteNonQuery(DbCommand command)
        {
            if (this.Transaction != null)
                command.Transaction = this.Transaction;
            int result = command.ExecuteNonQuery();
            return result;
        }
        #endregion

        #region ExecuteScalar
        public object ExecuteScalar(string sql)
        {
            DbCommand command = CreateCommand(sql);
            return ExecuteScalar(command);
        }
        public object ExecuteScalar(DbCommand command)
        {
            if (this.Transaction != null)
                command.Transaction = this.Transaction;
            object result = command.ExecuteScalar();
            return result;
        }
        #endregion

        #region ExecuteDataReader
        /// <summary>
        /// 数据适配对象
        /// </summary>
        public DbDataAdapter Adapter
        {
            get
            {
                if (_adapter == null)
                {
                    switch (DatabaseType)
                    {
                        case EDatabaseType.MSSQL:
                            _adapter = new SqlDataAdapter();
                            _adapter.SelectCommand = new SqlCommand();
                            break;
                        case EDatabaseType.Oracle:
                            _adapter = new OracleDataAdapter();
                            _adapter.SelectCommand = new OracleCommand();
                            break;
                        case EDatabaseType.MySQL:
                            _adapter = new MySqlDataAdapter();
                            _adapter.SelectCommand = new MySqlCommand();
                            break;
                        default:
                            throw new NotImplementedException("无该类型适配器");
                    }
                }
                return _adapter;
            }
        }
        private DbDataAdapter _adapter;

        public DbDataReader ExecuteDataReader(string sql)
        {
            DbCommand command = CreateCommand(sql);
            return ExecuteDataReader(command);
        }
        public DbDataReader ExecuteDataReader(DbCommand command)
        {
            if (this.Transaction != null)
                command.Transaction = this.Transaction;
            var result = command.ExecuteReader();
            return result;
        }
        #endregion

        #region ExecuteDataSet
        public DataSet ExecuteDataSet(string sql)
        {
            DbCommand command = CreateCommand(sql);
            return ExecuteDataSet(command);
        }
        public DataSet ExecuteDataSet(DbCommand command)
        {
            if (this.Transaction != null)
                command.Transaction = this.Transaction;
            DataSet ds = new DataSet();
            Fill(command, ds);
            return ds;
        }
        private int Fill(DbCommand command, DataSet ds)
        {
            Adapter.SelectCommand = command;
            Adapter.SelectCommand.CommandTimeout = 10;
            Adapter.SelectCommand.Connection = Connection;
            Adapter.SelectCommand.Transaction = Transaction;
            return Adapter.Fill(ds);
        }
        #endregion

        #region ExecuteReader
        public DbDataReader ExecuteReader(string sql)
        {
            DbCommand command = CreateCommand(sql);
            return ExecuteReader(command);
        }
        public DbDataReader ExecuteReader(DbCommand command)
        {
            if (this.Transaction != null)
                command.Transaction = this.Transaction;
            var result = command.ExecuteReader();
            return result;
        }
        #endregion

        #region ExecuteDataTable
        public DataTable ExecuteDataTable(string sql)
        {
            DbCommand command = CreateCommand(sql);
            return ExecuteDataTable(command);
        }
        public DataTable ExecuteDataTable(DbCommand command)
        {
            if (this.Transaction != null)
                command.Transaction = this.Transaction;
            DataTable dt = new DataTable();
            Fill(command, dt);
            return dt;
        }
        private int Fill(DbCommand command, DataTable dt)
        {
            Adapter.SelectCommand = command;
            Adapter.SelectCommand.CommandTimeout = 10;
            Adapter.SelectCommand.Connection = Connection;
            Adapter.SelectCommand.Transaction = Transaction;
            return Adapter.Fill(dt);
        }
        #endregion
        #endregion

        #region DbParameter
        public abstract class IDbParameterGenerator
        {
        }
        private IDbParameterGenerator _dbParameterGenerator { set; get; }
        /// <summary>
        /// 参数生成器
        /// </summary>
        public IDbParameterGenerator DBParameterGenerator
        {
            set { _dbParameterGenerator = value; }
            get
            {
                if (_dbParameterGenerator == null)
                {
                    _dbParameterGenerator = GetParameterGenerator(this.DatabaseType);
                }
                return _dbParameterGenerator;
            }
        }
        /// <summary>
        /// 获取参数生成器
        /// </summary>
        /// <param name="databaseType">数据提供程序</param> 
        IDbParameterGenerator GetParameterGenerator(EDatabaseType databaseType)
        {
            IDbParameterGenerator parameterGenerator = null;
            switch (databaseType)
            {
                case EDatabaseType.MSSQL:
                case EDatabaseType.Oracle:
                case EDatabaseType.MySQL:
                    break;
                default:
                    throw new NotImplementedException();
            }
            return parameterGenerator;
        }
        #endregion
    }
}
