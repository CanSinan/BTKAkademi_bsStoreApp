namespace Entities.Exceptions
{
    public sealed class BookNotFoundException : NotFoundException // sealed olduğu için hiç bir şekilde bir kalıtım işlemi yapılamaz.
    {
        public BookNotFoundException(int id) : base($"The book with id : {id} could not found.")
        {
        }
    }
}
