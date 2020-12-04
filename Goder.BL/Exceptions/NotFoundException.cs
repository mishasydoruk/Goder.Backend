namespace Goder.BL.Exceptions
{
    public class NotFoundException : BaseCustomException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string name, string id)
            : base($"Entity {name} with id ({id}) was not found.")
        {
            _httpError = 404;
        }
        public NotFoundException(string name) : base($"Entity {name} was not found.")
        {
            _httpError = 404;
        }

        public NotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}