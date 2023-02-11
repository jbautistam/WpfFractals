using System;
using System.Numerics;
using System.Windows;

namespace Fractals.Models.Fractals
{
    /// <summary>
    ///     Datos del canvas de dibujo
    /// </summary>
	public class CanvasModel
	{
        public CanvasModel(ParametersModel parameters, int width, int height)
        {
            Parameters = parameters;
            Width = width;
            Height = height;
        }

        /// <summary>
        ///     Transforma las coordenadas de un pixel
        /// </summary>
        public Complex TransformCoordinates(int pixelX, int pixelY)
        {
            return new(Parameters.XMin + (Parameters.XMax - Parameters.XMin) * pixelX / (Width - 1),
                       Parameters.YMin + (Parameters.YMax - Parameters.YMin) * pixelY / (Height - 1));
        }

        /// <summary>
        ///     Redimensiona las coordenadas
        /// </summary>
		internal void ResizeTo(Point start, Point end, double imageWidth, double imageHeight)
		{
			double distX = Parameters.XMax - Parameters.XMin; // Fx - Sx;
			double distY = Parameters.YMax - Parameters.YMin; // Fy - Sy;
			double newXMin = (double) start.X / imageWidth * distX + Parameters.XMin;
			double newXMax = (double) end.X / imageWidth * distX + Parameters.XMin;
			double newYMin = (double) start.Y / imageHeight * distY + Parameters.YMin;
			double newYMax = (double) end.Y / imageHeight * distY + Parameters.YMin;

                // Redimensiona los parámetros
                Parameters.ResizeTo(newXMin, newYMin, newXMax, newYMax);
		}

		/// <summary>
		///     Parámetros de generación
		/// </summary>
		public ParametersModel Parameters { get; }

        /// <summary>
        ///     Ancho del resultado
        /// </summary>
        public int Width { get; }

        /// <summary>
        ///     Altura del resultado
        /// </summary>
        public int Height { get; }
	}
}