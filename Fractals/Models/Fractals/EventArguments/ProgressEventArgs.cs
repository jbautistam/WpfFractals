using System;

namespace Fractals.Models.Fractals.EventArguments
{
	/// <summary>
	///		Argumentos del evento de progreso
	/// </summary>
	public class ProgressEventArgs : EventArgs
	{
		public ProgressEventArgs(int actual, int total)
		{
			Actual = actual;
			Total = total;
		}

		/// <summary>
		///		Valor actual
		/// </summary>
		public int Actual { get; }

		/// <summary>
		///		Total
		/// </summary>
		public int Total { get; }

		/// <summary>
		///		Porcentaje
		/// </summary>
		public double Percentage
		{
			get
			{
				if (Total == 0)
					return 0;
				else
					return 1.0 * Actual * 100 / Total;
			}
		}
	}
}
