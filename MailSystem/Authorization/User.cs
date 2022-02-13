namespace MailSystem.Authorization
{
    /// <summary>
    /// Class that represent a response of an authorization request
    /// </summary>
    public class User
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User role
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Authentication token
        /// </summary>
        public string Token { get; set; }
    }
}
