using System.Collections.Generic;

namespace GrowingWithTheWeb.Sorting
{
    public class BucketSort : IIntegerSortingAlgorithm {
        private const int DefaultBucketSize = 5;

        public void Sort(int[] array) {
            Sort(array, DefaultBucketSize);
        }

        public void Sort(int[] array, int bucketSize) {
            if (array.Length == 0) {
                return;
            }

            // Determine minimum and maximum values
            int minValue = array[0];
            int maxValue = array[0];
            for (int i = 1; i < array.Length; i++) {
                if (array[i] < minValue) {
                    minValue = array[i];
                } else if (array[i] > maxValue) {
                    maxValue = array[i];
                }
            }

            // Initialise buckets
            int bucketCount = (maxValue - minValue) / bucketSize + 1;
            IList<List<int>> buckets = new List<List<int>>(bucketCount);
            for (int i = 0; i < bucketCount; i++) {
                buckets.Add(new List<int>());
            }

            // Distribute input array values into buckets
            for (int i = 0; i < array.Length; i++) {
                buckets[(array[i] - minValue) / bucketSize].Add(array[i]);
            }

            // Sort buckets and place back into input array
            int currentIndex = 0;
            var insertionSort = new InsertionSort<int>();
            for (int i = 0; i < buckets.Count; i++) {
                int[] bucketArray = buckets[i].ToArray();
                insertionSort.Sort(bucketArray);
                for (int j = 0; j < bucketArray.Length; j++) {
                    array[currentIndex++] = bucketArray[j];
                }
            }
        }
    }
}