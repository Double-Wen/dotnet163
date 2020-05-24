using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3
{
    class CreditCardAccount:Account
    {
        private double creditMoney;

        private bool creditEnabled;

        public double CreditMoney { get => creditMoney; set => creditMoney = value; }
        public bool CreditEnabled { get => creditEnabled; set => creditEnabled = value; }
        
        public void disableCredit()
        {
            this.CreditEnabled = false;
            Console.WriteLine(this.CreditEnabled);
            return;
        }

        public void enableCredit()
        {
            this.CreditEnabled = true;
            Console.WriteLine(this.CreditEnabled);
            return;
        }

        public bool WithdrawMoney(double money)
        {
            Console.WriteLine("你是信用卡，不能取钱");
            return false;

        }

    }
}
