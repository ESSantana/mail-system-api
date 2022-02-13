using System;

namespace MailSystem.API.Middleware.Entities
{
    /// <summary>
    /// Class that represents all that will be save in redis
    /// </summary>
    public class CacheContext
    {
        /// <summary>
        /// Values from request
        /// </summary>
        public RequestCache Request { get; set; }
        /// <summary>
        /// Values from response
        /// </summary>
        public ResponseCache Response { get; set; }
        /// <summary>
        /// Date when request was performed
        /// </summary>
        public DateTime PerformedAt { get; set; }
    }
}
