using System;

namespace CygSoft.SmartSession.Application.Infrastructure
{
    public interface ISession
    {
        string Title { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
        string Id { get; }
    }
}