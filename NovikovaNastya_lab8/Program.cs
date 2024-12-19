using System;


namespace NovikovaNastya_lab8
{
    /// <summary>
    /// Виды банковского счета
    /// </summary>
    enum AccountType
    {
        Saving,  //Сберегательный
        Deposit, //Депозитный
        Currency //Валютный
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
        }

        //В классе банковский счет, созданном в предыдущих упражнениях, удалить методы заполнения полей. Вместо этих методов создать конструкторы.
        //Переопределить конструктор по умолчанию, создать конструктор для заполнения поля баланс, конструктор для заполнения поля тип банковского счета, конструктор для заполнения баланса и типа банковского счета. Каждый конструктор должен вызывать метод, генерирующий номер счета.

        static void Task1()
        {
            Console.WriteLine("\nУпражнение 9.1");

            Console.WriteLine("\nПервый:");
            BankAccount FirstAccount = new BankAccount();
            FirstAccount.DisplayInfo();

            Console.WriteLine("\nВторой:");
            BankAccount SecondAccount = new BankAccount(5555);
            SecondAccount.DisplayInfo();

            Console.WriteLine("\nТретий:");
            BankAccount ThirdAccount = new BankAccount(8);
            ThirdAccount.DisplayInfo();

            FirstAccount.Refill(600.00m);
            Console.WriteLine("\nПосле пополнения:");
            FirstAccount.DisplayInfo();

            FirstAccount.WithDraw(250.00m);
            Console.WriteLine("\nПосле снятия средств:");
            FirstAccount.DisplayInfo();

            SecondAccount.Refill(400.00m);
            Console.WriteLine("\nПосле пополнения:");
            SecondAccount.DisplayInfo();

            SecondAccount.WithDraw(100.50m);
            Console.WriteLine("\nПосле снятия средств:");
            SecondAccount.DisplayInfo();

            ThirdAccount.Refill(750.00m);
            Console.WriteLine("\nПосле пополнения:");
            ThirdAccount.DisplayInfo();

            ThirdAccount.WithDraw(700.00m);
            Console.WriteLine("\nПосле снятия средств:");
            ThirdAccount.DisplayInfo();

            FirstAccount.Transfer(FirstAccount, SecondAccount, 90.0m);
            Console.WriteLine("\nПосле перевода:");
            FirstAccount.DisplayInfo();
            SecondAccount.DisplayInfo();

            SecondAccount.Transfer(SecondAccount, ThirdAccount, 320.0m);
            Console.WriteLine("\nПосле перевода:");
            SecondAccount.DisplayInfo();
            ThirdAccount.DisplayInfo();

            Console.ReadLine();

        }

        //Создать новый класс BankTransaction, который будет хранить информацию о всех банковских операциях. При изменении баланса счета создается новый объект класса BankTransaction,
        //который содержит текущую дату и время, добавленную или снятую со счета сумму. Поля класса должны быть только для чтения (readonly). Конструктору класса передается один параметр – сумма.
        //В классе банковский счет добавить закрытое поле типа System.Collections.Queue, которое будет хранить объекты класса BankTransaction для данного банковского счета; изменить методы снятия со счета и добавления на счет так, чтобы в них
        //создавался объект класса BankTransaction и каждый объект добавлялся в переменную типа System.Collections.Queue.
        static void Task2()
        {
            Console.WriteLine("\nУпражнение 9.2");

            BankAccount account = new BankAccount(2000,5);
            account.Refill(200);
            account.WithDraw(400);
            account.Refill(700);

            account.DisplayInfo();  
           

            Console.ReadLine();
        }

        //В классе банковский счет создать метод Dispose, который данные о проводках из очереди запишет в файл. Не забудьте внутри метода Dispose вызвать метод GC.SuppressFinalize, который сообщает системе,
        //что она не должна вызывать метод завершения для указанного объекта.
        static void Task3()
        {
            Console.WriteLine("\nУпражнение 9.3");

            BankAccount myAccount = new BankAccount(10000.00m, (int)AccountType.Saving);
            Console.WriteLine("Первый счет: ");
            myAccount.DisplayInfo();
            myAccount.Refill(5000.00m);
            myAccount.WithDraw(2000.00m);

            Console.WriteLine("\nВторой счет: ");
            BankAccount fromAccount = new BankAccount(500.00m, (int)AccountType.Currency);
            fromAccount.DisplayInfo();
            myAccount.Transfer(fromAccount, myAccount, 100.00m);

            Console.WriteLine("\nПосле изменения:");
            Console.WriteLine("Первый счет: ");
            myAccount.DisplayInfo();

            Console.WriteLine("\nВторой счет: ");
            fromAccount.DisplayInfo();

            Console.WriteLine("\nИстория операций: ");
            myAccount.DisplayTransactionHistory();
            myAccount.Dispose();

            Console.ReadLine();
        }

        static void Task4()
        {
            Console.WriteLine("\nДомашнее задание 9.1");

            Song song1 = new Song();
            song1.SetName("Someone like you");
            song1.SetAuthor("Adele");

            Song song2 = new Song();
            song2.SetName("7 rings");
            song2.SetAuthor("Ariana Grande");
            song2.SetPrev(song1);

            Song song3 = new Song();
            song3.SetName("Rumour has it");
            song3.SetAuthor("Adele");
            song3.SetPrev(song2);

            Song song4 = new Song();
            song4.SetName("Thunder");
            song4.SetAuthor("Imagine Dragons");
            song4.SetPrev(song3);

            Song[] songs = { song1, song2, song3, song4 };

            foreach (Song song in songs)
            {
                song.PrintInfo();
            }

            if (song1.Equals(song2))
            {
                Console.WriteLine($"{song1.Title()} и {song2.Title()} одинаковы.");
            }
            else
            {
                Console.WriteLine($"{song1.Title()} и {song2.Title()} различаются.");
            }
            Console.ReadLine();
        }

    }
    
}
