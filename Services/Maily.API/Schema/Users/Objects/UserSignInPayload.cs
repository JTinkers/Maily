namespace Maily.API.Schema.Users.Objects
{
    public class UserSignInPayload
    {
        public int Id { get; set; }

        public string Nickname { get; set; }

        public string Token { get; set; }
    }
}
