using Bcrypt = BCrypt.Net.BCrypt;

namespace Maily.API.Services
{
    public class Hasher
    {
        public string CreateHash(string input)
        {
            var salt = Bcrypt.GenerateSalt();

            return Bcrypt.HashPassword(input, salt);
        }

        public bool IsMatching(string input, string hash)
        {
            return Bcrypt.Verify(input, hash);
        }
    }
}
