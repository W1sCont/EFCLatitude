using System;
using System.Collections.Generic;
using System.Text;

namespace SemaphoreTest
{
    public class MyThreadItem
    {
        private static int _idCounter = 0;
        public int Id { get; set; }
        public int Counter { get; set; }
        public ManualResetEvent IsRunning { get; set; }
        
        public MyThreadItem()
        {
            Id = Interlocked.Increment(ref _idCounter);
            Counter = 0 ;
            IsRunning = new ManualResetEvent(false);
        }
        public override string ToString()
        {
            return $"Таск {Id} - {Counter}";
        }
    }
}
