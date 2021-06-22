using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadConcurrent
{
	/// <summary>
	/// Clase Herramientas
	/// Provee todos las Herramientas para gestionar y ralizar busquedas en objetos tipo strings
	/// </summary>
	static class Herramientas
	{
		/// <summary>
		/// Cuenta las palabras que terminan en un determinado caracter
		/// </summary>
		/// <param name="texto">El texto en donce se contaran las palabras</param>
		/// <param name="find">Caracter a encontrar al final de cada palabra</param>
		/// <returns></returns>
		internal static int contadorEnd(this string texto , char find)
		{
			return texto.ToLower().Split().Count(x => x[^1] ==char.ToLower(find));
		}
	}
}
