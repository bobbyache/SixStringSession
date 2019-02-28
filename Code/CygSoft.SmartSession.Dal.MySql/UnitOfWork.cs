using CygSoft.SmartSession.Dal.MySql.Common;
using CygSoft.SmartSession.Dal.MySql.Repositories;
using CygSoft.SmartSession.Domain;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace CygSoft.SmartSession.Dal.MySql
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection connection;
        private IDbTransaction transaction;

        private bool _disposed;

        #region IUnitOfWork

        private IExerciseRepository exercises;
        public IExerciseRepository Exercises { get { return exercises ?? (exercises = new ExerciseRepository(transaction)); } }

        private IPracticeRoutineRepository practiceRoutines;
        public IPracticeRoutineRepository PracticeRoutines { get { return practiceRoutines ?? (practiceRoutines = new PracticeRoutineRepository(transaction)); } }

        public int Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new DatabaseCommitException("Failed to commit to the database", ex);
            }
            finally
            {
                transaction.Dispose();
                transaction = connection.BeginTransaction();
                ResetRepositories();
            }
            return 0;
        }

        public void Rollback()
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw new DatabaseCommitException("Failed to commit to the database", ex);
            }
            finally
            {
                transaction.Dispose();
                transaction = connection.BeginTransaction();
                ResetRepositories();
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
            connection = new MySqlConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
        }

        private void ResetRepositories()
        {
            exercises = null;
            practiceRoutines = null;
        }

        public void Dispose()
        {
            DoDispose(true);
            GC.SuppressFinalize(this);
        }

        private void DoDispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }
                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            DoDispose(false);
        }
    }
}
