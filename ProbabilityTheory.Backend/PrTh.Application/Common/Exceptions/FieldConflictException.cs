namespace PrTh.Application.Common.Exceptions
{
    public class FieldConflictException : Exception
    {
        public FieldConflictException(string name, string field, object key)
            : base($"Entity \"{name}\" with value ({key}) of {field} already exist.") { }
    }
}
