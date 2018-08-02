using CygSoft.SmartSession.Repositories.Schema;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.Interface
{
    public interface IScoreRepository
    {
        int Insert(ScoreModel obj);
        ScoreModel Select(int id);
        List<ScoreModel> SelectList();
    }
}
