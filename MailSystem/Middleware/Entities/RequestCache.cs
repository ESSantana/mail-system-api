using System.Collections.Generic;
using System.IO;

namespace MailSystem.API.Middleware.Entities
{
    /// <summary>
    /// Class that represents request
    /// </summary>
    public class RequestCache
    {

        /// <summary>
        /// Method used 'GET', 'POST', 'PUT', 'DELETE'
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// API path
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Values from body if exists
        /// </summary>
        public Stream Body { get; set; }
        /// <summary>
        /// Header properties
        /// </summary>
        public List<Header> Headers { get; set; }
        
#nullable enable
        /// <summary>
        /// Values from route if exists
        /// </summary>
        public List<object?> RouteValues { get; set; } = default!;
#nullable restore
        /// <summary>
        /// Values from query string if exists
        /// </summary>
        public string QueryString { get; set; }
    }
}
