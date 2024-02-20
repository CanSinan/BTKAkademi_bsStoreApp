namespace Entities.Exceptions
{
    public abstract class NotFoundException : Exception // absract olduğu için newlenemez....
    {
        protected NotFoundException(string message) : base(message)
        {

        }
    }
}
