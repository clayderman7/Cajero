using Microsoft.VisualBasic.CompilerServices;
using System;

namespace Cajero
{
    partial class Program
    {
        static readonly PantallaText pantalla;
        static int intentos;
        static string InputName;
        //static readonly DateTime dateTime = DateTime.Now;

        static Program()
        {
            intentos = 3;
            pantalla = new PantallaText();
        }

        static void Main(string[] args)
        {

            AccessLogicATM();

        }
    }
}