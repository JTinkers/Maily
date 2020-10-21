namespace Maily.API.Schema.Users
{
    /// <summary>
    /// Error code describing failure in process of signing in.
    /// </summary>
    public enum UserSignInErrorCode : short
    {
        UserNotFound,
        PasswordMismatch
    }
}
