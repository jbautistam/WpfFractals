using System;
using System.Windows;

namespace Fractals.Controls.Fractal
{
	/// <summary>
	///		Argumentos de los eventos de selección de <see cref="RubberAddorner"/>
	/// </summary>
	public class RubberSelectionEventArgs : EventArgs
	{
		public RubberSelectionEventArgs(Rect rectangle)
		{
			Rectangle = rectangle;
		}

		/// <summary>
		///		Rectángulo seleccionado
		/// </summary>
		public Rect Rectangle { get; }
	}
}
