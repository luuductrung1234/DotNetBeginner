using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegateAndEvent
{
    public class Person
    {
        #region Constructors

        public Person(String name, IEnumerable<int> usedBankIds)
        {
            Name = name;
            UsedBankIds = usedBankIds;
        }

        #endregion Constructors

        #region Properties

        public string Name { get; set; }

        public int AngerLevel { get; set; } = 0;
        public int WithdrawalTimes { get; set; } = 0;

        public IEnumerable<int> UsedBankIds { get; set; }

        #endregion Properties

        #region Events

        /// <summary>
        /// This event is a delegate that never has more 
        /// than one method assigned to. Do not need "event" keyword
        /// </summary>
        public EventHandler Shout;
        public event EventHandler<int> NeedBankSupport;
        public event EventHandler<int> NeedBankSubscription;

        #endregion Events

        #region Methods

        public int WithdrawalMoney(int bankId)
        {
            if (UsedBankIds.Contains(bankId))
            {
                WithdrawalTimes++;
                if (WithdrawalTimes >= 5)
                {
                    NeedBankSupport(this, bankId);

                    // reset
                    WithdrawalTimes = 0;
                }
                return new Random().Next();
            }
            NeedBankSubscription(this, bankId);
            return default;
        }

        public void Poke()
        {
            AngerLevel++;
            if (AngerLevel >= 3 && Shout != null)
            {
                Shout(this, EventArgs.Empty);
            }
        }

        #endregion Methods
    }
}