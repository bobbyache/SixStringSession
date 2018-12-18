using CygSoft.SmartSession.Domain;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Goals;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Sessions;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CygSoft.SmartSession.Dal.MySql
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private bool _disposed;

        #region IUnitOfWork

        private IGoalRepository goals;
        public IGoalRepository Goals { get { return goals ?? (goals = new GoalRepository(_transaction)); } }

        private IExerciseRepository exercises;
        public IExerciseRepository Exercises { get { return exercises ?? (exercises = new ExerciseRepository(_transaction)); } }

        private IPracticeRoutineRepository practiceRoutines;
        public IPracticeRoutineRepository PracticeRoutines { get { return practiceRoutines ?? (practiceRoutines = new PracticeRoutineRepository(_transaction)); } }

        public int Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new DatabaseCommitException("Failed to commit to the database", ex);
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
            return 0;
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw new DatabaseCommitException("Failed to commit to the database", ex);
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        #endregion IUnitOfWork

        //TODO: Inject repositories into UnitOfWork in order to test interaction between UnitOfWork, service, and repository calls.

        //public UnitOfWork(string connectionString, IExerciseRepository exerciseRepository, IPracticeRoutineRepository practiceRoutineRepository, IGoalRepository goalRepository)
        //{
        //    exercises = exerciseRepository ?? throw new ArgumentNullException("ExerciseRepository must be specified.");
        //    practiceRoutines = practiceRoutineRepository ?? throw new ArgumentNullException("PracticeRoutineRepository must be specified.");
        //    goals = goalRepository ?? throw new ArgumentNullException("GoalRepository must be specified.");

        //    _connection = new MySqlConnection(connectionString);
        //    _connection.Open();
        //    _transaction = _connection.BeginTransaction();
        //}

        public UnitOfWork(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
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
