using System;

namespace Cajero
{
    class Program
    {
        static Program()
        {
            intentos = 3;
            check = false;
            pantalla = new PantallaText();

        }

        private const ConsoleColor yellow = ConsoleColor.Yellow;
        private static readonly PantallaText pantalla;
        private static int intentos;
        static bool check;


        static void Main(string[] args)
        {

            pantalla.SettingsConsole();

            do
            {
                pantalla.PrintScreenFrame();
                pantalla.PrintTextInputPassword();

                check = pantalla.VerificacionDeContraseña(Console.ReadLine());
                if (check) break;

                intentos--;
                pantalla.PrintWrongPassword(intentos);

            } while (intentos != 0);

            switch (intentos)
            {
                case 0:
                    Console.ForegroundColor = yellow;
                    Console.SetCursorPosition(4, 18);
                    Console.WriteLine("Ha excedido el numero de intentos."); break;

                default:
                    MenuAction();
                    pantalla.PrintExitMassage();
                    break;
            }

        }

        static void MenuAction()
        {
            ConsoleKeyInfo ckey;

            do
            {
                pantalla.PrintTextMenu();

                ckey = Console.ReadKey(true);

                switch (ckey.Key)
                {
                    case ConsoleKey.D1:

                        pantalla.PrintSelected(ckey);
                        try
                        {
                            pantalla.Withdraw(double.Parse(Console.ReadLine()));
                            Console.ReadKey();
                        }
                        catch (FormatException)
                        {
                            Console.SetCursorPosition(29, 7);
                            Console.Write("Dato invalido!");
                            Console.ReadKey();
                        }
                        break;

                    case ConsoleKey.D2:

                        pantalla.PrintSelected(ckey);
                        try
                        {
                            pantalla.Deposit(double.Parse(Console.ReadLine()));
                            Console.ReadKey();
                        }
                        catch (FormatException)
                        {
                            Console.SetCursorPosition(29, 7);
                            Console.Write("Dato invalido");
                            Console.ReadKey();
                        }
                        break;

                    case ConsoleKey.D3:

                        pantalla.PrintSelected(ckey);
                        pantalla.GetSaldo();
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D4:

                        pantalla.PrintSelected(ckey);
                        pantalla.ConsultMoving();
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D5:

                        pantalla.PrintSelected(ckey);
                        pantalla.ChangePassword(Console.ReadLine());
                        Console.SetCursorPosition(22, 8);
                        Console.Write("Cambio de contraseña exitoso.");
                        Console.ReadKey();
                        break;

                }

            } while (ckey.Key != ConsoleKey.D6);

        }




    }

}