using System;
using System.Windows.Media;
using Fractals.Models.Palletes;

namespace Fractals.Generators.Pallete
{
    /// <summary>
    ///		Genera una paleta con un gradiante
    /// </summary>
    public class GradiantPalleteGenerator
	{
        /// <summary>
        ///     Genera un gradiante de color. 
        ///     <seealso cref="https://stackoverflow.com/questions/2011832/generate-color-gradient-in-c-sharp"/>
        /// </summary>
        public PalleteModel Generate(Color startColor, Color endColor, int length)
        {
            PalleteModel pallete = new();

                // Añade el color de inicio
                pallete.Add(startColor);
                // Genera los gradiantes
                if (length > 2)
                {
                    int steps = length - 1;
                    double diffA = endColor.A - startColor.A;
                    double diffR = endColor.R - startColor.R;
                    double diffG = endColor.G - startColor.G;
                    double diffB = endColor.B - startColor.B;
                    double stepA = diffA / steps;
                    double stepR = diffR / steps;
                    double stepG = diffG / steps;
                    double stepB = diffB / steps;

                        // Interpola los colores
                        for (int index = 1; index < steps; ++index)
                            pallete.Add(Color.FromArgb(Interpolate(startColor.A, stepA, index),
                                                       Interpolate(startColor.R, stepR, index),
                                                       Interpolate(startColor.G, stepG, index),
                                                       Interpolate(startColor.B, stepB, index))
                                       );
                        // Añade el color de fin
                        pallete.Add(endColor);
                }
                // Devuelve la paleta
                return pallete;

                // Interpola un color
                byte Interpolate(int startColor, double step, int i)
                {
                    return (byte) Math.Round(startColor + step * i);
                }
        }
    }
}