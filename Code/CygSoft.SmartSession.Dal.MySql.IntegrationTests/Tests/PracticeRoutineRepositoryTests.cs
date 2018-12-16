using CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests.Tests
{
    [TestFixture]
    public class PracticeRoutineRepositoryTests
    {
        [Test]
        public void PracticeRoutineRepository_Find_PracticeRoutine_With_Specific_Title_Gets_Applicable_Recs()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IPracticeRoutineSearchCriteria crit = new PracticeRoutineSearchCriteria();
                crit.Title = "monday";

                var PracticeRoutines = uow.PracticeRoutines.Find(crit);
                Assert.That(PracticeRoutines.Where(ex => ex.Title == "Monday").SingleOrDefault(), Is.Not.Null);
            }
        }

        [Test]
        public void PracticeRoutineRepository_Creates_A_New_PracticeRoutine_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            PracticeRoutine existingPracticeRoutine;

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                PracticeRoutine newPracticeRoutine = CreatePracticeRoutine();
                uow.PracticeRoutines.Add(newPracticeRoutine);
                uow.Commit();

                existingPracticeRoutine = uow.PracticeRoutines.Get(newPracticeRoutine.Id);

                uow.PracticeRoutines.Remove(newPracticeRoutine);
                uow.Commit();
            }

            Assert.IsNotNull(existingPracticeRoutine);

            Assert.That(existingPracticeRoutine.Title, Is.EqualTo("Created PracticeRoutine Title"));
            Assert.That(existingPracticeRoutine.DateCreated, Is.Not.Null);
            Assert.That(existingPracticeRoutine.DateModified, Is.Null);
        }

        [Test]
        public void PracticeRoutineRepository_Updates_An_PracticeRoutine_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            PracticeRoutine modifiedPracticeRoutine;
            var currentTime = Funcs.RemoveMilliSeconds(DateTime.Now);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                PracticeRoutine newPracticeRoutine = CreatePracticeRoutine();
                uow.PracticeRoutines.Add(newPracticeRoutine);
                uow.Commit();

                PracticeRoutine existingPracticeRoutine = uow.PracticeRoutines.Get(newPracticeRoutine.Id);

                existingPracticeRoutine.Title = "Modified PracticeRoutine Title";

                uow.PracticeRoutines.Update(existingPracticeRoutine);
                uow.Commit();

                modifiedPracticeRoutine = uow.PracticeRoutines.Get(existingPracticeRoutine.Id);

                uow.PracticeRoutines.Remove(existingPracticeRoutine);
                uow.Commit();
            }

            Assert.IsNotNull(modifiedPracticeRoutine);

            Assert.That(modifiedPracticeRoutine.Title, Is.EqualTo("Modified PracticeRoutine Title"));
            Assert.That(modifiedPracticeRoutine.DateCreated, Is.Not.Null);
            Assert.That(modifiedPracticeRoutine.DateModified, Is.Not.Null);
            Assert.That(modifiedPracticeRoutine.DateModified, Is.GreaterThanOrEqualTo(currentTime));
        }

        [Test]
        public void PracticeRoutineRepository_UnitOfWork_AddAndModify_Operates_As_Expected()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            PracticeRoutine modifiedPracticeRoutine;
            var currentTime = Funcs.RemoveMilliSeconds(DateTime.Now);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                PracticeRoutine newPracticeRoutine = CreatePracticeRoutine();
                uow.PracticeRoutines.Add(newPracticeRoutine);
                // --- do not commit !!!

                PracticeRoutine existingPracticeRoutine = uow.PracticeRoutines.Get(newPracticeRoutine.Id);

                existingPracticeRoutine.Title = "Modified PracticeRoutine Title";

                uow.PracticeRoutines.Update(existingPracticeRoutine);
                // --- do not commit !!!
                modifiedPracticeRoutine = uow.PracticeRoutines.Get(existingPracticeRoutine.Id);

                uow.Rollback();

                ActualValueDelegate<PracticeRoutine> testDelegate = () => uow.PracticeRoutines.Get(modifiedPracticeRoutine.Id);
                Assert.That(testDelegate, Throws.TypeOf<DatabaseEntityNotFoundException>());
            }
        }

        [Test]
        public void PracticeRoutineRepository_UnitOfWork_Delete_Operates_As_Expected()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            var currentTime = Funcs.RemoveMilliSeconds(DateTime.Now);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                PracticeRoutine newPracticeRoutine = CreatePracticeRoutine();
                uow.PracticeRoutines.Add(newPracticeRoutine);
                uow.Commit();

                uow.PracticeRoutines.Remove(newPracticeRoutine);
                uow.Rollback(); // don't delete.

                // fail here if the PracticeRoutine has been deleted.
                var notDeletedPracticeRoutine = uow.PracticeRoutines.Get(newPracticeRoutine.Id);
            }
        }

        [Test]
        public void PracticeRoutineRepository_Deletes_A_New_Metronome_PracticeRoutine_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var newEx = CreatePracticeRoutine();
                uow.PracticeRoutines.Add(newEx);
                uow.Commit();

                uow.PracticeRoutines.Remove(newEx);
                uow.Commit();

                ActualValueDelegate<PracticeRoutine> testDelegate = () => uow.PracticeRoutines.Get(newEx.Id);
                Assert.That(testDelegate, Throws.TypeOf<DatabaseEntityNotFoundException>());
            }
        }

        private PracticeRoutine CreatePracticeRoutine()
        {
            return new PracticeRoutine()
            {
                DateCreated = null,
                DateModified = null,
                Title = "Created PracticeRoutine Title"
            };
        }
    }
}
