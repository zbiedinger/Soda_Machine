﻿using System;
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
        private List<Can> _inventory;

        //Constructor (Spawner)
        public SodaMachine()
        {
            _register = new List<Coin>();
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

            for (int i = 0; i < 50; i++)
            {
                _register.Add(penny);
            }
            for (int i = 0; i < 20; i++)
            {
                _register.Add(nickel);
            }
            for (int i = 0; i < 10; i++)
            {
                _register.Add(dime);
            }
            for (int i = 0; i < 20; i++)
            {
                _register.Add(quarter);
            }
        }
        //A method to fill the sodamachines inventory with soda can objects.
        public void FillInventory()
        {
            RootBeer rootBeer = new RootBeer();
            Cola cola = new Cola();
            OrangeSoda orangeSoda = new OrangeSoda();

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
            string selectedSoda = UserInterface.SodaSelection(_inventory);
            Can selectedCan = GetSodaFromInventory(selectedSoda);
            UserInterface.DisplayCost(selectedCan);
        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)
        {
            Can selectedCan = new RootBeer();
            foreach (Can can in _inventory)
            {
                if (can.Name == nameOfSoda)
                {
                    selectedCan = can;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return selectedCan;
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
           
        }
        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        private List<Coin> GatherChange(double changeValue)
        {
            List<Coin> changeMe = new List<Coin> { };
            return changeMe;
        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(string name)
        {
            bool changeMe = true;
            return changeMe;
        }
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        private Coin GetCoinFromRegister(string name)
        {
            Coin changeMe = new Coin();
            return changeMe;
        }
        //Takes in the total payment amount and the price of can to return the change amount.
        private double DetermineChange(double totalPayment, double canPrice)
        {
            double changeMe = 0;
            return changeMe;
        }
        //Takes in a list of coins to returnt he total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)
        {
            double changeMe = 0;
            return changeMe;
        }
        //Puts a list of coins into the soda machines register.
        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
           
        }
    }
}
