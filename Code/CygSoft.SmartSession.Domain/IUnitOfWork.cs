using CygSoft.SmartSession.Domain.Exercises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IExerciseRepository Exercises { get; }
        int Complete();
    }
}
