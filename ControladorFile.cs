using System;
using System.Collections.Generic;
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
		private static readonly ReaderWriterLockSlim blokeotoFile = new();

		/// <summary>
		/// Lee el archivo de recurso de texto y retorna parcialmente partes del mismo usando yield
		/// </summary>
		/// <returns></returns>
		internal static IEnumerable<string> Lector()
		{
			string partialText = string.Empty;
			using (FileStream flujodeTexto = File.Open(@"C:\Users\Danis\Documents\codigo\ReadConcurrent\el_quijote.txt" , FileMode.Open , FileAccess.Read , FileShare.Read))
			{
				byte[] conjuntodeBytes = new byte[1024];
				while (flujodeTexto.Read(conjuntodeBytes , 0 , conjuntodeBytes.Length) > 0)
				{
					yield return Encoding.UTF8.GetString(conjuntodeBytes).Normalize();
				}
			}
			yield return partialText;
		}

		/// <summary>
		/// Escribe información en el archivo de registros
		/// </summary>
		/// <param name="texto">Texto a escribir en el archivo de registros</param>
		internal async static void Escritor(string texto)
		{
			blokeotoFile.EnterWriteLock();
			using (StreamWriter escritor = new(@"C:\Users\Danis\Documents\codigo\ReadConcurrent\registros.txt" , true))
			{
				await escritor.WriteLineAsync(texto);
			}
			blokeotoFile.ExitWriteLock();
		}

	}
}
