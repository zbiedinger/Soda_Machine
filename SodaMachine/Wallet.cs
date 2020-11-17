using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Wallet
    {
        //Member Variables (Has A)
        public List<Coin> Coins;
        //Constructor (Spawner)
        public Wallet()
        {
            Coins = new List<Coin>();
            FillRegister();
        }
        //Member Methods (Can Do)
        //Fills wallet with starting money
        /// <summary>
        /// Starts with $5. would like to change based on user input and if they got paid or not
        /// </summary>
        private void FillRegister()
        {
            Penny penny = new Penny();
            Nickel nickel = new Nickel();
            Dime dime = new Dime();
            Quarter quarter = new Quarter();

            for (int i = 0; i < 5; i++)
            {
                Coins.Add(penny);
            }
            for (int i = 0; i < 19; i++)
            {
                Coins.Add(nickel);
            }
            for (int i = 0; i < 15; i++)
            {
                Coins.Add(dime);
            }
            for (int i = 0; i < 10; i++)
            {
                Coins.Add(quarter);
            }
        }
    }
}
