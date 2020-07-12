namespace NumbersSorter.Services
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using NumbersSorter.Interfaces;

    public class NumbersSortingService : INumberSortingService
    {
        private readonly ILogger<NumbersSortingService> _logger;

        public NumbersSortingService(ILogger<NumbersSortingService> logger)
        {
            _logger = logger;
        }

        public bool TrySortNumbers(float[] source, bool sortDirAsc, out float[] sortedNumbers, out string error)
        {
            error = null;
            sortedNumbers = null;

            if (source == null || !source.Any())
            {
                error = "No digits";
                return false;
            }

            try
            {
                QuickSort(source, 0, source.Length - 1, sortDirAsc);
            }
            catch (Exception e)
            {
                error = "Failed Sorting";
                _logger.LogError(error);
                return false;
            }

            sortedNumbers = source;
            return true;
        }

        private void QuickSort(float[] array, int low, int high, bool sortDirAsc)
        {
            if (low < high)
            {
                int pivot = Partition(array, low, high, sortDirAsc);

                QuickSort(array, low, pivot - 1, sortDirAsc);
                QuickSort(array, pivot + 1, high, sortDirAsc);
            }
        }

        private int Partition(float[] array, int low, int high, bool sortDirAsc)
        {
            float pivot = array[high];

            int lowIndex = (low - 1);
            for (int currentIndex = low; currentIndex < high; currentIndex++)
            {
                if ((sortDirAsc && array[currentIndex] < pivot) || (!sortDirAsc && array[currentIndex] > pivot))
                {
                    lowIndex++;

                    float temp = array[lowIndex];
                    array[lowIndex] = array[currentIndex];
                    array[currentIndex] = temp;
                }
            }

            float temp1 = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp1;

            return lowIndex + 1;
        }
    }
}