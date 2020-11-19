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
        public double bankAccount;
        //Constructor (Spawner)
        public Wallet()
        {
            Coins = new List<Coin>();
            FillWallet();
        }
        
        //Member Methods (Can Do)
        //Fills wallet with starting money
        private void FillWallet()
        {
            bool isPayday = UserInterface.ContinuePrompt("\n\tIs it payday? (y/n)");
            int payCheck;

            if (isPayday)
            {
                Console.WriteLine("\n$_$_$_$_$_$_$_$_$_$_$_$_$_$_$_$_$_$_$\n");
                payCheck = 15;
                bankAccount = 20;
            }
            else
            {
                payCheck = 0;
                bankAccount = 0.40;
            }
 
            Penny penny = new Penny();
            Nickel nickel = new Nickel();
            Dime dime = new Dime();
            Quarter quarter = new Quarter();

            for (int i = 0; i < 5+ payCheck; i++)
            {
                Coins.Add(penny);
            }
            for (int i = 0; i < 19+ payCheck; i++)
            {
                Coins.Add(nickel);
            }
            for (int i = 0; i < 15+ payCheck; i++)
            {
                Coins.Add(dime);
            }
            for (int i = 0; i < 10+ payCheck; i++)
            {
                Coins.Add(quarter);
            }

            double totalValue = 0;
            foreach (Coin coin in Coins)
            {
                totalValue += coin.Value;
            }
            Console.WriteLine($"You have:\n${bankAccount} in you bank account\n${totalValue} in coins in your wallet\n");
            Console.WriteLine("=====================================");
            Console.WriteLine("\tENTER to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
