using System;
using System.Linq;
using System.Threading;

namespace ReadConcurrent
{
	class Multitask
	{
		static void Main(string[] args)
		{
			args = null;
			Thread nucleoOne = new(ProcesoOne);
			Thread nucloeTwo = new(ProcesoTwo);
			Thread nucleoThree = new(ProcesoThree);
			Thread nucleoFour = new(ProcesoFour);

			nucleoOne.Start("Danis");
			nucloeTwo.Start("Geral");
			nucleoThree.Start("Merchan");
			nucleoFour.Start("OHH YEAH");
		}

		private static void ProcesoOne(object obj)
		{
			if (Thread.CurrentThread.Name == null)Thread.CurrentThread.Name = "PROCESO 1 WORD-N";
			foreach (var fracciondeTexto in ControladorFile.Lector()) Herramientas.words_end_In_N += fracciondeTexto.CleanText().ContadorEnd('n');
			Console.WriteLine($"El número de palabras por la N es: {Herramientas.words_end_In_N}");
			ControladorFile.Escritor($"El número de palabras por la N es: {Herramientas.words_end_In_N}");
		}
		private static void ProcesoTwo(object obj)
		{
			if (Thread.CurrentThread.Name == null)
				Thread.CurrentThread.Name = "PROCESO 2 #-TENSES>15W";
			foreach (var fracciondeTexto in ControladorFile.Lector())
			{
				foreach (var parrafo in fracciondeTexto.preparadodeSentenses())
				{
					Herramientas.tenses_up_Tfive += parrafo.ContadorSentences();
				}
			}
			Console.WriteLine($"El número de de oraciones con mas de 15 palabras es: {Herramientas.tenses_up_Tfive}");
			ControladorFile.Escritor($"El número de de oraciones con mas de 15 palabras es: {Herramientas.tenses_up_Tfive}");
		}
		private static void ProcesoThree(object obj)
		{
			if (Thread.CurrentThread.Name == null)
				Thread.CurrentThread.Name = "PROCESO 3 #-PARRAFOS";
			foreach (var fracciondeTexto in ControladorFile.Lector())
			{
				Herramientas.qantity_of_Parrafos += fracciondeTexto.preparadodeSentenses().ContadordeParrafos();
				Console.WriteLine($"La cantidad de parrafos en el texto es: {Herramientas.qantity_of_Parrafos}");
				ControladorFile.Escritor($"La cantidad de parrafos en el texto es: {Herramientas.qantity_of_Parrafos}");
			}
		}
		private static void ProcesoFour(object obj)
		{
			if (Thread.CurrentThread.Name == null)
				Thread.CurrentThread.Name = "PROCESO 1 WORD-N";
			foreach (var fracciodeTexto in ControladorFile.Lector())
			{
				Herramientas.qantity_letter_distic_n =  string.Concat(fracciodeTexto.CleanText().ToLower().Where(c => !char.IsWhiteSpace(c) && c != 'n' )).Length;
				Console.WriteLine($"La cantidad de caracteres alfanúmericos distintos a 'N' 'n' es: {Herramientas.qantity_letter_distic_n}");
				ControladorFile.Escritor($"La cantidad de caracteres alfanúmericos distintos a 'N' 'n' es: {Herramientas.qantity_letter_distic_n}");
			}
		}
	}
}
