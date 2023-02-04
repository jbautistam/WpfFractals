using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Threading;

namespace Fractals.Models.Fractals.Generators
{
    /// <summary>
    ///		Modelo base para la generación de fractales de tipo conjunto (Mandelbrot, Julia, Newton...)
    /// </summary>
    public abstract class BaseFractalSetGenerator : IFractalGenerator
    {
        // Manejadores de eventos
        public event EventHandler<EventArguments.ProgressEventArgs>? ProgressChanged;

        /// <summary>
        ///		Obtiene el canvas predeterminado
        /// </summary>
        public abstract ParametersModel GetDefault();

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
        protected abstract FractalPointsModel Generate(CanvasModel canvas);

        /// <summary>
        ///     Lanza el evento de progreso
        /// </summary>
        protected void RaiseEvent(int actual, int total)
        {
			ProgressChanged?.Invoke(this, new EventArguments.ProgressEventArgs(actual, total));
		}
    }
}