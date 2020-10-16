namespace Maily.API.Middleware.Authorization
{
    public enum AuthorizationResult : short
    {
        NoToken,
        NoAssociatedUser
    }
}
