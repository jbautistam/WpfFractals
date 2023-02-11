using System;
using System.Numerics;

namespace Fractals.Models.Fractals.Generators
{
    /// <summary>
    ///		Modelo de generación de un conjunto de Julia
    /// </summary>
    public class JuliaSetGenerator : BaseFractalSetGenerator
    {
        /// <summary>
        ///		Obtiene el canvas predeterminado
        /// </summary>
        public override ParametersModel GetDefault()
        {  
            ParametersModel parameters = new ParametersModel(-2, -2, 2, 2, 1_000);

                // Asigna el centro predeterminado
                parameters.XCenter = -0.74543;
                parameters.YCenter = 0.11301;
                // Devuelve los parámetros
                return parameters;
        }

        /// <summary>
        ///		Genera el conjunto
        /// </summary>
        protected override FractalPointsModel Generate(CanvasModel canvas)
        {
            FractalPointsModel fractalPoints = new(canvas);
            Complex central = new(canvas.Parameters.XCenter, canvas.Parameters.YCenter);

                // Calcula los puntos de escape del conjunto
                for (int pixelX = 0; pixelX < canvas.Width; pixelX++)
                {
                    // Calcula los puntos
                    for (int pixelY = 0; pixelY < canvas.Height; pixelY++)
                    {
                        Complex point = canvas.TransformCoordinates(pixelX, pixelY);

                            // Obtiene el valor de escape del punto
                            fractalPoints.Points[pixelX, pixelY] = ComputePoint(point, central, canvas.Parameters.Iterations, canvas.Parameters.Scape);
                    }
                    // Lanza el evento
                    RaiseEvent(pixelX, canvas.Width);
                }
                // Devuelve los puntos generados
                return fractalPoints;
        }

        /// <summary>
        ///     Calcula el valor de escape de un número complejo
        /// </summary>
        private double ComputePoint(Complex point, Complex c, int iterations, int scape)
        {
            int iteration = 0;
            Complex z = point;

                // Calcula el valor hasta que se sale del ámbito o se sobrepasa el número de iteraciones
                do
                {
                    // Calcula z = z ^ 2 + c
                    z = z * z + c;
                    // Incrementa el número de iteraciones
                    iteration++;
                }
                while (z.Magnitude < scape && iteration < iterations);
                // Devuelve el número de iteración dependiendo de si ha salido por el valor de escape o por las iteraciones máximas
                if (iteration < iterations)
                    return 1.0 * iteration / iterations;
                else
                    return 0;
        }
    }
}
