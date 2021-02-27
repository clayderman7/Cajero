using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.IO;


namespace Cajero
{
    class Program
    {
        static readonly string pathPass = @"C:\Users\Los Ortegas\source\repos\Cajero\Folder pruebaFile\PwStore";
        const string
            menu = "Bienvenido al Cajero",
            option1 = "Retirar",
            option2 = "Consultar Saldo",
            option3 = "Consultar Movimientos",
            option4 = "Salir";
        static int intentos = 3;
        static bool check = false;
        private static readonly Cajero usuario = new Cajero();

        static void Main(string[] args)
        {

            Console.CursorSize = 10;
            Console.SetBufferSize(72, 18);
            Console.SetWindowSize(80, 20);
            Console.Title = "Bank Thief International";
            File.WriteAllText(pathPass, "2315");

            do
            {
                Console.Clear();
                Console.WriteLine("\n*******Ingrese los 4 digitos de su contraseña*******\n");

                check = usuario.VerificacionDeContraseña(Console.ReadLine());
                if (check) break;

                intentos--;
                Console.WriteLine("Contraseña incorrecta.  Intentos restantes {0}.\nPresione Enter para Reintentar.", intentos);
                Console.ReadKey();

            } while (intentos != 0);

            switch (intentos)
            {
                case 0:
                    Console.WriteLine("\nHa excedido el numero de intentos."); break;
                default:
                    Menu();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n\t¡Que tenga un buen dia!");
                    Console.ReadKey();
                    break;
            }

        }

        static void Menu()
        {
            ConsoleKeyInfo ckey;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                // Console.WriteLine("\n***{0}***\n\n1.- {1}\n\n2.- {2}\n\n3.- {3}\n\n4.- {4}\n ", menu, option1, option2, option3, option4);
                string path = @"C:\Users\Los Ortegas\source\repos\Cajero\Folder pruebaFile\PruebaFile.txt";
                string blog = File.ReadAllText(path);
                Console.WriteLine(blog);

                //Console.Write("Seleccione una opcion: [_] ");
                Console.SetCursorPosition(32, 16);
                ckey = Console.ReadKey(true);

                if (ckey.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nIngrese el monto de retiro.");
                    try
                    {
                        usuario.Withdraw(double.Parse(Console.ReadLine()));
                        Console.ReadKey();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Dato invalido");
                        Console.ReadKey();
                    }
                }
                else if (ckey.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nIngrese el monto de Deposito. Min( 1$ ) Max( $500 )\n");
                    try
                    {
                        usuario.Deposit(double.Parse(Console.ReadLine()));
                        Console.ReadKey();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Dato invalido");
                        Console.ReadKey();
                    }

                }
                else if (ckey.Key == ConsoleKey.D3)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nSu saldo es: {0:c}\n", usuario.GetSaldo());
                    Console.ReadKey();
                }
                else if (ckey.Key == ConsoleKey.D4)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    usuario.ConsultMoving();
                    Console.ReadKey();
                }
                else if (ckey.Key == ConsoleKey.D6)
                {
                    Console.Clear();
                    Console.ResetColor();
                    break;
                }

            } while (ckey.Key != ConsoleKey.D4);

        }
    }

}