using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    public class MyProcess
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public MyProcess(string name, int id)
        {
            Name = name;
            Id = id;
        }
        public override string ToString()
        {
            return $"{Name} - {Id}";
        }
    }
}
