using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Fractals.Models.Fractals.Generators
{
    /// <summary>
    ///		Generador de fractales
    /// </summary>
    public class FractalGenerator
    {
		/// <summary>
		///		Tipo de fractal
		/// </summary>
		public enum FractalType
		{
			/// <summary>Desconocido: no se debería utilizar</summary>
			Unknown,
			/// <summary>Conjunto de Mandelbrot</summary>
			Mandelbrot,
			/// <summary>Conjunto de Julia</summary>
			Julia
		}
        // Manejadores de eventos
        public event EventHandler<EventArguments.ProgressEventArgs>? ProgressChanged;

		public FractalGenerator(int width, int height)
		{
			// Asigna las propiedades
			Width = width;
			Height = height;
			// Crea el generador predeterminado
			Update(FractalType.Mandelbrot);
			//Generator = new MandelbrotSetGenerator();
			//Canvas = new CanvasModel(Generator.GetDefault(), width, height);
		}

		/// <summary>
		///		Modifica el tipo
		/// </summary>
		public void Update(FractalType type)
		{
			if (Type != type)
			{
				// Cambia el tipo
				Type = type;
				// Modifica el generador
				UpdateGenerator(Type);
			}
		}

		/// <summary>
		///		Actualiza el generador
		/// </summary>
		private void UpdateGenerator(FractalType type)
		{
			// Cambia el generador
			switch (type)
			{
				case FractalType.Julia:
						Generator = new JuliaSetGenerator();
					break;
				default:
						Generator = new MandelbrotSetGenerator();
					break;
			}
			// Asigna el manejador de eventos
			Generator.ProgressChanged += (sender, args) => ProgressChanged?.Invoke(this, args);
			// Crea el canvas
			Reset();
		}

		/// <summary>
		///		Inicializa el canvas
		/// </summary>
		public void Reset()
		{
			if (Generator is not null)
				Canvas = new CanvasModel(Generator.GetDefault(), Width, Height);
		}

		/// <summary>
		///		Crea los valores del fractal
		/// </summary>
		public async Task<FractalPointsModel?> ComputeFractalAsync(CancellationToken cancellationToken)
		{
			if (Canvas is not null && Generator is not null)
				return await Generator.GenerateAsync(Canvas, cancellationToken);
			else
				return null;
		}

		/// <summary>
		///		Redimensiona el canvas de dibujo
		/// </summary>
		public void Resize(Point topLeft, Point bottomRight, double actualWidth, double actualHeight)
		{
			if (Canvas is not null)
				Canvas.ResizeTo(topLeft, bottomRight, actualWidth, actualHeight);
		}

		/// <summary>
		///		Tipo de fractal
		/// </summary>
		public FractalType Type { get; private set; }

        /// <summary>
        ///     Generador de fractales
        /// </summary>
        private IFractalGenerator? Generator { get; set; }

		/// <summary>
		///		Canvas del generador
		/// </summary>
		private CanvasModel? Canvas { get; set; }

		/// <summary>
		///		Ancho
		/// </summary>
		public int Width { get; }

		/// <summary>
		///		Alto
		/// </summary>
		public int Height { get; }
    }
}
