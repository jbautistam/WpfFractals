using System;

namespace Fractals.Models.Fractals
{
	/// <summary>
	///		Modelo de generación de fractales
	/// </summary>
	public class FractalPointsModel
	{
		public FractalPointsModel(CanvasModel canvas)
		{
			Canvas = canvas;
			Points = new double[canvas.Width, canvas.Height];
		}

		/// <summary>
		///		Depuración de los datos del fractal
		/// </summary>
		public System.Text.StringBuilder Debug()
		{
			System.Text.StringBuilder builder = new();

				// Genera la cadena
				for (int x = 0; x < Canvas.Width; x++)
					for (int y = 0; y < Canvas.Height; y++)
						builder.AppendLine($"{x}\t{y}\t{Points[x, y]}");
				// Devuelve la cadena generada
				return builder;
		}

		/// <summary>
		///		Canvas con el cual se genera el modelo
		/// </summary>
		public CanvasModel Canvas { get; }

		/// <summary>
		///		Puntos del canvas (valor entre 0 y 1)
		/// </summary>
		public double[,] Points { get; }
	}
}
