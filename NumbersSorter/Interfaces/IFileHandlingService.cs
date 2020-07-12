namespace NumbersSorter.Interfaces
{
    public interface IFileHandlingService
    {
        bool TryWriteToFile(object source, out string error);
        bool TryReadFile(out string error, out string result);
    }
}