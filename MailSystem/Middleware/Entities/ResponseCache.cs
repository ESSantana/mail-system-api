using System.Collections.Generic;
using System.IO;

namespace MailSystem.API.Middleware.Entities
{
    /// <summary>
    /// Class that represents response
    /// </summary>
    public class ResponseCache
    {
        /// <summary>
        /// Content type
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// Response length
        /// </summary>
        public long? ContentLength { get; set; }
        /// <summary>
        /// Body content
        /// </summary>
        public Stream Body { get; set; }
        /// <summary>
        /// Header properties
        /// </summary>
        public List<Header> Headers { get; set; }
        /// <summary>
        /// Response status code
        /// </summary>
        public int StatusCode { get; set; }
    }
}
