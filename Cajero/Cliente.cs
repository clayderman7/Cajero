using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cajero
{
    class Cliente
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public int Password { get; set; }
        public int Cuenta { get; set; }
        public double Saldo { get; set; }
        //private readonly string connectionString;
        //private Dictionary<string, Cliente> listClientes;
        public List<string> movements;

        public Cliente()
        {
            //connectionString = "Server=127.0.0.1;Port=3306;Database=banco_db;Uid=root;password=Mimamamemima.2020.;";
            //listClientes = new Dictionary<string, Cliente>();
            movements = new List<string>();
        }

    }
}
