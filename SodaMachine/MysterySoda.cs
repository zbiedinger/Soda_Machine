using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class MysterySoda : Can
    {
        //Member Variables (Has A)
        Random rand;

        //Constructor (Spawner)
        public MysterySoda()
        {
            rand = new Random();

            int selection = rand.Next(4);
            Tuple<double, string> mysterySodaType = null;
            switch (selection)
            {
                case 0:
                    mysterySodaType = Tuple.Create(0.55, "Mt. Dew");
                    break;
                case 1:
                    mysterySodaType = Tuple.Create(0.22, "Sprite");
                    break;
                case 2:
                    mysterySodaType = Tuple.Create(0.17, "Dr. Pepper");
                    break;
                case 3:
                    mysterySodaType = Tuple.Create(0.75, "Spuirt");
                    break;
            }

            Name = mysterySodaType.Item2;
            price = mysterySodaType.Item1;
        }
        //Member Methods (Can Do)
    }
}
