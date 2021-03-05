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
        static Program()
        {
            intentos = 3;
            check = false;
            usuario = new Cajero();
            pantalla = new PantallaText();
            frameScreenPath = @"C:\Users\Los Ortegas\source\repos\Cajero\Folder pruebaFile\MarcoPantalla.txt";
        }

        private static readonly string frameScreenPath;
        private static int intentos;
        static bool check;
        private static readonly Cajero usuario;
        private static readonly PantallaText pantalla;


        static async Task Main(string[] args)
        {

            pantalla.SettingsConsole();
            
            do
            {
                Console.Clear();

                await SimpleReadAsyncWindows();

                pantalla.printTextInputPassword();

                check = usuario.VerificacionDeContraseña(Console.ReadLine());
                if (check) break;

                intentos--;
                pantalla.printWrongPassword(intentos);                

            } while (intentos != 0);

            switch (intentos)
            {
                case 0:
                    Console.WriteLine("\nHa excedido el numero de intentos."); break;
                default:
                    Menu();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("!Que tenga un buen dia!");
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

                string path = @"C:\Users\Los Ortegas\source\repos\Cajero\Folder pruebaFile\MenuOptions.txt";
                string screemOptions = File.ReadAllText(path);
                Console.WriteLine(screemOptions);

                // Situando el cursor en: Seleccione una opcion: [_]  
                Console.SetCursorPosition(48, 15);

                ckey = Console.ReadKey(true);

                if (ckey.Key == ConsoleKey.D1)
                {
                    Console.Clear();                    
                    string pahtRetiro = @"C:\Users\Los Ortegas\source\repos\Cajero\Folder pruebaFile\Option1.txt";
                    string screenWithdraw = File.ReadAllText(pahtRetiro);
                    Console.WriteLine(screenWithdraw);

                    try
                    {
                        Console.SetCursorPosition(33, 7);
                        usuario.Withdraw(double.Parse(Console.ReadLine()));
                        Console.ReadKey();
                    }
                    catch (FormatException)
                    {
                        Console.SetCursorPosition(28, 7);
                        Console.Write("Dato invalido!");
                        Console.ReadKey();
                    }
                }
                else if (ckey.Key == ConsoleKey.D2)
                {
                    Console.Clear();                    
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
                    Console.WriteLine("\nSu saldo es: {0:c}\n", usuario.GetSaldo());
                    Console.ReadKey();
                }
                else if (ckey.Key == ConsoleKey.D4)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    usuario.ConsultMoving();
                    Console.ReadKey();
                }
                else if (ckey.Key == ConsoleKey.D5)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Ingrese una nueva contraseña");
                    usuario.ChangePassword(Console.ReadLine());
                    Console.WriteLine("Cambio de contraseña exitoso.");
                    Console.ReadKey();
                }
                else if (ckey.Key == ConsoleKey.D6)
                {
                    Console.Clear();
                    Console.ResetColor();
                    break;
                }

            } while (ckey.Key != ConsoleKey.D6);

        }

        
        static async Task SimpleReadAsyncWindows()
        {
            
            string screenPw = await File.ReadAllTextAsync(frameScreenPath);
            Console.WriteLine(screenPw);
                     
        }

    }

}