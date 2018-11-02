using CygSoft.SmartSession.Domain;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Goals;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CygSoft.SmartSession.Dal.MySql
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private IGoalRepository goals;
        private IExerciseRepository exercises;

        private bool _disposed;

        #region IUnitOfWork

        public IGoalRepository Goals { get { return goals ?? (goals = new GoalRepository(_transaction)); } }
        public IExerciseRepository Exercises { get { return exercises ?? (exercises = new ExerciseRepository(_transaction)); } }

        public int Complete()
        {
            Commit();
            return 0;
        }

        #endregion IUnitOfWork

        public UnitOfWork(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }


        protected void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            goals = null;
            exercises = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
