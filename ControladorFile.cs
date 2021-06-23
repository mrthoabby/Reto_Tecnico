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
		private static readonly string ruta_quijote = @"C:\Users\Danis\Documents\codigo\ReadConcurrent\el_quijote.txt";
		//Fue necesario copiar y pegar el contenido de el archivo de don quijote varias veces para superar las 10MB

		/// <summary>
		/// Lee el archivo de recurso de texto y retorna un <see cref="string"/> con todo el texto
		/// </summary>
		/// <returns>
		/// Retorna un <c>IEnumerable<string></c> con el contenido del texto leido
		/// </returns>
		internal static string Lector()
		{
			using (FileStream flujodeTexto = File.Open(ruta_quijote , FileMode.Open , FileAccess.Read , FileShare.Read))
			{
				string todoelarchivo = ""; //Podría ser mas rapido un stream builder pero no es seguro para operaciones multihilo
				byte[] conjuntodeBytes = new byte[flujodeTexto.Length];
				while (flujodeTexto.Read(conjuntodeBytes , 0 , conjuntodeBytes.Length) > 0)
				{
					todoelarchivo += Encoding.UTF8.GetString(conjuntodeBytes).Normalize();
				}
				return todoelarchivo;
			}
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
