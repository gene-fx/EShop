namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) :  base(message)
        {
            
        }

        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
            
        }

        public NotFoundException(List<object> keyList, object name) : base($"Entity \"{name}\" that is in the sequent list was not found\n"
                                                                                           +$"List:\n {keyList}")
        {
            
        }
    }
}
