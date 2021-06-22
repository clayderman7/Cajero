using MySql.Data.MySqlClient;
using System;

namespace Cajero
{
    class Cajero
    {
        private int count;
        public readonly DateTime dateTime;
        public string ConnectionString { get; }
        public Cliente cliente;

        public Cajero()
        {
            cliente = new Cliente();
            dateTime = DateTime.Now;
            count = 1;
            ConnectionString = "Server=127.0.0.1;Port=3306;Database=banco_db;Uid=root;password=Mimamamemima.2020.;";
        }

        public void ChargeDataUser(string inputUser)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
                {
                    conexion.Open();
                    MySqlCommand command = new MySqlCommand()
                    {
                        CommandTimeout = 60,
                        Connection = conexion,
                        CommandText = "SELECT * FROM (clientes, cuenta) WHERE Nombre=?inputUser AND Cliente=idCliente;"
                    };
                    command.Parameters.Add(new MySqlParameter("?inputUser", inputUser));
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cliente.Id = reader.GetInt32("idCliente");
                        cliente.Name = reader.GetString("Nombre");
                        cliente.LastName = reader.GetString("Apellido");
                        cliente.Cuenta = reader.GetInt32("Cuenta_Num");
                        cliente.Saldo = reader.GetDouble("Saldo");
                        cliente.Password = reader.GetInt32("Contraseña");
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public double GetSaldo()
        {
            using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
            {
                conexion.Open();
                MySqlCommand command = new MySqlCommand()
                {
                    CommandTimeout = 60,
                    Connection = conexion,
                    CommandText = "INSERT INTO transacciones (Cuenta_Numero, Tipo_Transaccion, Monto, Fecha, Hora, Descripcion) VALUES (?Cuenta, ?TipoTransaccion, ?Saldo, ?Date, ?Time, ?Descripcion);"
                };
                command.Parameters.AddRange(new[]
                {
                    new MySqlParameter("?cuenta", cliente.Cuenta),
                    new MySqlParameter("?TipoTransaccion", $"Consulta"),
                    new MySqlParameter("?Saldo", 0),
                    new MySqlParameter("?Date", dateTime.Date),
                    new MySqlParameter("?Time", dateTime.TimeOfDay),
                    new MySqlParameter("?Descripcion", $"Consulta de Saldo")
                });
                command.ExecuteNonQuery();
                return cliente.Saldo;
            }
        }

        public void Withdraw(double amountToWithdraw)
        {
            try
            {
                cliente.Saldo -= amountToWithdraw;
                using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
                {
                    conexion.Open();
                    MySqlCommand command = new MySqlCommand()
                    {
                        CommandTimeout = 60,
                        Connection = conexion,
                        CommandText = "INSERT INTO transacciones (Cuenta_Numero, Tipo_Transaccion, Monto, Fecha, Hora, Descripcion) VALUES (?Cuenta, ?TipoTransaccion, ?AmountToWithdraw, ?Date, ?Time, ?Descripcion); UPDATE cuenta SET Saldo=?Saldo WHERE Cliente=?ID;"
                    };
                    command.Parameters.AddRange(new[]
                    {
                        new MySqlParameter("?Cuenta", cliente.Cuenta),
                        new MySqlParameter("?TipoTransaccion", $"Retiro"),
                        new MySqlParameter("?AmountToWithdraw", amountToWithdraw),
                        new MySqlParameter("?Date", dateTime.Date),
                        new MySqlParameter("?Time",dateTime.TimeOfDay),
                        new MySqlParameter("?Descripcion", $"Retiro exitoso"),
                        new MySqlParameter("?Saldo", cliente.Saldo),
                        new MySqlParameter("?ID", cliente.Id)
                    });
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public void Deposit(double amountToDeposit)
        {
            try
            {
                cliente.Saldo += amountToDeposit;
                using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
                {
                    conexion.Open();
                    MySqlCommand command = new MySqlCommand()
                    {
                        CommandTimeout = 60,
                        Connection = conexion,
                        CommandText = "INSERT INTO transacciones (Cuenta_Numero, Tipo_Transaccion, Monto, Fecha, Hora, Descripcion) VALUES (?Cuenta, ?TipoTransaccion, ?AmountToDeposit, ?Date, ?Time, ?Descripcion); UPDATE cuenta SET Saldo=?Saldo WHERE Cliente=?ID;"
                    };
                    command.Parameters.AddRange(new[]
                    {
                        new MySqlParameter("?Cuenta", cliente.Cuenta),
                        new MySqlParameter("?TipoTransaccion", $"Deposito"),
                        new MySqlParameter("?AmountToDeposit", amountToDeposit),
                        new MySqlParameter("?Date", dateTime.Date),
                        new MySqlParameter("?Time", dateTime.TimeOfDay),
                        new MySqlParameter("?Descripcion", $"Deposito exitoso"),
                        new MySqlParameter("?Saldo", cliente.Saldo),
                        new MySqlParameter("?ID", cliente.Id)
                    });
                    command.ExecuteNonQuery();
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public void ConsultMovements()
        {
            try
            {
                string record;
                using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
                {
                    conexion.Open();
                    MySqlCommand command = new MySqlCommand()
                    {
                        CommandTimeout = 60,
                        Connection = conexion,
                        CommandText = "SELECT * FROM transacciones WHERE Cuenta_Numero=?Cuenta ORDER BY Fecha DESC LIMIT 0,5;"
                    };
                    command.Parameters.Add(new MySqlParameter("?Cuenta", cliente.Cuenta));
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        record = string.Format("IdT: {0} -, {1}, de {2}$ --> Fecha: {3}, Hora: {4} -, {5};", reader.GetString("id_Transaccion"), reader.GetString("Tipo_Transaccion"), reader.GetString("Monto"), reader.GetString("Fecha"), reader.GetString("Hora"), reader.GetString("Descripcion"));
                        cliente.movements.Add(record);
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

            int row = 4;
            //int contador = 6;
            foreach (string move in cliente.movements)
            {
                //if (contador.Equals(0)) break;
                Console.Write("{1}.- {0}", move, count++);
                Console.SetCursorPosition(4, row += 2);
                //contador--;
            }
        }

        public void ChangePassword(int newPassword)
        {
            using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
            {
                conexion.Open();
                MySqlCommand command = new MySqlCommand()
                {
                    CommandTimeout = 60,
                    Connection = conexion,
                    CommandText = "UPDATE clientes SET Contraseña=?newPassword WHERE idCliente=?ID;"
                };
                command.Parameters.AddRange(new[]
                {
                    new MySqlParameter("?newPassword", newPassword),
                    new MySqlParameter("?ID", cliente.Id)
                });
                command.ExecuteNonQuery();
            }
        }

        public bool AuthenticationUser(string InputUser)
        {
            using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
            {
                conexion.Open();
                MySqlCommand command = new MySqlCommand()
                {
                    CommandTimeout = 60,
                    Connection = conexion,
                    CommandText = "SELECT Nombre FROM clientes WHERE Nombre=?InputUser;"
                };
                command.Parameters.Add(new MySqlParameter("?InputUser", InputUser));
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return reader.GetString("Nombre").Equals(InputUser);
                }
                return false;
            };
        }

        public bool AuthenticationPassword(int InputPassword)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
                {
                    conexion.Open();
                    MySqlCommand command = new MySqlCommand()
                    {
                        CommandTimeout = 60,
                        Connection = conexion,
                        CommandText = "SELECT Contraseña FROM clientes WHERE idCliente=?Id;"
                    };
                    command.Parameters.Add(new MySqlParameter("?Id", cliente.Id));
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        return reader.GetInt32("Contraseña").Equals(InputPassword);
                    }
                    return false;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return false;
            }
        }

    }
}
