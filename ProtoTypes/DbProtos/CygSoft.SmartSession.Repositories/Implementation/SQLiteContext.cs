using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace CygSoft.SmartSession.Repositories.Implementation
{
    public class SQLiteContext : IDisposable
    {
        private readonly SQLiteConnection _dbConn;
        private string _connectionString = "";

        public SQLiteContext()
        {
            _connectionString = "Data Source=BunnyDb.db;Version=3;";
            _dbConn = new SQLiteConnection(_connectionString);

            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        protected virtual void Delete(string sql, object parameters = null)
        {
            Open();
            _dbConn.Execute(sql, parameters);
        }

        protected virtual int Insert<T>(string sql, object poco)
        {
            Open();
            return _dbConn.ExecuteScalar<int>(sql, (T)poco);
        }

        protected virtual void InsertBulk<T>(string sql, object listPoco)
        {
            Open();
            using (SQLiteTransaction trans = _dbConn.BeginTransaction())
            {
                _dbConn.Execute(sql, listPoco, transaction: trans);
                trans.Commit();
            }
        }

        protected virtual T Select<T>(string sql, object parameters = null) where T : new()
        {
            Open();
            var o = _dbConn.Query<T>(sql, parameters).FirstOrDefault();
            if (o != null)
                return o;

            return new T();
        }

        protected virtual List<T> SelectList<T>(string sql, object parameters = null)
        {
            Open();
            return _dbConn.Query<T>(sql, parameters).ToList();
        }

        protected virtual void Update<T>(string sql, object poco)
        {
            Open();
            _dbConn.Execute(sql, (T)poco);
        }

        protected virtual void ExecuteNonQuery(string sql)
        {
            Open();
            using (SQLiteCommand command = new SQLiteCommand(sql, _dbConn))
            {
                command.ExecuteNonQuery();
            }
        }

        #region helpers
        public void Dispose()
        {
            _dbConn.Close();
            _dbConn.Dispose();
        }

        protected void Open()
        {
            if (_dbConn.State == ConnectionState.Closed)
                _dbConn.Open();
        }
        #endregion
    }
}
