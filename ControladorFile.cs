using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ReadConcurrent
{
	/// <summary>
	/// Clase controlador de archivos
	/// </summary>
	/// <remarks>
	/// Contiene metodos para Leer o modificar archivos de texto.
	/// </remarks>
	static class ControladorFile
	{
		private static readonly ReaderWriterLockSlim blokeotoFile = new();

		/// <summary>
		/// Lee el archivo de recurso de texto y retorna parcialmente partes del mismo usando yield
		/// </summary>
		/// <returns>
		/// Retorna un <c>IEnumerable<string></c> con el contenido del texto leido
		/// </returns>
		internal static IEnumerable<string> Lector()
		{
			using (FileStream flujodeTexto = File.Open(@"C:\Users\Danis\Documents\codigo\ReadConcurrent\el_quijote.txt" , FileMode.Open , FileAccess.Read , FileShare.Read))
			{
				byte[] conjuntodeBytes = new byte[flujodeTexto.Length]; //Se lee todo el archivo para mantener congruencia en los datos en validacion de oraciones sin embargo se mantiene extructura yield en caso de implementar solucion alternativa para leer archivo por partes, pensando en el rendimiento de maquinas con pocos recursos
				while (flujodeTexto.Read(conjuntodeBytes , 0 , conjuntodeBytes.Length) > 0)
				{
					yield return Encoding.UTF8.GetString(conjuntodeBytes).Normalize();
				}
			}
		}

		/// <summary>
		/// Escribe información en el archivo de registros
		/// </summary>
		/// <param name="texto">Texto a escribir en el archivo de registros</param>
		internal  static void Escritor(string texto)
		{
			blokeotoFile.EnterWriteLock();
			using (StreamWriter escritor = new(@"C:\Users\Danis\Documents\codigo\ReadConcurrent\registros.txt" , true))
			{
				escritor.WriteLineAsync(texto); 
			}
			blokeotoFile.ExitWriteLock();
		}

	}
}
