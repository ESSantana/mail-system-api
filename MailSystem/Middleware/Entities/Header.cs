namespace MailSystem.API.Middleware.Entities
{
    /// <summary>
    /// Class that respresents header property
    /// </summary>
    public class Header
    {
        /// <summary>
        /// Property name    
        /// </summary>
        public string Key { get; private set; }
        /// <summary>
        /// Property value    
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Property name</param>
        /// <param name="value">Property value</param>
        public Header(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
