﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OysterCard
{
    public class Oyster
    {
        private double balance;
        private double maxLimit = 90.00;
        private bool inJourney = false;
        public Oyster(double _balance)
        {
            balance = _balance;
        }

        public double oysterBalance { get => balance; set => balance = value; }
        public double maxOysterLimit { get => maxLimit; set => maxLimit = value; }
        public bool inOysterJourney { get => inJourney; set => inJourney = value; }

        public double TopUp(double amount)
        {
            if ((oysterBalance+amount) > maxOysterLimit)
            {
                throw new Exception($"The maximum balance is {maxOysterLimit} GBP");
            }
                oysterBalance += amount;
                return oysterBalance;
        }

        public double Deduct(double amount)
        {
            oysterBalance -= amount;
            return oysterBalance;
        }

        public bool TouchIn()
        {
            if (oysterBalance < 1.00)
            {
                throw new Exception("You don't have the minimum balance of 1.00 GBP");
            }
            inOysterJourney = true;
            return inOysterJourney;
        }

        public bool TouchOut()
        {
            inOysterJourney = false;
            return inOysterJourney;
        }
    }

}
