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
		internal static int tenses_up_Tfive = 0;
		internal static int qantity_of_Parrafos = 0;
		internal static int qantity_letter_distic_n = 0;
		private static readonly string[] character_garbage = new[]{"\r","\0","\t" };
		private static readonly string[] spaces_garbage = new[] {"  ","   ","\n" };

		/// <summary>
		/// Cuenta las palabras que terminan en un determinado caracter
		/// </summary>
		/// <param name="texto">El texto en donce se contaran las palabras</param>
		/// <param name="find">Caracter a encontrar al final de cada palabra</param>
		/// <returns>Un entero con la cantidad de palabras terminadas en el caracter buscado</returns>
		internal static int ContadorEnd(this string texto , char find) => texto.ToLower().Split(" ").Count(x => x.Length > 0 && x[^1] == char.ToLower(find));

		internal static int ContadorSentences(this string texto)
		{
			return texto.Split('.').Count(tense => tense != "" && tense[^1] != '▄' && tense.Split(' ').Count(subpalabra => subpalabra.isword() ) > 15);
		}

		internal static int ContadordeParrafos(this string[] sentenses)
		{
			return sentenses.Count(parrafo => parrafo.Length > 0 && parrafo.Replace(".▄","█")[^1] == '█');
			
		}

		internal static string[] preparadodeSentenses(this string texto)
		{
			return texto.CleanText('.' , false).Split("▀");
		}
		/// <summary>
		/// Toma un string y se encarga de hacer limpieza dejando solo caracteres alfanumericos, espacios en blancos y un caracter  <see langword="exepcion"/> pasado como argumento.
		/// </summary>
		/// <param name="texto">El texto recibido para limpiar</param>
		/// <param name="exepcion">Toma un caracter para tomarlo como una exepcion y no ser eliminado "El caracter por defecto es el espacio en blanco"</param>
		/// <returns>Retorna un string con caracteres alfanumericos, espacios en blancos y un caracter seleccionado</returns>
		internal static string CleanText(this string texto , char exepcion = ' ' , bool saltodeLinea = true)
		{
			if(saltodeLinea)
			return new string(texto.GarbajeRecolectorText(saltodeLinea).Where(valor => valor == ' ' || valor == exepcion || char.IsLetterOrDigit(valor)).ToArray()).Trim();
			else
				return new string(texto.GarbajeRecolectorText(saltodeLinea).Where(valor => valor == ' ' || valor == exepcion || valor == '▄' || valor == '▀'  || char.IsLetterOrDigit(valor)).ToArray()).Trim();
		}



		//internal static string RemoveLetter()
		//{

		//}

		/// <summary>
		/// Limpia un string de caracteres en blancos dobles, triples y de el caracter de escape salto de linea
		/// </summary>
		/// <param name="texto">El texto a limipar</param>
		/// <returns>un string sin espacios dobles o triples y sin saltos de linea</returns>
		private static string GarbajeRecolectorText(this string texto, bool salto)
		{
			string textToClear = "";
			if (salto)
			{
				for (int i = 0 ; i < 3 ; i++)
				{
					textToClear = texto.Replace(spaces_garbage[i] , " ");
				}
			}
			else
			{
				for (int i = 0 ; i < 2 ; i++)
				{
					textToClear = texto.Replace(spaces_garbage[i] , " ");
				}
				textToClear = textToClear.Replace("\n" , "▄▀").Replace("▄▀▄▀" , "▄▀");
			}
			return textToClear;
		}

		internal static bool isword(this string word)
		{
			return !(word == "") || !word.Any(letra => char.IsDigit(letra));
		}


	}
}
