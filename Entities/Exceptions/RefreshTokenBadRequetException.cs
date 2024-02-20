namespace Entities.Exceptions
{
    public class RefreshTokenBadRequetException : BadRequestException
    {
        public RefreshTokenBadRequetException() 
            : base($"Invalid client request. The tokenDto has some invalid values.")
        {
        }
    }
}
