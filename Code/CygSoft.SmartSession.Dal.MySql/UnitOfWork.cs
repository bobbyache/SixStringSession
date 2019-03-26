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
        public static IDbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        private IDbConnection connection;
        private IDbTransaction transaction;
        private bool _disposed;

        public IExerciseRepository Exercises { get; private set; }
        public IPracticeRoutineRepository PracticeRoutines { get; private set; }

        public UnitOfWork(IDbConnection connection, IExerciseRepository exerciseRepository, IPracticeRoutineRepository practiceRoutineRepository)
        {
            this.connection = connection ?? throw new ArgumentNullException("A closed IDbConnection must be provided.");
            Exercises = exerciseRepository ?? throw new ArgumentNullException("ExerciseRepository must be specified.");
            PracticeRoutines = practiceRoutineRepository ?? throw new ArgumentNullException("PracticeRoutineRepository must be specified.");

            connection.Open();
            transaction = connection.BeginTransaction();
        }

        ~UnitOfWork()
        {
            DoDispose(false);
        }

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
            }
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
    }
}
