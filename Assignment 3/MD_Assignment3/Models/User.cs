using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD_Assignment3.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} -- " +
                $"Name: {Name} -- " +
                $"Age: {Age}";
        }

        public override bool Equals(object? obj)
        {
            var item = obj as User;
            if (item == null)
            {
                return false;
            }
            return this.Id.Equals(item.Id);
        }
    }
}
