using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadConcurrent
{
    static class ControladorFile
    {

        internal static void metodo(string number)
		{
            string nameFile = @"C:\Users\Danis\Documents\codigo\ReadConcurrent\el_quijote.txt";
            using(var flujodeTexto = File.Open(nameFile , FileMode.Open , FileAccess.Read , FileShare.Read))
			{
                byte[] conjuntodeBytes = new byte[1024];
                UTF8Encoding  text = new(true);
                while(flujodeTexto.Read(conjuntodeBytes,0,conjuntodeBytes.Length) > 0)
				{
                    Console.WriteLine($"Read from process {number} {conjuntodeBytes[0]}");
                    Console.WriteLine($"This is of {number}"+text.GetString(conjuntodeBytes).Normalize());
				}
			}

        }

	}
}
