using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Customer
    {
        //Member Variables (Has A)
        public Wallet Wallet;
        public Backpack Backpack;

        //Constructor (Spawner)
        public Customer()
        {
            Wallet = new Wallet();
            Backpack = new Backpack();
        }
        //Member Methods (Can Do)

        //This method will be the main logic for a customer to retrieve coins form their wallet.
        //Takes in the selected can for price reference
        //Will need to get user input for coins they would like to add.
        //When all is said and done this method will return a list of coin objects that the customer will use a payment for their soda.
        public List<Coin> GatherCoinsFromWallet(Can selectedCan)
        {
            List<Coin> customersPayment = new List<Coin>();
            double changeTotal = 0;
            string reteivedcoin = null;
            Coin tempCoin = null;

            while (changeTotal < selectedCan.Price)
            {
                reteivedcoin = UserInterface.CoinSelection(selectedCan, customersPayment);
                
                if (reteivedcoin == "Done")
                {
                    return customersPayment;
                }

                tempCoin = GetCoinFromWallet(reteivedcoin);

                if (tempCoin == null)
                {
                    Console.WriteLine($"You have no more {reteivedcoin}.");
                    Console.WriteLine("Enter to contunes getting coins.");
                    Console.ReadLine();
                }
                else
                {
                    customersPayment.Add(tempCoin);
                }
                changeTotal = 0;
                foreach (Coin coin in customersPayment)
                {
                    changeTotal += coin.Value;
                }
            }
            return customersPayment; 
        }
        //This method will be the main logic for a customer
        public double ReceivePaymentFromCard(double selectedCanprice)
        {
            
            if(Wallet.bankAccount >= selectedCanprice)
            {
                Wallet.bankAccount -= selectedCanprice;
                return selectedCanprice;
            }
            return 0;
        }

        //Returns a coin object from the wallet based on the name passed into it.
        //Returns null if no coin can be found
        public Coin GetCoinFromWallet(string coinName)
        {
            foreach (Coin coin in Wallet.Coins)
            {
                if (coin.Name == coinName)
                {
                    Wallet.Coins.Remove(coin);
                    return coin;
                }
                else { continue; }
            }
            return null;
        }
        //Takes in a list of coin objects to add into the customers wallet.
        public void AddCoinsIntoWallet(List<Coin> coinsToAdd)
        {
            //look at .AddRange()?
            foreach (Coin coinToAdded in coinsToAdd)
            {
                Wallet.Coins.Add(coinToAdded);
            }
        }
        //Takes in a double of the returned payment and adds to bankaccount.
        public void AddMoneyToCard(double returnedPayment)
        {
            Wallet.bankAccount += returnedPayment;
        }
        //Takes in a can object to add to the customers backpack.
        public void AddCanToBackpack(Can purchasedCan)
        {
            Backpack.cans.Add(purchasedCan);
            Backpack.BagRipped();
            Console.WriteLine($"\nYou have {Backpack.cans.Count} can in your backpack.");
        }
        //Takes in a can object from the customers backpack.
        public void DrinkSoda(Can can)
        {
            if (UserInterface.ContinuePrompt("\nAre you thirsty? (y/n)"))
            {
                Console.WriteLine($"\nThat's a thirst quenching {can.Name}!");
                Backpack.cans.Remove(can);
            }
        }
    }
}
