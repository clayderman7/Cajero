using MySql.Data.MySqlClient;
using System;
using System.Globalization;

namespace Cajero
{
    partial class Program
    {

        private static void AccessLogicATM()
        {
            pantalla.SettingsConsole();
        Begin:
            pantalla.PrintScreenFrame();
            pantalla.PrintTextIdUser();
            InputName = Console.ReadLine().ToLower().Trim();

            if (pantalla.AuthenticationUser(InputName))
            {
                pantalla.ChargeDataUser(InputName);
                do
                {
                    pantalla.PrintScreenFrame();
                    pantalla.PrintTextInputPassword();
                    try
                    {
                        if (pantalla.AuthenticationPassword(int.Parse(Console.ReadLine())))
                            break;
                    }
                    catch (FormatException)
                    {
                        pantalla.PrintInvalidData();
                    }

                    intentos--;
                    pantalla.PrintWrongPassword(intentos);
                } while (intentos != 0);

                if (intentos.Equals(0))
                {
                    pantalla.PrintScreenFrame();
                    Console.SetCursorPosition(4, 12);
                    Console.Write("Ha excedido el numero de intentos.");
                    Console.SetCursorPosition(4, 18);
                }
                else
                {
                    MenuActions();
                    pantalla.PrintExitMassage();
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
                            double amountToWithdraw = double.Parse(Console.ReadLine());
                            if (amountToWithdraw.Equals(0))
                            {
                                Console.SetCursorPosition(22, 7);
                                Console.Write("Monto minimo de retiro es: ( $1 )");
                                Console.ReadKey();
                            }
                            else if (amountToWithdraw > pantalla.cliente.Saldo)
                            {
                                using (MySqlConnection conexion = new MySqlConnection(pantalla.ConnectionString))
                                {
                                    conexion.Open();
                                    MySqlCommand command = new MySqlCommand()
                                    {
                                        CommandTimeout = 60,
                                        Connection = conexion,
                                        CommandText = "INSERT INTO transacciones (Cuenta_Numero, Tipo_Transaccion, Monto, Fecha, Hora, Descripcion) VALUES (?Cuenta, ?TipoTransaccion, ?AmountToWithdraw, ?Date, ?Time, ?Descrip);"
                                    };
                                    command.Parameters.AddRange(new[]
                                    {
                                        new MySqlParameter("?Cuenta", pantalla.cliente.Cuenta),
                                        new MySqlParameter("?TipoTransaccion", $"Retiro"),
                                        new MySqlParameter("?AmountToWithdraw", amountToWithdraw),
                                        new MySqlParameter("?Date", pantalla.dateTime.Date),
                                        new MySqlParameter("?Time", pantalla.dateTime.TimeOfDay),
                                        new MySqlParameter("?Descrip", $"Saldo Insuficiente")
                                    });
                                    command.ExecuteNonQuery();
                                };
                                Console.SetCursorPosition(27, 7);
                                Console.Write("Saldo insuficiente!");
                                Console.ReadKey();
                            }
                            else
                            {
                                pantalla.Withdraw(amountToWithdraw);
                                Console.SetCursorPosition(28, 7);
                                Console.Write($"Retiro exitoso!");
                                Console.ReadKey();
                            }
                        }
                        catch (FormatException)
                        {
                            pantalla.PrintInvalidData();
                        }
                        break;

                    case ConsoleKey.D2:
                        pantalla.PrintSelected(ckey);
                        try
                        {
                            double amountToDeposit = double.Parse(Console.ReadLine());
                            if (amountToDeposit.Equals(0) || amountToDeposit > 500)
                            {
                                Console.SetCursorPosition(4, 11);
                                Console.Write("\n\t-Monto de deposito Min(1 $)\n\n\t-Monto de deposito Max(500 $)");
                                Console.ReadKey();
                            }
                            else
                            {
                                pantalla.Deposit(amountToDeposit);
                                Console.SetCursorPosition(22, 7);
                                Console.Write("Deposito completado con exito!");
                                Console.ReadKey();
                            }
                        }
                        catch (FormatException)
                        {
                            pantalla.PrintInvalidData();
                        }
                        break;

                    case ConsoleKey.D3:
                        pantalla.PrintSelected(ckey);
                        Console.SetCursorPosition(27, 7);
                        Console.Write("Disponble: {0}", pantalla.GetSaldo().ToString("c", new CultureInfo("en_us")));
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D4:
                        pantalla.PrintSelected(ckey);
                        pantalla.ConsultMovements();
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D5:
                    WriteAgain:
                        pantalla.PrintSelected(ckey);
                        try
                        {
                            int newPwd = int.Parse(Console.ReadLine());
                            if (newPwd > 9999 || newPwd < 1000)
                            {
                                Console.SetCursorPosition(20, 8);
                                Console.WriteLine("La contraseña debe ser de 4 digitos");
                                Console.ReadKey();
                                goto WriteAgain;
                            }
                            else
                            {
                                pantalla.ChangePassword(newPwd);
                                Console.SetCursorPosition(22, 8);
                                Console.Write("Cambio de contraseña exitoso.");
                                Console.ReadKey();
                            }
                        }
                        catch (FormatException)
                        {
                            pantalla.PrintInvalidData();
                        }
                        break;
                }

            } while (ckey.Key != ConsoleKey.D6);

        }

    }
}
