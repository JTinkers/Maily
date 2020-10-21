namespace Maily.API.Schema.Users.Objects
{
    /// <summary>
    /// Proxy containing fields required in process signing up the <see cref="User"/>.
    /// </summary>
    public class UserSignUpPayload
    {
        public int Id { get; set; }

        public string Nickname { get; set; }

        public string Token { get; set; }
    }
}
