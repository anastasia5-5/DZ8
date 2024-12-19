using System;
using System.Collections;
using System.IO;

namespace NovikovaNastya_lab8
{
    internal class BankAccount : IDisposable
    {
        /// <summary>
        /// Свойства
        /// </summary>
        public ulong AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public AccountType AccountType { get; private set; }


        /// <summary>
        /// Закрытое поле типа System.Collections.Queue
        /// </summary>
        private readonly Queue transactions = new Queue();

        /// <summary>
        /// Флаг для отслеживания вызова метода Dispose
        /// </summary>
        private bool disposed = false;


        /// <summary>
        /// Конструктор по умолчанию 
        /// </summary>
        public BankAccount()
        {
            AccountNumber = GenerateAccountNumber();
            Balance = 0;
            AccountType = 0;
        }

        /// <summary>
        /// Конструктор для заполнения балансса
        /// </summary>
        /// <param name="balance"></param>
        public BankAccount(decimal balance)
        {
            AccountNumber = GenerateAccountNumber();
            Balance = balance;
            AccountType = 0;
        }

        /// <summary>
        /// Конструктор для заполнения типа банковского счета
        /// </summary>
        /// <param name="accountType"></param>
        public BankAccount(int accountType)
        {
            AccountNumber = GenerateAccountNumber();
            Balance = 0;
            AccountType = AccountType;
        }

        /// <summary>
        /// Конструктор для заполнения баланса и типа банковского счета
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="accountType"></param>
        public BankAccount(decimal balance, int accountType)
        {
            AccountNumber = GenerateAccountNumber();
            Balance = balance;
            AccountType = AccountType;
        }

        /// <summary>
        /// Метод для генерации номера счета
        /// </summary>
        /// <returns></returns>
        private ulong GenerateAccountNumber()
        {
            Random random = new Random();
            return (ulong)random.Next(100000000, 999999999);
        }

        /// <summary>
        /// Метод для пополнения счета
        /// </summary>
        /// <param name="amount"></param>
        public void Refill(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма пополнения должно быть больше 0");
            }
            Balance += amount;
            var transaction = new BankTransaction(amount);
            transactions.Enqueue(transaction);
        }

        /// <summary>
        /// Метод для снятия средств со счета
        /// </summary>
        /// <param name="amount"></param>
        public void WithDraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма снятия должна быть больше 0");   
            }
            
            if(amount > Balance)
            {
                throw new InvalidOperationException("Недостаточно средств или неверная сумма для снятия");
            }

            Balance -= amount;
            var transaction = new BankTransaction(-amount);
        }

        /// <summary>
        /// Метод отображения информации о счете
        /// </summary>
        public void DisplayInfo()
        {
            Console.WriteLine($"Номер счета:{AccountNumber}");
            Console.WriteLine($"Баланс:{Balance}");
            Console.WriteLine($"Тип банковского счета:{GetAccountTypeName((int)AccountType)}");
            Console.WriteLine("\nИстория транзакций:");
            foreach( BankTransaction transaction in transactions )
            {
                Console.WriteLine(transaction.ToString());
            }
        }

        /// <summary>
        /// Метод преобразования типа счета в строку
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns></returns>
        public string GetAccountTypeName(int accountType)
        {
            switch (accountType)
            {
                case 1:
                    return "Savings";
                case 2:
                    return "Deposit";
                case 3:
                    return "Currency";
                default:
                    return "Underfined";
            }
        }

        //Метод для вывода истории транзакции
        public void DisplayTransactionHistory()
        {
            Console.WriteLine($"История операций для счета {AccountNumber}:");
            foreach ( var transaction in transactions )
            {
                Console.WriteLine(transaction);
            }
        }
        /// <summary>
        /// Реализация метода Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);// метод завершения вызывать не нужно
        }

        /// <summary>
        /// Перегруженный метод Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    SaveTransactionsToFile();
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Запись транзакций в файл
        /// </summary>
        private void SaveTransactionsToFile()
        {

            string filePath = $"..\\..\\{AccountNumber}transaction_history.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    foreach (BankTransaction transaction in transactions)
                    {
                        writer.WriteLine(transaction.ToString());
                    }
                }
                Console.WriteLine($"Данные о проводках записаны в файл: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не получилось сохранить в файл: {ex.Message}");
            }
        }
        ~BankAccount()
        {
            Dispose(false);
        }
       


        /// <summary>
        /// Метод который переводит деньги с одного счета на другой 
        /// <param name="fromAccount"></param>
        /// <param name="toAccount"></param>
       /// <param name="amount"></param>
       public void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
       {
           if (amount <= 0)
           {
               Console.WriteLine("Сумма перевода должна быть больше 0");
               return;
           }
           if (amount > Balance)
           {
               Console.WriteLine("Недостаточно средств на счете");
               return;
           }
           Balance -= amount;
           WithDraw(amount);
           fromAccount.Refill(amount);

           Console.WriteLine($"Переведено {amount} со счета {AccountNumber} на счет {fromAccount.GetType()}");
       }
        
    }
}

    

