using CygSoft.SmartSession.BaseRepository.Schema;
using System.Collections.Generic;

namespace CygSoft.SmartSession.BaseRepository.Interface
{
    public interface IScoreRepository
    {
        int Insert(ScoreModel obj);
        ScoreModel Select(int id);
        List<ScoreModel> SelectList();
    }
}
