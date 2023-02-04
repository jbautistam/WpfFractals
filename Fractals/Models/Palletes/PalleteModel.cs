using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Fractals.Models.Palletes
{
    /// <summary>
    ///		Modelo con los datos de una paleta
    /// </summary>
    public class PalleteModel
    {
        /// <summary>
        ///		Añade un color a la paleta
        /// </summary>
        public void Add(Color color)
        {
            Colors.Add((color, BitConverter.ToInt32(new[] { color.B, color.G, color.R, color.A}, 0)));
        }

        /// <summary>
        ///		Obtiene un color de la paleta
        /// </summary>
        public Color Get(double index)
        {
            return Colors[GetIndex(index)].color;
        }

        /// <summary>
        ///     Obtiene un color de la paleta en formato entero a partir de un índice
        /// </summary>
        public int GetInt(int index)
        {
            return Colors[GetIndex((double) (index % Length) / Length)].bgr32;
        }

        /// <summary>
        ///     Obtiene un color de la paleta en formato entero
        /// </summary>
        public int GetInt(double index)
        {
            return Colors[GetIndex(index)].bgr32;
        }

        /// <summary>
        ///     Obtiene el índice sobre la lista de colores
        /// </summary>
        private int GetIndex(double index)
        {
            return ((int) (index * Colors.Count)) % Colors.Count;
        }

        /// <summary>
        ///		Colores de la paleta
        /// </summary>
        private List<(Color color, int bgr32)> Colors { get; } = new();

        /// <summary>
        ///     Tamaño de la paleta
        /// </summary>
        public int Length => Colors.Count;
    }
}
