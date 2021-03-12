using System;
using System.Collections.Generic;
using System.IO;



namespace Cajero
{
    class Cajero
    {

        private readonly string pathPassword;
        private string recordMovements;
        private double saldo;
        static int count;
        private readonly List<string> movements;
        private static SortedList<string, int> clients;
        private readonly DateTime dateTime;

        public Cajero()
        {

            pathPassword = "PwStore.txt";
            movements = new List<string>();
            recordMovements = null;
            dateTime = DateTime.Now;
            saldo = 4000.00f;
            count = 1;
            clients = new SortedList<string, int>
            {
                { "clayderman", int.Parse(File.ReadAllText(pathPassword)) },
                { "lupita"    , 1999 },
                { "arthur"    , 3242 }
            };
        }

        public void GetSaldo()
        {
            recordMovements = string.Format("Consulta de Saldo. --> Fecha: {0:d} Hora: {0:t}.", dateTime);
            movements.Add(recordMovements);
            Console.SetCursorPosition(27, 7);
            Console.Write("Disponble: {0:c}", saldo);
        }

        public void Deposit(double amountToDeposit)
        {
            if (amountToDeposit == 0 || amountToDeposit > 500)
            {
                Console.SetCursorPosition(4, 11);
                Console.Write("\n\t-Monto de deposito Min(1 $)\n\n\t-Monto de deposito Max(500 $)");
            }
            else
            {
                recordMovements = string.Format("Deposito: {1:c}. --> Fecha: {0:d} Hora: {0:t}.", dateTime, amountToDeposit);
                movements.Add(recordMovements);
                saldo += amountToDeposit;
                Console.SetCursorPosition(22, 7);
                Console.Write("Deposito completado con exito!");
            }
        }

        public void Withdraw(double amountToWithdraw)
        {
            if (amountToWithdraw > saldo)
            {
                recordMovements = string.Format("Retiro negado: {0:c}. --> Fecha: {1:d} Hora: {1:t}.", amountToWithdraw, dateTime);
                movements.Add(recordMovements);
                Console.SetCursorPosition(26, 7);
                Console.Write("Saldo insuficiente!");
            }
            else if (amountToWithdraw == 0)
            {
                Console.SetCursorPosition(22, 7);
                Console.Write("Monto minimo de retiro es: ( $1 )");
            }
            else
            {
                recordMovements = string.Format("Retiro: {0:c}. --> Fecha: {1:d}  Hora: {1:t}.", amountToWithdraw, dateTime);
                movements.Add(recordMovements);
                saldo -= amountToWithdraw;
                Console.SetCursorPosition(29, 7);
                Console.Write($"Retiro exitoso!");
            }

        }
        public void ConsultMovements()
        {
            int row = 4;
            movements.Reverse();
            foreach (string move in movements)
            {
                Console.Write("{1}.- {0}", move, count++);
                Console.SetCursorPosition(4, row += 2);
            }
        }

        public void ChangePassword(string newPassword) => File.WriteAllText(pathPassword, newPassword);

        public bool AuthenticationUser(string InputUser) => clients.ContainsKey(InputUser);

        public bool AuthenticationPassword(string keyName, int InputPassword)
        {
            foreach (var client in clients.Keys)
            {
                if (client.Contains(keyName) && clients[client] == InputPassword)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
