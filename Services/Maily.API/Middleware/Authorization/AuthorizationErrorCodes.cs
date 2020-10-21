namespace Maily.API.Middleware.Authorization
{
    /// <summary>
    /// Error code describing failure in process of authorization.
    /// </summary>
    public enum AuthorizationErrorCodes : short
    {
        NoToken,
        NoAssociatedUser
    }
}
