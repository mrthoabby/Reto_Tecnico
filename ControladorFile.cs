using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReadConcurrent
{
    /// <summary>
    /// Clase controlador de archivos
    /// Contiene todos los metos para acceder y leer o modificar los archivos de texto.
    /// </summary>
    /// <remarks>
    /// Lee archivos de texto y los deja listo para su usuo
    /// </remarks>
    static class ControladorFile
    {

        /// <summary>
        /// Lee el archivo de recurso de texto y ejecuta las acciones establecidas
        /// </summary>
        internal static void LectoEjecutor()
		{
            string nameFile = @"C:\Users\Danis\Documents\codigo\ReadConcurrent\el_quijote.txt";
            using(var flujodeTexto = File.Open(nameFile , FileMode.Open , FileAccess.Read , FileShare.Read))
			{
                byte[] conjuntodeBytes = new byte[1024];
                UTF8Encoding  text = new(true);
                while(flujodeTexto.Read(conjuntodeBytes,0,conjuntodeBytes.Length) > 0)
				{
                    Console.WriteLine($"This is of"+text.GetString(conjuntodeBytes).Normalize());
				}
			}

        }

	}
}
