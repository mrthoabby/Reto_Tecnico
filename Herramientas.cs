using System.Linq;

namespace ReadConcurrent
{
	/// <summary>
	/// Clase Herramientas
	/// Provee todos las Herramientas para gestionar y ralizar busquedas en objetos tipo strings
	/// </summary>
	static class Herramientas
	{
		internal static int words_end_In_N = 0;


		private static readonly string[] character_garbage = new[]{"\r","\0","\t" };
		private static readonly string[] spaces_garbage = new[] {"  ","   ","\n" };

		/// <summary>
		/// Cuenta las palabras que terminan en un determinado caracter
		/// </summary>
		/// <param name="texto">El texto en donce se contaran las palabras</param>
		/// <param name="find">Caracter a encontrar al final de cada palabra</param>
		/// <returns></returns>
		internal static int ContadorEnd(this string texto , char find) => texto.ToLower().Split(" ").Count(x => x.Length > 0 && x[^1] == char.ToLower(find));
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="texto"></param>
		/// <returns></returns>
		internal static string CleanText(this string texto, char exepcion = ' ')
		{
			return new(texto.GarbajeRecolectorText().Where(valor => valor == ' ' || valor == exepcion ||char.IsLetterOrDigit(valor)).ToArray());
		}
		private static string GarbajeRecolectorText(this string texto)
		{
			string textToClear = "";
			for (int i = 0 ; i < 3 ; i++)
			{
				textToClear = texto.Replace(spaces_garbage[i] , " ");
			}
			return textToClear;
		}
	}
}
