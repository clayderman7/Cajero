﻿using System;
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

        private readonly List<string> movimientos = new List<string>();
        private readonly DateTime dateTime = DateTime.Now;
        readonly TimeZoneInfo timeZone = TimeZoneInfo.Local;

        public Cajero()
        {
            pathPassword = @"C:\Users\Los Ortegas\source\repos\Cajero\Folder pruebaFile\PwStore.txt";
            contraseña = File.ReadAllText(pathPassword);
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
                Console.WriteLine("\nEl monto minimo y maximo de deposito por cajero son : -Monto Min(1)\n\t\t\t-Monto Max(500)");
            }
            else
            {
                String objList = string.Format("Deposito: {2:c}. --> Fecha: {0:d}  Hora: {0:t} Zone: {1}.", dateTime, timeZone, monto);
                movimientos.Add(objList);
                this.saldo += monto;
                Console.Clear();
                Console.WriteLine($"\nHas depositado: {monto:c}");
            }
        }

        public void Withdraw(double saldo)
        {
            if (saldo > this.saldo)
            {
                if (saldo == 0) { Console.WriteLine("Monto minimo de retiro es $1"); }

                Console.WriteLine("\nSaldo insuficiente");
                String objList = string.Format("Intento de Retiro: {0:c}. Saldo Insuficiente. --> Fecha: {1:d}  Hora: {1:t} Zone: {2}. ", saldo, dateTime, timeZone);
                movimientos.Add(objList);
            }
            else
            {
                String objList = string.Format("Retiro: {0:c}. --> Fecha: {1:d}  Hora: {1:t} Zone: {2}.", saldo, dateTime, timeZone);
                movimientos.Add(objList);
                this.saldo -= saldo;
                Console.Clear();
                Console.WriteLine($"\nHas retirado: {saldo:c}");
            }
        }

        public void ChangePassword(string newPassword) => File.WriteAllText(pathPassword, newPassword);

        public void ConsultMoving() => movimientos.ForEach(moves => Console.WriteLine("\n {1}.- {0}", moves, count++));

        public bool VerificacionDeContraseña(string contraseñaIn) => contraseñaIn == contraseña;

    }
}