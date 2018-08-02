using CygSoft.SmartSession.DAL.Repository.Schema;
using System.Collections.Generic;

namespace CygSoft.SmartSession.DAL.Repository.Interface
{
    public interface IScoreRepository
    {
        int Insert(ScoreModel obj);
        ScoreModel Select(int id);
        List<ScoreModel> SelectList();
    }
}
