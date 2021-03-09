using System;
using System.Collections.Generic;
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
<<<<<<< HEAD
            pathPassword = @"C:\Users\Los Ortegas\source\repos\Cajero\FolderTestFile\PwStore.txt";
            contraseña = File.ReadAllText(pathPassword);
            movimientos = new List<string>();
            recordMoving = null;
            dateTime = DateTime.Now;
            saldo = 4000.00f;
=======
            this.pathPassword = "Folder pruebaFile\PwStore.txt";
            this.contraseña = File.ReadAllText(pathPassword);
            this.movimientos = new List<string>();
            this.recordMoving = null;
            this.dateTime = DateTime.Now;
            this.saldo = 4000.00f;
>>>>>>> faa6fe1395062b5b0f35993b6a3f1f5dbf1abfa8
            count = 1;
        }

        public void GetSaldo()
        {
            this.recordMoving = string.Format("Consulta de Saldo. --> Fecha: {0:d} Hora: {0:t}.", dateTime);
            movimientos.Add(recordMoving);
            Console.SetCursorPosition(27, 7);
            Console.Write("Disponble: {0:c}", saldo);
        }

        public void Deposit(double monto)
        {
            if (monto == 0 || monto > 500)
            {
                Console.SetCursorPosition(4, 11);
                Console.Write("\n\t-Monto de deposito Min(1 $)\n\n\t-Monto de deposito Max(500 $)");
            }
            else
            {
                this.recordMoving = string.Format("Deposito: {1:c}. --> Fecha: {0:d} Hora: {0:t}.", dateTime, monto);
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
                this.recordMoving = string.Format("Retiro negado: {0:c}. --> Fecha: {1:d} Hora: {1:t}.", saldo, dateTime);
                movimientos.Add(recordMoving);
                Console.SetCursorPosition(27, 7);
                Console.Write("Saldo insuficiente!");
            }
            else if (saldo == 0)
            {
                Console.SetCursorPosition(23, 7);
                Console.Write("Monto minimo de retiro es 1 $");
            }
            else
            {
                this.recordMoving = string.Format("Retiro: {0:c}. --> Fecha: {1:d}  Hora: {1:t}.", saldo, dateTime);
                movimientos.Add(recordMoving);
                this.saldo -= saldo;
                Console.SetCursorPosition(29, 7);
                Console.Write($"Retiro exitoso!");
            }

        }
        public void ConsultMoving()
        {
            int row = 4;
            foreach (string moves in movimientos)
            {
                Console.Write("{1}.- {0}", moves, count++);
                Console.SetCursorPosition(4, row += 2);
            }
        }

        public void ChangePassword(string newPassword) => File.WriteAllText(pathPassword, newPassword);

        public bool VerificacionDeContraseña(string contraseñaIn) => contraseñaIn == contraseña;

    }
}
