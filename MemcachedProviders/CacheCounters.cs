using System.Diagnostics;

namespace MemcachedProviders
{
    public class CacheCounters
    {
        private const string TotalOpName = "# operations executed";
        private const string OpPerSecName = "# operations / sec";
        private const string AddOpName = "# of add operations executed";
        private const string GetOpName = "# of get operations executed";
        private const string AddOpPerSecName = "# of add operations / sec";
        private const string GetOpPerSecName = "# of get operations / sec";

        private PerformanceCounter totalOperations;
        private PerformanceCounter operationsPerSecond;
        private PerformanceCounter addOperations;
        private PerformanceCounter getOperations;
        private PerformanceCounter addPerSecondOperations;
        private PerformanceCounter getPerSecondOperations;

        private const string CounterCategory = "Memcached Cache Provider";

        public CacheCounters()
        {
            Initialise();
        }

        public void IncrementTotalOperPc()
        {
            totalOperations?.Increment();
            operationsPerSecond?.Increment();
        }

        public void IncrementAddOperPc()
        {
            addOperations?.Increment();
            addPerSecondOperations?.Increment();
        }

        public void IncrementGetOperPc()
        {
            getOperations?.Increment();
            getPerSecondOperations?.Increment();
        }

        public static void Create()
        {
            var counters = new CounterCreationDataCollection();

            var totalOps = new CounterCreationData
            {
                CounterName = TotalOpName,
                CounterHelp = "Total number of operations executed",
                CounterType = PerformanceCounterType.NumberOfItems32
            };
            counters.Add(totalOps);

            var opsPerSecond = new CounterCreationData
            {
                CounterName = OpPerSecName,
                CounterHelp = "Number of operations executed per second",
                CounterType = PerformanceCounterType.RateOfCountsPerSecond32
            };
            counters.Add(opsPerSecond);

            var addOps = new CounterCreationData
            {
                CounterName = AddOpName,
                CounterHelp = "Number of add operations execution",
                CounterType = PerformanceCounterType.NumberOfItems32
            };
            counters.Add(addOps);

            var addOpsPerSec = new CounterCreationData
            {
                CounterName = AddOpPerSecName,
                CounterHelp = "Number of add operations per second",
                CounterType = PerformanceCounterType.RateOfCountsPerSecond32
            };
            counters.Add(addOpsPerSec);

            var getOps = new CounterCreationData
            {
                CounterName = GetOpName,
                CounterHelp = "Number of get operations execution",
                CounterType = PerformanceCounterType.NumberOfItems32
            };
            counters.Add(getOps);

            var getOpsPerSec = new CounterCreationData
            {
                CounterName = GetOpPerSecName,
                CounterHelp = "Number of get operations per second",
                CounterType = PerformanceCounterType.RateOfCountsPerSecond32
            };
            counters.Add(getOpsPerSec);

            // create new category with the counters above
            PerformanceCounterCategory.Create(CounterCategory,
                "Memcached Cache Provider Performance Counter",
                PerformanceCounterCategoryType.SingleInstance, counters);
        }

        public static void Remove()
        {
            if (!PerformanceCounterCategory.Exists(CounterCategory)) return;

            PerformanceCounterCategory.Delete(CounterCategory);
        }

        private void Initialise()
        {
            if (!PerformanceCounterCategory.Exists(CounterCategory)) return;

            // create counters to work with
            totalOperations = new PerformanceCounter
            {
                CategoryName = CounterCategory,
                CounterName = TotalOpName,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };

            operationsPerSecond = new PerformanceCounter
            {
                CategoryName = CounterCategory,
                CounterName = OpPerSecName,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };

            addOperations = new PerformanceCounter
            {
                CategoryName = CounterCategory,
                CounterName = AddOpName,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };

            addPerSecondOperations = new PerformanceCounter
            {
                CategoryName = CounterCategory,
                CounterName = AddOpPerSecName,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };

            getOperations = new PerformanceCounter
            {
                CategoryName = CounterCategory,
                CounterName = GetOpName,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };

            getPerSecondOperations = new PerformanceCounter
            {
                CategoryName = CounterCategory,
                CounterName = GetOpPerSecName,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };
        }
    }
}