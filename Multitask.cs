using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace ReadConcurrent
{
	/// <summary>
	/// Clase Multi Hilo
	/// </summary>
	/// <remarks>
	/// Clase encargada de realizar 4 tareas al mismo tiempo sobre un mismo archvo
	/// </remarks>
	class Multitask
	{
		static void Main(string[] args)
		{
			args = null;
			bool isavaible = true;
			if (!File.Exists(@"C:\Users\Danis\Documents\codigo\ReadConcurrent\registros.txt"))
			{
				try
				{
					File.Create(@"C:\Users\Danis\Documents\codigo\ReadConcurrent\registros.txt" , 1045 , FileOptions.Asynchronous);
				}
				catch (UnauthorizedAccessException)
				{
					Console.WriteLine("No se tienen permisos para crear el archivo registros en este directorio");
					isavaible = false;
				}
				catch (Exception)
				{
					Console.WriteLine("Se produjo un error al intentar crear el archivo registros");
					isavaible = false;
				}
			}

			if (isavaible)
			{
				Thread nucleoOne = new(ProcesoOne);
				Thread nucloeTwo = new(ProcesoTwo);
				Thread nucleoThree = new(ProcesoThree);
				Thread nucleoFour = new(ProcesoFour);

				nucleoOne.Start("PROCESO 1 WORD-N");
				nucloeTwo.Start("PROCESO 2 #-TENSES>15W");
				nucleoThree.Start("PROCESO 3 #-PARRAFOS");
				nucleoFour.Start("PROCESO 4 WORD no-N n");
				Console.WriteLine($"4 Procesos en marcha...");
			}
			else
			{
				Console.WriteLine("No fue posible iniciar el programa por que no se pudo crear el archivo de registro");
			}
		}

		/// <summary>
		/// Hilo Encargado de contrar palabras termindas en N
		/// </summary>
		/// <param name="name">Nombre del hilo</param>
		private static void ProcesoOne(object name)
		{
			if (Thread.CurrentThread.Name == null)
				Thread.CurrentThread.Name = name.ToString();
			checked
			{
				Herramientas.words_end_In_N += ControladorFile.Lector().CleanText().ContadorEnd('n');
			}
			Console.WriteLine($"El número de palabras por la N es: {Herramientas.words_end_In_N}");
			ControladorFile.Escritor($"El número de palabras por la terminadas en n N son: {Herramientas.words_end_In_N}");
		}
		/// <summary>
		/// Hilo Encargado de contrar oraciones con mas de 15 palabras
		/// </summary>
		/// <param name="name">Nombre del hilo</param>
		private static void ProcesoTwo(object name)
		{
			if (Thread.CurrentThread.Name == null)
				Thread.CurrentThread.Name = name.ToString();
			foreach (var parrafo in ControladorFile.Lector().PreparadodeSentenses())
			{
				checked
				{
					Herramientas.tenses_up_Tfive += parrafo.ContadorSentences();
				}
			}
			Console.WriteLine($"El número de de oraciones con mas de 15 palabras es: {Herramientas.tenses_up_Tfive}");
			ControladorFile.Escritor($"El número de de oraciones con mas de 15 palabras es: {Herramientas.tenses_up_Tfive}");
		}

		/// <summary>
		/// Hilo Encargado de contar la cantidad de parrafos en el texto
		/// </summary>
		/// <param name="name">Nombre del hilo</param>
		private static void ProcesoThree(object name)
		{
			if (Thread.CurrentThread.Name == null)
				Thread.CurrentThread.Name = name.ToString();
			checked
			{
				Herramientas.qantity_of_Parrafos += ControladorFile.Lector().PreparadodeSentenses().ContadordeParrafos();
			}
			Console.WriteLine($"La cantidad de parrafos en el texto es: {Herramientas.qantity_of_Parrafos}");
			ControladorFile.Escritor($"La cantidad de parrafos en el texto es: {Herramientas.qantity_of_Parrafos}");
		}

		/// <summary>
		/// Hilo Encargado de contar la cantidad de caracteres alfanúmericos distintos a 'N' Y 'n'
		/// </summary>
		/// <param name="name">Nombre del hilo</param>
		private static void ProcesoFour(object name)
		{
			if (Thread.CurrentThread.Name == null)
				Thread.CurrentThread.Name = name.ToString();
			checked
			{
				Herramientas.qantity_letter_distic_n = string.Concat(ControladorFile.Lector().CleanText().ToLower().Where(c => !char.IsWhiteSpace(c) && c != 'n')).Length;
			}
			Console.WriteLine($"La cantidad de caracteres alfanúmericos distintos a 'N' 'n' es: {Herramientas.qantity_letter_distic_n}");
			ControladorFile.Escritor($"La cantidad de caracteres alfanúmericos distintos a 'N' 'n' es: {Herramientas.qantity_letter_distic_n}");
		}
	}
}
