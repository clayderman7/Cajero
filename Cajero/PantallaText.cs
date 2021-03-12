using System;
using System.IO;
using System.Threading;

namespace Cajero
{
    class PantallaText : Cajero
    {

        private readonly ConsoleColor yellow;
        private readonly string pathScreenMenu;
        private readonly string screenOptions;
        private readonly string screenFrame;
        private readonly string frameScreenPath;

        public PantallaText()
        {
            frameScreenPath = "MarcoPantalla.txt";
            pathScreenMenu = "MenuOptions.txt";
            screenFrame = File.ReadAllText(frameScreenPath);
            screenOptions = File.ReadAllText(pathScreenMenu);
            yellow = ConsoleColor.Yellow;
            Console.Write("Iniciando pantalla!...");
        }

        public void SettingsConsole()
        {
            Console.CursorSize = 10;
            Console.SetWindowSize(80, 20);
            Console.ForegroundColor = yellow;
            Console.Title = "Bank Console";
        }

        public void PrintScreenFrame()
        {
            Console.Clear();
            Console.Write(screenFrame);
        }

        public void PrintTextIdUser()
        {
            Console.SetCursorPosition(27, 1);
            Console.Write("Ingrese Identificación");
            Console.SetCursorPosition(30, 7);
            Console.Write("ID: __________ ");
            Console.SetCursorPosition(34, 7);

        }

        public void PrintWrongUser()
        {
            Console.SetCursorPosition(4, 12);
            Console.Write("ID no registrado!");
            Console.SetCursorPosition(4, 14);
            Console.Write("Reintento en: ");

            for (int i = 3; i >= 0; i--)
            {
                Console.SetCursorPosition(18, 14);
                Console.Write(i);
                Thread.Sleep(999);
            }
        }

        public void PrintTextInputPassword()
        {
            Console.SetCursorPosition(25, 1);
            Console.Write("Ingreso de Contraseña");
            Console.SetCursorPosition(31, 5);
            Console.Write("Contraseña: ");
            Console.SetCursorPosition(33, 8);
            Console.Write("[____]");
            Console.SetCursorPosition(34, 8);
        }

        public void PrintWrongPassword(int intent)
        {
            Console.SetCursorPosition(5, 12);
            Console.Write("Contraseña incorrecta.  Intentos restantes {0}.\n\n\t\tPresione [ Enter ] para Continuar.", intent);
            Console.ReadKey();
        }

        public void PrintTextMenu()
        {
            Console.Clear();
            Console.Write(screenOptions);
            Console.SetCursorPosition(48, 15);
        }

        public void PrintExitMassage()
        {
            Console.Clear();
            Console.Write(screenFrame);
            Console.SetCursorPosition(19, 8);
            Console.Write("Gracias por usar nuestros servicios");
            Console.SetCursorPosition(2, 18);
            Console.ReadKey();
        }

        public void PrintSelected(ConsoleKeyInfo selectKey)
        {
            switch (selectKey.Key)
            {
                case ConsoleKey.D1:

                    Console.Clear();
                    Console.Write(screenFrame);
                    Console.SetCursorPosition(33, 1);
                    Console.Write("Retiro");
                    Console.SetCursorPosition(27, 5);
                    Console.Write("Ingrese el monto:");
                    Console.SetCursorPosition(33, 7);
                    Console.Write("[_____]");
                    Console.SetCursorPosition(34, 7);
                    break;

                case ConsoleKey.D2:

                    Console.Clear();
                    Console.Write(screenFrame);
                    Console.SetCursorPosition(31, 1);
                    Console.Write("Deposito");
                    Console.SetCursorPosition(21, 4);
                    Console.Write("Ingrese el monto a Depositar.");
                    Console.SetCursorPosition(33, 7);
                    Console.Write("[_____]");
                    Console.SetCursorPosition(34, 7);
                    break;

                case ConsoleKey.D3:

                    Console.Clear();
                    Console.Write(screenFrame);
                    Console.SetCursorPosition(28, 1);
                    Console.Write("Consulta de saldo");
                    break;

                case ConsoleKey.D4:

                    Console.Clear();
                    Console.Write(screenFrame);
                    Console.SetCursorPosition(25, 1);
                    Console.Write("Consulta de movimientos");
                    Console.SetCursorPosition(4, 4);
                    break;

                case ConsoleKey.D5:

                    Console.Clear();
                    Console.Write(screenFrame);
                    Console.SetCursorPosition(25, 1);
                    Console.Write("Cambio de Contraseña");
                    Console.SetCursorPosition(22, 5);
                    Console.Write("Ingrese una nueva contraseña");
                    Console.SetCursorPosition(33, 8);
                    Console.Write("[_____]");
                    Console.SetCursorPosition(34, 8);
                    break;

            }

        }

    }
}
