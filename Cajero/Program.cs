using System;
using System.Threading.Tasks;

namespace Cajero
{
    class Program
    {
        static Program()
        {
            intentos = 3;
            pantalla = new PantallaText();
        }

        static readonly PantallaText pantalla;
        static int intentos;

        static void Main(string[] args)
        {

            pantalla.SettingsConsole();

        Begin:

            pantalla.PrintScreenFrame();
            pantalla.PrintTextIdUser();
            string InputID = Console.ReadLine().ToLower().Trim();

            if (pantalla.AuthenticationUser(InputID))
            {
                do
                {
                    pantalla.PrintScreenFrame();
                    pantalla.PrintTextInputPassword();

                    try
                    {
                        if (pantalla.AuthenticationPassword(InputID, int.Parse(Console.ReadLine())))
                            break;
                    }
                    catch (FormatException)
                    {
                        Console.SetCursorPosition(30, 8);
                        Console.Write("Dato invalido!");
                    }

                    intentos--;
                    pantalla.PrintWrongPassword(intentos);

                } while (intentos != 0);

                switch (intentos)
                {
                    case 0:
                        pantalla.PrintScreenFrame();
                        Console.SetCursorPosition(4, 12);
                        Console.Write("Ha excedido el numero de intentos.");
                        Console.SetCursorPosition(4, 18);
                        break;

                    default:
                        MenuActions();
                        pantalla.PrintExitMassage();
                        break;
                }
            }
            else
            {
                pantalla.PrintWrongUser();
                goto Begin;
            }
        }

        private static void MenuActions()
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
                        pantalla.ConsultMovements();
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