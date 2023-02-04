using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Threading;

namespace Fractals.Models.Fractals.Generators
{
    /// <summary>
    ///		Modelo de generación de un conjunto de Julia
    /// </summary>
    public class JuliaSetGenerator : IFractalGenerator
    {
        /// <summary>
        ///		Obtiene el canvas predeterminado
        /// </summary>
        public ParametersModel GetDefault()
        {  
            ParametersModel parameters = new ParametersModel(-0.21, -0.69, -0.22, -0.7, 1_000);

                // Asigna el centro predeterminado
                parameters.XCenter = -0.4;
                parameters.YCenter = 0.6;
                // Devuelve los parámetros
                return parameters;
        }

        /// <summary>
        ///		Genera el conjunto de forma asíncrona
        /// </summary>
        public async Task<FractalPointsModel> GenerateAsync(CanvasModel canvas, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Generate(canvas));
        }

        /// <summary>
        ///		Genera el conjunto
        /// </summary>
        private FractalPointsModel Generate(CanvasModel canvas)
        {
            FractalPointsModel fractalPoints = new(canvas);
            Complex central = new(canvas.Parameters.XCenter, canvas.Parameters.YCenter);

                // Calcula los puntos de escape del conjunto
                for (int pixelX = 0; pixelX < canvas.Width; pixelX++)
                    for (int pixelY = 0; pixelY < canvas.Height; pixelY++)
                    {
                        Complex point = canvas.TransformCoordinates(pixelX, pixelY) + central;

                            // Obtiene el valor de escape del punto
                            fractalPoints.Points[pixelX, pixelY] = ComputePoint(point, canvas.Parameters.Iterations, canvas.Parameters.Scape);
                    }
                // Devuelve los puntos generados
                return fractalPoints;
        }

        /// <summary>
        ///     Calcula el valor de escape de un número complejo
        /// </summary>
        private double ComputePoint(Complex c, int iterations, int scape)
        {
            int iteration = 0;
            Complex z = new();

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
