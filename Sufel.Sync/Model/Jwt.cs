using System;

namespace Sufel.Sync.Model
{
    /// <summary>
    /// Jwt Class.
    /// </summary>
    public class Jwt
    {
        public DateTime Expire { get; set; }
        public string Token { get; set; }
    }
}
