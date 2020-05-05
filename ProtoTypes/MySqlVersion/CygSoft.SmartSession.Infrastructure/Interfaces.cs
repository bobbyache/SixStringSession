using System;

namespace CygSoft.SmartSession.Infrastructure
{ 
    public interface IIdentityItem
    {
        int Id { get; set; }
    }
    public interface IEntity : IIdentityItem
    {
        DateTime? DateCreated { get; set; }
        DateTime? DateModified { get; set; }
    }

    public interface IWeightedEntity
    {
        int Weighting { get; }
        double PercentCompleted();
    }
}
