using System;
using System.Collections.Generic;

namespace DelegateAndEvent
{
    class Program
    {
        delegate int DoSomething(int input);

        static void Main(string[] args)
        {
            var thomas = new Person("Thomas", new List<int> { 1, 4 });

            // simple delegate
            DoSomething doSomething = new DoSomething(thomas.WithdrawalMoney);
            var result = doSomething(1);
            System.Console.WriteLine($"Do something and get the result: {result}");

            // events
            thomas.Shout += Thomas_Shout;
            thomas.NeedBankSupport += Thomas_NeedBankSupport;
            thomas.NeedBankSubscription += Thomas_NeedBankSubscription;

            // shout event
            thomas.Poke();
            thomas.Poke();
            thomas.Poke();
            thomas.Poke();

            // need bank support event
            thomas.WithdrawalMoney(1);
            thomas.WithdrawalMoney(1);
            thomas.WithdrawalMoney(1);
            thomas.WithdrawalMoney(1);

            // need bank subscription event
            thomas.WithdrawalMoney(5);
        }

        private static void Thomas_Shout(object sender, EventArgs e)
        {
            var thomas = (Person)sender;
            System.Console.WriteLine($"{thomas.Name} has angry level {thomas.AngerLevel}. He is shouting!");
        }

        private static void Thomas_NeedBankSupport(object sender, int bankId)
        {
            var thomas = (Person)sender;
            System.Console.WriteLine($"{thomas.Name} exceed withdrawal with {thomas.WithdrawalTimes} times. He make a call for bank {bankId} for support");
        }

        private static void Thomas_NeedBankSubscription(object sender, int bankId)
        {
            var thomas = (Person)sender;
            System.Console.WriteLine($"{thomas.Name} sign for bank {bankId}");
        }
    }
}
