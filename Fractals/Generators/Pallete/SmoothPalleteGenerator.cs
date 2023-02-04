using System;
using System.Windows.Media;
using Fractals.Models.Palletes;

namespace Fractals.Generators.Pallete
{
    /// <summary>
    ///     Generador de una paleta a partir de un número de iteraciones
    /// </summary>
	public class SmoothPalleteGenerator
	{
        /// <summary>
        ///     Genera una paleta
        /// </summary>
        public PalleteModel Generate(int length)
        {
            PalleteModel pallete = new();

                // Genera la paleta
                for (int index = 0; index < length; index++)
                    pallete.Add(GetColor(index, length));
                // Devuelve la paleta generada
                return pallete;
        }

        /// <summary>
        ///     Obtiene un color
        /// </summary>
        private Color GetColor(int iterations, int length)
        {
            double interpolated = (double) iterations / length * 100;

                // Devuelve el color generado
                return Color.FromArgb(255, 
                                      Clamp(15.3 * interpolated - 0.2295 * Math.Pow(interpolated, 2)), 
                                      Clamp(-0.2295 * Math.Pow(interpolated, 2) + 30.6 * interpolated - 765), 
                                      Clamp(-0.2295 * Math.Pow(interpolated, 2) + 45.9 * interpolated - 2040));

                // Obtiene un valor entre 0 y 255
                byte Clamp(double value)
                {
                    return (byte) Math.Min(Math.Max(value, 0), 255);
                }
        }
	}
}
