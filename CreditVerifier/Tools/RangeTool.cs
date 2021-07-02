using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditVerifier.Tools
{
    public class RangeTool
    {
        public static int FindRangeIndex(List<Range> ranges, double value)
        {
            if (ranges == null || value <= 0 || value > 100000000)
                return -1;

            if (value > ranges.Last().End)
                return ranges.Count;

            int low = 0, high = ranges.Count - 1;

            // Binary search
            while (low <= high)
            {

                // Find the mid element
                int mid = (low + high) >> 1;

                // If element is found
                if (value >= ranges[mid].Start &&
                    value <= ranges[mid].End)
                    return mid;

                // Check in first half
                else if (value < ranges[mid].Start)
                    high = mid - 1;

                // Check in second half
                else
                    low = mid + 1;
            }

            // Not found
            return -1;
        }

        public static List<Range> StartPositionsToRanges(int[] startPositions, bool hasUpperLimit = false)
        {
            if (startPositions == null)
                throw new ArgumentNullException("Start positions cannot be null");
            List<Range> ranges = new List<Range>();

            for (int i = 1; i < startPositions.Length; i++)
            {
                ranges.Add(new Range(startPositions[i - 1], startPositions[i] - 1));
            }
            return ranges;
        }
    }
}
