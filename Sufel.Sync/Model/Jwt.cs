using System;

namespace Sufel.Sync.Model
{
    /// <summary>
    /// Jwt Class.
    /// </summary>
    public class Jwt
    {
        /// <summary>
        /// Gets or sets the expire.
        /// </summary>
        /// <value>The expire.</value>
        public DateTime Expire { get; set; }
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public string Token { get; set; }
    }
}
