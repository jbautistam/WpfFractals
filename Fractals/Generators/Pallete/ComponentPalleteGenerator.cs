using System;
using System.Windows.Media;
using Fractals.Models.Palletes;

namespace Fractals.Generators.Pallete
{
    /// <summary>
    ///		Genera una paleta por un componente del color
    /// </summary>
    public class ComponentePalleteGenerator
	{
        /// <summary>
        ///     Genera la paleta
        /// </summary>
        public PalleteModel Generate(bool useRed, bool useGreen, bool useBlue, int length)
        {
            PalleteModel pallete = new();

                // Crea la paleta
                for (int index = 0; index < length; index++)
                {
                    byte component = (byte) (255 * ((double) index / length));

                        pallete.Add(Create(useRed, useGreen, useBlue, component));
                }
                // Devuelve la paleta
                return pallete;


                // Interpola un color
                Color Create(bool useRed, bool useGreen, bool useBlue, byte value)
                {
                    byte red = 0, green = 0, blue = 0;

                        // Recoge los componentes
                        if (useRed)
                            red = value;
                        if (useGreen)
                            green = value;
                        if (useBlue)
                            blue = value;
                        // Crea el color
                        return Color.FromRgb(red, green, blue);
                }
        }
    }
}