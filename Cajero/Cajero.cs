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
        private double saldo;
        static int count;
        private readonly List<string> movimientos;
        private readonly DateTime dateTime;
        readonly TimeZoneInfo timeZone;
        
        public Cajero()
        {
            
            pathPassword = @"C:\Users\Los Ortegas\source\repos\Cajero\Folder pruebaFile\PwStore.txt";
            contraseña = File.ReadAllText(pathPassword);
            timeZone = TimeZoneInfo.Local;
            movimientos = new List<string>();
            dateTime = DateTime.Now;
            saldo = 4000.00f;
            count = 1;            
        }

        public double GetSaldo()
        {
            String objList = string.Format("Consulta de Saldo. --> Fecha: {0:d}  Hora: {0:t} Zone: {1}.", dateTime, timeZone);
            movimientos.Add(objList);
            return saldo;
        }

        public void Deposit(double monto)
        {
            if (monto == 0 || monto > 500)
            {
                Console.WriteLine("\nEl monto minimo y maximo de deposito por cajero son : -Monto Min(1)\n\t\t\t\t\t-Monto Max(500)");
            }
            else
            {
                String objList = string.Format("Deposito: {2:c}. --> Fecha: {0:d}  Hora: {0:t} Zone: {1}.", dateTime, timeZone, monto);
                movimientos.Add(objList);
                this.saldo += monto;
                Console.Clear();
                Console.WriteLine($"\nDeposito completado con exito!");
            }
        }

        public void Withdraw(double saldo)
        {
            if (saldo > this.saldo)
            {
                String objList = string.Format("Intento de Retiro: {0:c}. Saldo Insuficiente. --> Fecha: {1:d}  Hora: {1:t} Zone: {2}. ", saldo, dateTime, timeZone);
                movimientos.Add(objList);
                Console.SetCursorPosition(27, 7);
                Console.Write("Saldo insuficiente!");
            }
            else if (saldo == 0)
            {
               Console.SetCursorPosition(22, 7);
               Console.Write("Monto minimo de retiro es $1");
            }
            else
            {
                String objList = string.Format("Retiro: {0:c}. --> Fecha: {1:d}  Hora: {1:t} Zone: {2}.", saldo, dateTime, timeZone);
                movimientos.Add(objList);
                this.saldo -= saldo;
                Console.SetCursorPosition(28, 7);
                Console.Write($"Retiro exitoso!");
            }
            
        }
        
        public void ChangePassword(string newPassword) => File.WriteAllText(pathPassword, newPassword);

        public void ConsultMoving() => movimientos.ForEach(moves => Console.WriteLine("\n {1}.- {0}", moves, count++));

        public bool VerificacionDeContraseña(string contraseñaIn) => contraseñaIn == contraseña;

    }
}