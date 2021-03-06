using System;
using System.IO;

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
            frameScreenPath = @"C:\Users\Los Ortegas\source\repos\Cajero\Folder pruebaFile\MarcoPantalla.txt";
            pathScreenMenu = @"C:\Users\Los Ortegas\source\repos\Cajero\Folder pruebaFile\MenuOptions.txt";
            screenFrame = File.ReadAllText(frameScreenPath);
            screenOptions = File.ReadAllText(pathScreenMenu);
            yellow = ConsoleColor.Yellow;
            Console.WriteLine("Iniciando pantalla!");

        }

        public void SettingsConsole()
        {
            Console.CursorSize = 10;
            Console.SetWindowSize(80, 20);
            Console.ForegroundColor = yellow;
            Console.Title = "Bank International Console";
        }

        public void PrintScreenFrame()
        {
            Console.Clear();
            Console.Write(screenFrame);
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
            // Situando el cursor en: Seleccione una opcion: [_]
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
