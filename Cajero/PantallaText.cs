using System;
using System.Collections.Generic;
using System.Text;

namespace Cajero
{
    class PantallaText
    {
        public PantallaText()
        {

            Console.WriteLine("Iniciando pantalla");

        }

        public void SettingsConsole()
        {
            Console.CursorSize = 10;
            Console.SetWindowSize(100, 30);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Title = "Bank International Console";
        }

        public void printTextInputPassword()
        {

            Console.SetCursorPosition(20, 1);
            Console.Write("Ingreso de Contraseña");
            Console.SetCursorPosition(29, 5);
            Console.Write("Contraseña: ");
            Console.SetCursorPosition(33, 8);
            Console.Write("[____]");
            // Colocando el cursor en [____]
            Console.SetCursorPosition(34, 8);
        }
        public void printWrongPassword(int intent)
        {
            Console.SetCursorPosition(5, 12);
            Console.Write("Contraseña incorrecta.  Intentos restantes {0}.\n\n\t\tPresione [ Enter ] para Continuar.", intent);
            Console.ReadKey();
        }

    }
}
