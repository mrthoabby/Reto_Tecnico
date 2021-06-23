using System.Linq;

namespace ReadConcurrent
{
	/// <summary>
	/// Clase Herramientas
	/// </summary>
	/// <remarks>
	/// Provee todos las herramientas para gestionar analizar y realizar busquedas en objetos tipo <code>string</code>
	/// </remarks>
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
		/// <returns>Retorna un <see cref="int"/> con la cantidad de palabras terminadas en el caracter buscado</returns>
		internal static int ContadorEnd(this string texto , char find) => texto.ToLower().Split(" ").Count(x => x.Length > 0 && x[^1] == char.ToLower(find));

		/// <summary>
		/// Cuenta la cantidad de parrafos que contengan oraciones con mas de 15 palabras
		/// </summary>
		/// <param name="texto">Recibe un <see cref="string"/> Con el texto a analizar</param>
		/// <returns></returns>
		internal static int ContadorSentences(this string texto)
		{
			return texto.Split('.').Count(tense => tense != "" && tense[^1] != '▄' && tense.Split(' ').Count(subpalabra => subpalabra.isword() ) > 15);
		}

		/// <summary>
		/// Cuenta la cantidad de parrafos en un array de string
		/// </summary>
		///<remarks>
		///Toma un array de <see cref="string"/> que fue preparado por el metodo <see cref="preparadodeSentenses(string)"/>
		///<para>para determinar cuantos parrafos existen en este array  EL el cual deja el valor <value>.▄</value> donde hay un punto y aparte  </para>
		/// </remarks>
		/// <param name="sentenses">Arrays con parrafos</param>
		/// <returns>Retorna un <see cref="int"/> con la cantidad de parrafos en el Arrego de <see cref="string"/> recibido</returns>
		internal static int ContadordeParrafos(this string[] sentenses)
		{
			
			return sentenses.Count(parrafo => parrafo.Length > 0 && parrafo.Replace(".▄","█")[^1] == '█');
			
		}


		internal static string[] preparadodeSentenses(this string texto)
		{
			return texto.CleanText('.' , false).Split("▀");
		}

		/// <summary>
		/// Toma un string y se encarga de hacer limpieza o filtrado dejando solo caracteres alfanumericos, espacios en blancos y un caracter  <see langword="exepcion"/> pasado como argumento.
		/// </summary>
		/// <remarks>
		/// Cuando el parametro <paramref name="saltodeLinea"/> esta establecido en <see cref="false"/> omite los caracteres  el filtrado de los caracteres <c>'▀' y '▄'</c> 
		/// </remarks>
		/// <param name="texto">El texto recibido para Filtra</param>
		/// <param name="exepcion">Toma un caracter como exepcion para no ser filtrado "El caracter por defecto es el espacio en blanco"</param>
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
