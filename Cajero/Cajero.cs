using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.IO;


namespace Cajero
{
    class Cajero
    {
        private readonly string pathPassword;
        private readonly string contraseña;
        private string recordMoving;
        private double saldo;
        static int count;
        private readonly List<string> movimientos;
        private readonly DateTime dateTime;
        
        
        public Cajero()
        {
            
            pathPassword = @"C:\Users\Los Ortegas\source\repos\Cajero\Folder pruebaFile\PwStore.txt";
            contraseña = File.ReadAllText(pathPassword);            
            movimientos = new List<string>();
            recordMoving = null;
            dateTime = DateTime.Now;
            saldo = 4000.00f;
            count = 1;            
        }

        public void GetSaldo()
        {
            recordMoving = string.Format("Consulta de Saldo. --> Fecha: {0:d} Hora: {0:t}.", dateTime);
            movimientos.Add(recordMoving);
            Console.SetCursorPosition(30, 7);
            Console.Write("Su saldo es: {0:c}", saldo);
        }

        public void Deposit(double monto)
        {
            if (monto == 0 || monto > 500)
            {
                Console.SetCursorPosition(4, 11);
                Console.Write("El monto minimo y maximo de deposito por cajero son : \n\t\t\t\t\t-Monto Min(1)\n\t\t\t\t\t-Monto Max(500)");
            }
            else
            {
                recordMoving = string.Format("Deposito: {2:c}. --> Fecha: {0:d} Hora: {0:t}.", dateTime, monto);
                movimientos.Add(recordMoving);
                this.saldo += monto;
                Console.SetCursorPosition(22, 7);
                Console.Write("Deposito completado con exito!");
            }
        }

        public void Withdraw(double saldo)
        {
            if (saldo > this.saldo)
            {
                recordMoving = string.Format("Intento de Retiro: {0:c}. Saldo Insuficiente. --> Fecha: {1:d} Hora: {1:t}.", saldo, dateTime);
                movimientos.Add(recordMoving);
                Console.SetCursorPosition(27, 7);   Console.Write("Saldo insuficiente!");
            }
            else if (saldo == 0)
            {
               Console.SetCursorPosition(23, 7);    Console.Write("Monto minimo de retiro es $1");
            }
            else
            {
                recordMoving = string.Format("Retiro: {0:c}. --> Fecha: {1:d}  Hora: {1:t}.", saldo, dateTime);
                movimientos.Add(recordMoving);
                this.saldo -= saldo;
                Console.SetCursorPosition(29, 7);   Console.Write($"Retiro exitoso!");
            }
            
        }
        
        public void ChangePassword(string newPassword) => File.WriteAllText(pathPassword, newPassword);

        public void ConsultMoving() => movimientos.ForEach(moves => Console.WriteLine("{1}.- {0}\n\t", moves, count++));

        public bool VerificacionDeContraseña(string contraseñaIn) => contraseñaIn == contraseña;

    }
}