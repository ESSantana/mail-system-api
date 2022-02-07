using System;

namespace MailSystem.API.Middleware.Entities
{
    public class CacheContext
    {
        public RequestCache Request { get; set; }
        public ResponseCache Response { get; set; }
        public DateTime PerformedAt { get; set; }
    }
}
