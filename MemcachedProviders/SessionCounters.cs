using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemcachedProviders
{
    public class SessionCounters
    {
        private const string TotalOperName = "# operations executed";
        private const string OperPerSecName = "# operations / sec";
        private const string MemcachedOp = "# of memcached operations executed";
        private const string MemcachedOpPerSec = "# of memcached operations / sec";
        private const string DbOp = "# of database operations executed";
        private const string DbOpPerSec = "# of database operations / sec";

        private PerformanceCounter totalOperations;
        private PerformanceCounter operationsPerSecond;
        private PerformanceCounter memcachedOperations;
        private PerformanceCounter memcachedOperPerSec;
        private PerformanceCounter dbOperations;
        private PerformanceCounter dbOperationsPerSec;

        private const string CounterCategory = "Memcached Session Provider";
        
        public SessionCounters()
        {
            Initialise();
        }
        
        public void IncrementTotalOperPc()
        {
            this.totalOperations?.Increment();
            this.operationsPerSecond?.Increment();
        }

        public void IncrementMemcachedPc()
        {
            this.memcachedOperations?.Increment();
            this.memcachedOperPerSec?.Increment();
        }

        public void IncrementDbPc()
        {
            this.dbOperations?.Increment();
            this.dbOperationsPerSec?.Increment();
        }

        public static void Create()
        {
            var counters = new CounterCreationDataCollection();

            var totalOps = new CounterCreationData
            {
                CounterName = TotalOperName,
                CounterHelp = "Total number of operations executed",
                CounterType = PerformanceCounterType.NumberOfItems32
            };
            counters.Add(totalOps);

            var opsPerSecond = new CounterCreationData
            {
                CounterName = OperPerSecName,
                CounterHelp = "Number of operations executed per second",
                CounterType = PerformanceCounterType.RateOfCountsPerSecond32
            };
            counters.Add(opsPerSecond);

            var memcachedOps = new CounterCreationData
            {
                CounterName = MemcachedOp,
                CounterHelp = "Number of Memcached operations execution",
                CounterType = PerformanceCounterType.NumberOfItems32
            };
            counters.Add(memcachedOps);

            var memcachedOpsPerSec = new CounterCreationData
            {
                CounterName = MemcachedOpPerSec,
                CounterHelp = "Number of Memcached operations per second",
                CounterType = PerformanceCounterType.RateOfCountsPerSecond32
            };
            counters.Add(memcachedOpsPerSec);

            var dbOps = new CounterCreationData
            {
                CounterName = DbOp,
                CounterHelp = "Number of database operations execution",
                CounterType = PerformanceCounterType.NumberOfItems32
            };
            counters.Add(dbOps);

            var dbOpsPerSec = new CounterCreationData
            {
                CounterName = DbOpPerSec,
                CounterHelp = "Number of database operations execution",
                CounterType = PerformanceCounterType.RateOfCountsPerSecond32
            };
            counters.Add(dbOpsPerSec);

            PerformanceCounterCategory.Create(CounterCategory,
                        "Memcached Session Provider Performance Counter",
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

            totalOperations = new PerformanceCounter
            {
                CategoryName = SessionCounters.CounterCategory,
                CounterName = TotalOperName,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };

            operationsPerSecond = new PerformanceCounter
            {
                CategoryName = SessionCounters.CounterCategory,
                CounterName = OperPerSecName,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };

            memcachedOperations = new PerformanceCounter
            {
                CategoryName = SessionCounters.CounterCategory,
                CounterName = MemcachedOp,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };

            memcachedOperPerSec = new PerformanceCounter
            {
                CategoryName = SessionCounters.CounterCategory,
                CounterName = MemcachedOpPerSec,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };

            dbOperations = new PerformanceCounter
            {
                CategoryName = SessionCounters.CounterCategory,
                CounterName = DbOp,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };

            dbOperationsPerSec = new PerformanceCounter
            {
                CategoryName = SessionCounters.CounterCategory,
                CounterName = DbOpPerSec,
                MachineName = ".",
                ReadOnly = false,
                RawValue = 0
            };
        }
    }
}
