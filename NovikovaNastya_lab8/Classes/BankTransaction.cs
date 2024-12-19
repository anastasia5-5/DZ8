using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovikovaNastya_lab8
{
    internal class BankTransaction
    {
        /// <summary>
        /// Поле даты и времени операции
        /// </summary>
        public readonly DateTime TransactionDate;
        /// <summary>
        /// Сумма операции
        /// </summary>
        public readonly decimal Amount;

        public BankTransaction(decimal amount)
        {
            TransactionDate = DateTime.Now;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"[{TransactionDate}] Cумма: {Amount}";
        }
    }
}
