using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {
        //账户类
        public class Account
        {
            //字段
            public double money;
            //属性
            private string id;
            public string identity
            {
                get
                {
                    return id;
                }
                set
                {
                    id = value;
                }
            }
            private string pwd;
            public string password
            {
                get
                {
                    return pwd;
                }
                set
                {
                    pwd = value;
                }
            }

            //构造方法
            public Account(string id, string pwd, double money)
            {
                this.id = id;
                this.money = money;
                this.pwd = pwd;
            }
            //方法：获取钱数
            public double getmoney()
            {
                return money;
            }
            //改变钱数
            public void setmoney(double val)
            {
                this.money = val;
            }

            //存钱
            public bool savemoney(double money)
            {
                if (money < 0)
                    return false;
                else
                {
                    this.money += money;
                    return true;
                }
            }
            //取钱
            public virtual bool withdrawmoney(double money)
            {
                if (money <= this.money)
                {
                    this.money -= money;
                    return true;
                }
                else
                    return false;

            }
            //抛出异常
            public void test(double money)
            {
                Random r = new Random();
                if (money <= this.money && r.Next(3) < 1)
                    throw (new BadCashException("bad cash found,stop transaction"));
            }

            //验证账户密码
            public bool ismatch(string id, string pwd)
            {
                return (id == this.id && pwd == this.pwd);
            }
        }
        //银行类
        public class Bank
        {
            //将账户类型对象存入集合,可索引访问
            List<Good> accounts = new List<Good>();
            //开户
            public Good openaccount(string id, string pwd, double money, double credit)
            {
                Good account = new Good(id, pwd, money, credit);
                accounts.Add(account);
                return account;
            }
            //销户
            public bool closeaccount(Good account)
            {
                int idx = accounts.IndexOf(account);
                if (idx < 0)
                    return false;
                accounts.Remove(account);
                return true;
            }
            //查户
            public Good findaccount(string id, string pwd)
            {
                foreach (Good account in accounts)
                {
                    if (account.ismatch(id, pwd))
                        return account;
                }
                return null;
            }
        }
        //ATM类
        public class ATM
        {
            Bank bank;
            public double m;
            public int flag = 1;
            private int value;
            //构造方法
            public ATM(Bank bank)
            {
                this.bank = bank;
                int n = 1;
                SetValue(n);
            }
            //打印
            public void show(string msg)
            {
                Console.WriteLine(msg);
            }
            //输入
            public string get()
            {
                return Console.ReadLine();
            }
            //使用事件及委托
            //一大笔钱被取走了
            //取款数大于10000，则可以激活这个事件
            public delegate void NumManipulationHandler();
            public event NumManipulationHandler BigMoneyFetched;
            protected virtual void OnNumChanged()
            {
                if (BigMoneyFetched != null)
                {
                    BigMoneyFetched();
                }

            }

            public void SetValue(int n)
            {
                if (value != n)
                {
                    value = n;
                    OnNumChanged();
                }
            }


            //交易界面
            public void transaction()
            {

                show("please insert your card");
                string id = get();
                show("please enter your password");
                string pwd = get();
                Good account = bank.findaccount(id, pwd);
                if (account == null)
                {
                    show("card invalid or password not correct");
                    return;
                }
                show("1:display;2:save;3:withdraw");
                string op = get();
                if (op == "1")
                {
                    //余额
                    show("balance:" + account.getmoney());
                }
                else if (op == "2")
                {
                    show("save money");
                    string smoney = get();
                    double money = double.Parse(smoney);
                    bool ok = account.savemoney(money);
                    if (ok)
                        show("ok");
                    else
                        show("error");
                    show("balance:" + account.getmoney());
                    m = account.getmoney();
                }
                else if (op == "3")
                {
                    show("withdraw money");
                    string smoney = get();
                    double money = double.Parse(smoney);
                    bool ok = account.withdrawmoney(money);
                    if (ok)
                        show("ok");

                    else
                        show("error");
                    try
                    {
                        account.test(money);
                    }
                    catch (BadCashException e)
                    {
                        Console.WriteLine("BadCashException: {0}", e.Message);
                    }
                    show("balance:" + account.getmoney());
                    if (money > 1000)
                        flag = -1;
                    m = account.getmoney();
                }

            }

        }

        //信用账户子类
        public class Good : Account
        {
            //信用额度字段
            public double credit;

            //构造方法继承
            public Good(string id, string pwd, double money, double credit) : base(id, pwd, money)
            {
                this.credit = credit;
            }
            //取钱方法进行覆盖
            public override bool withdrawmoney(double money)
            {
                if (money <= credit + base.money)
                {
                    if (money > base.money)
                        credit -= (money - base.money);

                    base.money -= money;
                    return true;
                }
                else
                {
                    credit = 0;
                    return false;
                }
            }

        }
        //定义一个异常类BadCashException，表示有坏的钞票
        public class BadCashException : ApplicationException
        {
            public BadCashException(string message) : base(message)
            {
            }
        }
        //枚举低到高信用额度
        enum Credit { low = 0, middle = 50, high = 100 };
        static void Main(string[] args)
        {
            //首先选择信用额度，以区分普通账户与信用账户
            Console.WriteLine("your credit is {0}", (int)Credit.middle);
            Good account = new Good("kim", "8888", 60, (int)Credit.middle);
            Bank bank = new Bank();
            bank.openaccount(account.identity, account.password, account.money, account.credit);
            ATM atm = new ATM(bank);
            for (int i = 0; i < 5; i++)
            {
                atm.transaction();
                if (atm.flag == -1)
                {
                    //注册
                    //使用lambda表达式
                    atm.BigMoneyFetched += new ATM.NumManipulationHandler(() => {
                        Console.WriteLine("please notice:");
                        Console.WriteLine("you have withdrawn money more than 1000");
                    });
                    atm.SetValue(7);

                }
                //记录存款
                double m = atm.m;
                //增加修改账号修改密码功能
                atm.show("your account is" + " " + account.identity);
                atm.show("do you want to change id or password");
                atm.show("1:change id 2:change password 3:quit");
                string op = atm.get();
                if (op == "1")
                {
                    atm.show("input new id:");
                    account.identity = atm.get();
                    atm.show("your new information is:");
                    atm.show(account.identity + " " + account.password);
                    bank.openaccount(account.identity, account.password, m, account.credit);
                }
                else if (op == "2")
                {
                    atm.show("input new password:");
                    account.password = atm.get();
                    atm.show("your new information is:");
                    atm.show(account.identity + " " + account.password);
                    bank.openaccount(account.identity, account.password, m, account.credit);
                }


            }
        }
    }
}
