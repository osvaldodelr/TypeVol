using System;
using System.Runtime.InteropServices;

class Program
{
    // Importar las funciones de la API de Windows para mostrar/ocultar la ventana
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    [DllImport("user32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    // Constantes para las teclas y acciones
    const int SW_HIDE = 0;
    const int SW_SHOW = 5;

    const uint MOD_ALT = 0x1;
    const uint MOD_CONTROL = 0x2;
    const uint MOD_SHIFT = 0x4;
    const uint VK_V = 0x56; // Tecla 'V'

    static bool isWindowHidden = false;

    static void Main(string[] args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // Solo ejecutamos el código de Windows si estamos en Windows
            IntPtr consoleHandle = GetConsoleWindow();

            // Registrar el hotkey: Ctrl + Alt + Shift + V
            if (RegisterHotKey(consoleHandle, 1, MOD_CONTROL | MOD_ALT | MOD_SHIFT, VK_V))
            {
                Console.WriteLine("Presiona Ctrl + Alt + Shift + V para ocultar/mostrar la ventana.");

                // Mantener el programa en ejecución y esperando el hotkey
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;

                    // Alternar entre mostrar y ocultar la ventana con el hotkey
                    if (isWindowHidden)
                    {
                        // Mostrar la ventana si está oculta
                        ShowWindowAsync(consoleHandle, SW_SHOW);
                        isWindowHidden = false;
                        Console.Clear();
                        Console.WriteLine("Ventana Restaurada.");
                    }
                    else
                    {
                        // Ocultar la ventana si está visible
                        ShowWindowAsync(consoleHandle, SW_HIDE);
                        isWindowHidden = true;
                        Console.Clear();
                        Console.WriteLine("Ventana Ocultada.");
                    }
                }

                // Desregistrar el hotkey cuando el programa termine
                UnregisterHotKey(consoleHandle, 1);
            }
            else
            {
                Console.WriteLine("No se pudo registrar el hotkey.");
            }
        }
        else
        {
            Console.WriteLine("El sistema operativo no es Windows, no se puede ocultar/mostrar la ventana.");
        }
    }
}