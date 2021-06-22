using System;
using System.IO;
using System.Text;
using System.Threading;

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
		private static ReaderWriterLockSlim blokeotoFile = new ReaderWriterLockSlim();


		/// <summary>
		/// Lee el archivo de recurso de texto y ejecuta las acciones establecidas
		/// </summary>
		internal static void LectoEjecutor(string proceso)
		{
			using (FileStream flujodeTexto = File.Open(@"C:\Users\Danis\Documents\codigo\ReadConcurrent\el_quijote.txt" , FileMode.Open , FileAccess.Read , FileShare.Read))
			{
				byte[] conjuntodeBytes = new byte[1024];
				UTF8Encoding  text = new(true);
				while (flujodeTexto.Read(conjuntodeBytes , 0 , conjuntodeBytes.Length) > 0)
				{
					Console.WriteLine($"This is of" + text.GetString(conjuntodeBytes).Normalize());
				}

			}

		}

		/// <summary>
		/// Escribe información en el archivo de registros
		/// </summary>
		/// <param name="texto">Texto a escribir en el archivo de registros</param>
		internal async static void Escritor(string texto)
		{
			blokeotoFile.EnterWriteLock();
			using (StreamWriter escritor = new StreamWriter(@"C:\Users\Danis\Documents\codigo\ReadConcurrent\registros.txt" , true))
			{
				await escritor.WriteLineAsync(texto);
			}
			blokeotoFile.ExitWriteLock();
		}




	}
}
