using Bcrypt = BCrypt.Net.BCrypt;

namespace Maily.API.Services
{
    /// <summary>
    /// Service used in hashing and verifying strings of data.
    /// </summary>
    public class Hasher
    {
        /// <summary>
        /// Create hash of an input string.
        /// </summary>
        /// <param name="input">String data to hash.</param>
        /// <returns>Hashed string data.</returns>
        public string CreateHash(string input)
        {
            var salt = Bcrypt.GenerateSalt();

            return Bcrypt.HashPassword(input, salt);
        }

        /// <summary>
        /// Compares input string data with generated hash for a match.
        /// </summary>
        /// <param name="input">Unhashed data to compare.</param>
        /// <param name="hash">Hash to compare with data.</param>
        /// <returns>Whether string data matches the hash.</returns>
        public bool IsMatching(string input, string hash)
        {
            return Bcrypt.Verify(input, hash);
        }
    }
}
