using System;
using System.Threading;

namespace ReadConcurrent
{
	class Multitask
	{
		static void Main(string[] args)
		{
			Thread hilo1 = new Thread(ejemplo1);
			//Thread hil21 = new Thread(prueba);

			hilo1.Start();
			//hil21.Start();
			void ejemplo1()
			{
				ControladorFile.metodo("HILO1");
			}

			ControladorFile.metodo("Main");
			//void prueba()
			//{
			//	for (int i = 0 ; i < 10 ; i++)
			//	{
			//		Console.WriteLine($"Prueba {i}");
			//		Thread.Sleep(5000);
			//	}
			//}

		}
	}
}
