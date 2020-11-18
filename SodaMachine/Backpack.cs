using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Backpack
    {
        //Member Variables (Has A)
        public List<Can> cans;
        //Constructor (Spawner)
        public Backpack()
        {
            cans = new List<Can>();
        }

        //Member Methods (Can Do)

        public void BagRipped()
        {
            if(cans.Count > 6)
            {
                Console.WriteLine("\nYour bag ripped and lost all your cans of soda!");
                cans.Clear();
            }
        }
    }
}
