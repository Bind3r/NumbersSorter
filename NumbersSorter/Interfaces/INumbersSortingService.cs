namespace NumbersSorter.Interfaces
{
    public interface INumberSortingService
    {
        bool TrySortNumbers(float[] source, bool sortDirAsc, out float[] sortedNumbers, out string error);
    }
}
