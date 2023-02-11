using System;

namespace Fractals.Models.Fractals
{
    /// <summary>
    ///     Parámetros de generación del fractal
    /// </summary>
	public class ParametersModel
	{
        public ParametersModel(double xmin, double ymin, double xmax, double ymax, int iterations)
        {
            XMin = xmin;
            YMin = ymin;
            XMax = xmax;
            YMax = ymax;
            Iterations = iterations;
        }

        /// <summary>
        ///     Redimension los parámetros
        /// </summary>
		internal void ResizeTo(double xMin, double yMin, double xMax, double yMax)
		{
			XMin = xMin;
            YMin = yMin;
            XMax = xMax;
            YMax = yMax;
		}

        /// <summary>
        ///     X mínima
        /// </summary>
        public double XMin { get; set; }

        /// <summary>
        ///     Y mínima
        /// </summary>
        public double YMin { get; set; }

        /// <summary>
        ///     X máxima
        /// </summary>
        public double XMax { get; set; }

        /// <summary>
        ///     Y máxima
        /// </summary>
        public double YMax { get; set; }

        /// <summary>
        ///     Centro X (cuando se necesitan parámetros adicionales)
        /// </summary>
        public double XCenter { get; set; }

        /// <summary>
        ///     Centro Y (cuando se necesitan parámetros adicionales)
        /// </summary>
        public double YCenter { get; set; }

        /// <summary>
        ///     Valor de escape de la función de generación
        /// </summary>
        public int Scape { get; set; } = 4;

        /// <summary>
        ///     Número de iteraciones
        /// </summary>
        public int Iterations { get; set; }
	}
}