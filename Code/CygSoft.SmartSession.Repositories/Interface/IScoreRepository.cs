using CygSoft.SmartSession.Repositories.Schema;using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.Interface
{
    public interface IScoreRepository
    {
        int Insert(ScoreRecord obj);
        ScoreRecord Select(int id);
        List<ScoreRecord> SelectList();
    }
}
