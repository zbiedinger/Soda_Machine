using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        //Member Variables (Has A)
        private List<Coin> _register;
        private double _creditPayments;
        private List<Can> _inventory;


        //Constructor (Spawner)
        public SodaMachine()
        {
            _register = new List<Coin>();
            _creditPayments = 0;
            _inventory = new List<Can>();
            FillInventory();
            FillRegister();
        }


        //Member Methods (Can Do)
        //A method to fill the sodamachines register with coin objects.
        public void FillRegister()
        {
            Penny penny = new Penny();
            Nickel nickel = new Nickel();
            Dime dime = new Dime();
            Quarter quarter = new Quarter();

            for (int i = 0; i < 20; i++)
            {
                _register.Add(quarter);
            }
            for (int i = 0; i < 10; i++)
            {
                _register.Add(dime);
            }
            for (int i = 0; i < 20; i++)
            {
                _register.Add(nickel);
            }
            for (int i = 0; i < 50; i++)
            {
                _register.Add(penny);
            }
        }
        
        //A method to fill the sodamachines inventory with soda can objects.
        public void FillInventory()
        {
            RootBeer rootBeer = new RootBeer();
            Cola cola = new Cola();
            OrangeSoda orangeSoda = new OrangeSoda();
            MysterySoda flavorBlast = new MysterySoda();

            for (int i = 0; i < 5; i++)
            {
                _inventory.Add(rootBeer);
            }
            for (int i = 0; i < 7; i++)
            {
                _inventory.Add(cola);
            }
            for (int i = 0; i < 10; i++)
            {
                _inventory.Add(orangeSoda);
            }
            for (int i = 0; i < 3; i++)
            {
                _inventory.Add(flavorBlast);
            }
        }
        
        //Method to be called to start a transaction.
        //Takes in a customer which can be passed freely to which ever method needs it.
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);
            if (willProceed)
            {
                Transaction(customer);
            }
        }
        
        //This is the main transaction logic think of it like "runGame".  This is where the user will be prompted for the desired soda.
        //grab the desired soda from the inventory.
        //get payment from the user.
        //pass payment to the calculate transaction method to finish up the transaction based on the results.
        private void Transaction(Customer customer)
        {
            string selectedSodaName = UserInterface.SodaSelection(_inventory);
            Can selectedCan = GetSodaFromInventory(selectedSodaName);
            UserInterface.DisplayCost(selectedCan);

            if(UserInterface.ContinuePrompt("\nPaying by credit card? (y/n)"))
            {
                double customersPayment = customer.ReceivePaymentFromCard(selectedCan.Price);
                CalculateTransaction(customersPayment, selectedCan, customer);
            }
            else
            {
                List<Coin> customersPayment = customer.GatherCoinsFromWallet(selectedCan);
                CalculateTransaction(customersPayment, selectedCan, customer);
            }
        }
        
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)
        {
            foreach (Can can in _inventory)
            {
                if (can.Name == nameOfSoda)
                {
                    _inventory.Remove(can);
                    return can;
                }
                else
                {
                    continue;
                }
            }
            return null;
        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Dispense soda, and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Dispense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Dispense soda.
        //If the payment does not meet the cost of the soda: dispense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)
        {
            double totalPayment = TotalCoinValue(payment);

            if (totalPayment == chosenSoda.Price)
            {
                Console.WriteLine($"Here is your {chosenSoda.Name}.");
                DepositPaymentIntoRegister(payment);
                customer.AddCanToBackpack(chosenSoda);
                customer.DrinkSoda(chosenSoda);
            }
            else if(totalPayment > chosenSoda.Price)
            {
                double changeValue = DetermineChange(totalPayment, chosenSoda.Price);
                List<Coin> changeToGiveBack = GatherChange(changeValue);
                
                if (changeToGiveBack == null) 
                {
                    Console.WriteLine($"Not enough change in register. Here is your money back.");
                    _inventory.Add(chosenSoda);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"Here is your {chosenSoda.Name}.");
                    Console.WriteLine($"Here is your change: {changeValue}");
                    DepositPaymentIntoRegister(payment);
                    customer.AddCanToBackpack(chosenSoda);
                    customer.AddCoinsIntoWallet(changeToGiveBack);
                    customer.DrinkSoda(chosenSoda);
                }
            }
            else
            {
                Console.WriteLine("\nNot enough money.");
                Console.WriteLine("Here is your change\n");
                customer.AddCoinsIntoWallet(payment);
                _inventory.Add(chosenSoda);
            }
        }

        //This is the method for calculating result of a transation if a credit card it used.
        private void CalculateTransaction(double payment, Can chosenSoda, Customer customer)
        {
            if (payment == chosenSoda.Price)
            {
                Console.WriteLine($"Here is your {chosenSoda.Name}.");
                DepositPaymentIntoRegister(payment);
                customer.AddCanToBackpack(chosenSoda);
                customer.DrinkSoda(chosenSoda);
            }
            else
            {
                Console.WriteLine("\nCard declined.");
                customer.AddMoneyToCard(payment);
                _inventory.Add(chosenSoda);
            }
        }

        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        private List<Coin> GatherChange(double changeValue)
        {
            double changeInReg = TotalCoinValue(_register);
            double placeHolderChangeValue = changeValue;
            List<Coin> ChangeGivenBack = new List<Coin>();

            if (changeInReg > changeValue)
            {
                while(placeHolderChangeValue >= 0.25)
                {
                    if (RegisterHasCoin("Quarter"))
                    {
                        ChangeGivenBack.Add(GetCoinFromRegister("Quarter"));
                        placeHolderChangeValue -= 0.25;
                    }
                    else { break; }
                }
                while (placeHolderChangeValue >= 0.1)
                {
                    if (RegisterHasCoin("Dime"))
                    {
                        ChangeGivenBack.Add(GetCoinFromRegister("Dime"));
                        placeHolderChangeValue -= 0.1;
                    }
                    else { break; }
                }
                while (placeHolderChangeValue >= 0.05)
                {
                    if (RegisterHasCoin("Nickel"))
                    {
                        ChangeGivenBack.Add(GetCoinFromRegister("Nickel"));
                        placeHolderChangeValue -= 0.05;
                    }
                    else { break; }
                }
                while (placeHolderChangeValue >= 0.01)
                {
                    if (RegisterHasCoin("Penny"))
                    {
                        ChangeGivenBack.Add(GetCoinFromRegister("Penny"));
                        placeHolderChangeValue -= 0.01;
                    }
                    else { break; }
                }
                return ChangeGivenBack;
            }
            else if (changeInReg == changeValue)
            {
                _register.Clear();
                return _register;
            }
            else
            {
                return null;
            }
        }
        
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(string name)
        {
            foreach (Coin coin in _register)
            {
                if (coin.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        private Coin GetCoinFromRegister(string name)
        {
            foreach (Coin coin in _register)
            {
                if (coin.Name == name)
                {
                    _register.Remove(coin);
                    return coin;
                }
                else { continue; }
            }
            return null;
        }
        
        //Takes in the total payment amount and the price of can to return the change amount.
        private double DetermineChange(double totalPayment, double canPrice)
        {
            double changeValue = totalPayment - canPrice;
            return changeValue;
        }
        
        //Takes in a list of coins to returnt he total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)
        {
            double changeTotal = 0;
            foreach (Coin coin in payment)
            {
                changeTotal += coin.Value;
            }
            return changeTotal;
        }
        
        //Puts a list of coins into the soda machines register.
        private void DepositPaymentIntoRegister(List<Coin> coins)
        {
            foreach (Coin coin in coins)
            {
                _register.Add(coin);
            }
        }

        //Puts a card payment into the soda machine.
        private void DepositPaymentIntoRegister(double cardpayment)
        {
            _creditPayments += cardpayment;
        }
    }
}
