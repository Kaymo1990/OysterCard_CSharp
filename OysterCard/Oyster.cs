using System;
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
        public Oyster(double _balance)
        {
            balance = _balance;
        }

        public double oysterBalance { get => balance; set => balance = value; }
        public double maxOysterLimit { get => maxLimit; set => maxLimit = value; }

        public double TopUp(double amount)
        {
            this.oysterBalance += amount;
            return oysterBalance;
        }
    }

}
