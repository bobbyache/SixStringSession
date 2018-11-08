using System.Collections.Generic;
using System.Data;
using System.Linq;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using Dapper;

namespace CygSoft.SmartSession.Dal.MySql
{
    internal class ExerciseRepository : RepositoryBase, IExerciseRepository
    {
        public ExerciseRepository(IDbTransaction transaction) : base(transaction) { }

        public void Add(Exercise entity)
        {
            entity.Id = Connection.ExecuteScalar<int>(sql: "sp_InsertExercise", 
                //param: new { }, 
                param: entity,
                commandType: CommandType.StoredProcedure
                );

            //string sql =
            //    "INSERT INTO Exercise ( " +
            //    "	Title, " +
            //    "	DifficultyRating, " +
            //    "	PracticalityRating, " +
            //    "	TargetMetronomeSpeed, " +
            //    "	TargetPracticeTime, " +
            //    "	PercentageCompleteCalculationType, " +
            //    "	DateCreated, " +
            //    "	DateModified " +
            //    "	) " +
            //    "VALUES ( " +
            //    $"	'{entity.Title}', " +
            //    $"	{entity.DifficultyRating}, " +
            //    $"	{entity.PracticalityRating}, " +
            //    $"	{entity.TargetMetronomeSpeed}, " +
            //    $"	{entity.TargetPracticeTime}, " +
            //    $"	{(int)entity.PercentageCompleteCalculationType}, " +
            //    $"	'{entity.DateCreated.ToString("yyyy-MM-ddTHH:mm:ss")}', " +
            //    $"	'{entity.DateModified.ToString("yyyy-MM-ddTHH:mm:ss")}' " +
            //    "	); ";

            //entity.Id = Connection.ExecuteScalar<int>(sql);
        }

        public void AddRange(IEnumerable<Exercise> entities)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<Exercise> Find(Specification<Exercise> specification, string[] keywords, int page = 0, int pageSize = 100)
        {
            return Connection.Query<Exercise>("SELECT * FROM Exercise;").ToList();
        }

        public IReadOnlyList<Exercise> Find(Specification<Exercise> specification, int page = 0, int pageSize = 100)
        {
            return Connection.Query<Exercise>("SELECT * FROM Exercise;").ToList();
        }

        public IReadOnlyList<Exercise> Find(object criteria)
        {
            throw new System.NotImplementedException();
        }

        public Exercise Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Exercise entity)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Exercise> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Exercise entity)
        {
            throw new System.NotImplementedException();
        }
    }
}