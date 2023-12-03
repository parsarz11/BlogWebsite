namespace WeblogApp.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException():base("Item not found") { }

        public NotFoundException(string message):base(message) { }

        public NotFoundException(string message,Exception exception) : base(message,exception) { }
    }
}
