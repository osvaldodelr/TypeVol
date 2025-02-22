using System;
using System.Runtime.InteropServices;
using NHotkey;

namespace VolumeControlApp
{
    class Program
    {
        // Importar las funciones de la API de Windows para mostrar/ocultar la ventana
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        const int SW_HIDE = 0;       // Para ocultar la ventana
        const int SW_SHOW = 5;       // Para mostrar la ventana

        static bool isWindowHidden = false;

        static void Main(string[] args)
        {
            // Registrar el atajo de teclado global Ctrl+Alt+Shift+V
            HotkeyManager.Current.AddOrReplace("Ctrl+Alt+Shift+V", (sender, e) =>
            {
                IntPtr handle = GetConsoleWindow();

                if (isWindowHidden)
                {
                    // Mostrar la ventana si está oculta
                    ShowWindowAsync(handle, SW_SHOW);
                    isWindowHidden = false;
                    Console.Clear();
                    Console.WriteLine("Ventana Restaurada.");
                }
                else
                {
                    // Ocultar la ventana si está visible
                    ShowWindowAsync(handle, SW_HIDE);
                    isWindowHidden = true;
                    Console.Clear();
                    Console.WriteLine("Ventana Ocultada.");
                }
            });

            // Mantener la aplicación en ejecución y esperando eventos de hotkeys
            Console.WriteLine("Presiona Ctrl + Alt + Shift + V para ocultar/mostrar la ventana.");
            Console.ReadLine();  // Mantiene la aplicación abierta
        }
    }
}
