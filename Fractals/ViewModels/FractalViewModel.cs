using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Fractals.Models.Fractals;
using Fractals.Models.Fractals.EventArguments;
using Fractals.Models.Fractals.Generators;

namespace Fractals.ViewModels
{
	/// <summary>
	///		ViewModel para el dibujo de fractales
	/// </summary>
	public class FractalViewModel : Base.BaseObservableObject
	{
		// Variables privadas
		private FractalGenerator _fractalGenerator;
		private int _progressActual, _progressTotal;
		private double _progressPercentage;
		private bool _canDraw = true;

		public FractalViewModel(MainViewModel mainViewModel)
		{
			MainViewModel = mainViewModel;
			_fractalGenerator = new(1_935, 1_047);
			_fractalGenerator.ProgressChanged += (sender, args) => UpdateProgress(args);
		}

		/// <summary>
		///		Inicializa el ViewModel
		/// </summary>
		public void Initialize()
		{
			// Inicializa el canvas
			Reset();
			// Indica que puede dibujar
			CanDraw = true;
		}

		/// <summary>
		///		Inicializa el ViewModel
		/// </summary>
		public void Reset()
		{
			_fractalGenerator.Reset();
		}

		/// <summary>
		///		Crea los valores del fractal
		/// </summary>
		public async Task<FractalPointsModel?> ComputeFractalAsync(CancellationToken cancellationToken)
		{
			FractalPointsModel? fractalPoints = null;

				// Indica que no se puede dibujar
				CanDraw = false;
				// Genera el fractal
				fractalPoints = await _fractalGenerator.ComputeFractalAsync(cancellationToken);
				// Indica que ya se puede dibujar de nuevo
				CanDraw = true;
				// Devuelve el fractal generado
				return fractalPoints;
		}

		/// <summary>
		///		Redimensiona el canvas de dibujo
		/// </summary>
		public void Resize(Point topLeft, Point bottomRight, double actualWidth, double actualHeight)
		{
			_fractalGenerator.Resize(topLeft, bottomRight, actualWidth, actualHeight);
		}

		/// <summary>
		///		Obtiene el generador de fractales adecuado
		/// </summary>
		public void UpdateFractal(FractalGenerator.FractalType type)
		{
			_fractalGenerator.Update(type);
		}

		/// <summary>
		///		Actualiza el progreso del dibujo
		/// </summary>
		private void UpdateProgress(ProgressEventArgs args)
		{
			Actual = args.Actual;
			Total = args.Total;
			ProgressPercentage = args.Percentage;
		}

		/// <summary>
		///		ViewModel principal
		/// </summary>
		public MainViewModel MainViewModel { get; }

		/// <summary>
		///		Indica si puede dibujar
		/// </summary>
		public bool CanDraw
		{
			get { return _canDraw; }
			set { CheckProperty(ref _canDraw, value); }
		}

		/// <summary>
		///		Progreso actual
		/// </summary>
		public int Actual
		{
			get { return _progressActual; }
			set { CheckProperty(ref _progressActual, value); }
		}

		/// <summary>
		///		Total del progreso
		/// </summary>
		public int Total
		{
			get { return _progressTotal; }
			set { CheckProperty(ref _progressTotal, value); }
		}

		/// <summary>
		///		Porcentaje de progreso
		/// </summary>
		public double ProgressPercentage
		{
			get { return _progressPercentage; }
			set { CheckProperty(ref _progressPercentage, value); }
		}
	}
}
