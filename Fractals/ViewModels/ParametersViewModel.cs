using System;

namespace Fractals.ViewModels
{
	/// <summary>
	///		ViewModel del canvas
	/// </summary>
	public class ParametersViewModel : Base.BaseObservableObject
	{
		// Variables privadas
		private double _xMin, _yMin, _xMax, _yMax, _xCenter, _yCenter;
		private int _scape, _iterations;

		public ParametersViewModel(FractalViewModel fractalViewModel)
		{
			FractalViewModel = fractalViewModel;
		}

		/// <summary>
		///		Inicializa los controles
		/// </summary>
		internal void Initialize()
		{
			if (FractalViewModel.FractalGenerator.Canvas is not null)
			{
				XMin = FractalViewModel.FractalGenerator.Canvas.Parameters.XMin;
				YMin = FractalViewModel.FractalGenerator.Canvas.Parameters.YMin;
				XMax = FractalViewModel.FractalGenerator.Canvas.Parameters.XMax;
				YMax = FractalViewModel.FractalGenerator.Canvas.Parameters.YMax;
				XCenter = FractalViewModel.FractalGenerator.Canvas.Parameters.XCenter;
				YCenter = FractalViewModel.FractalGenerator.Canvas.Parameters.YCenter;
				Scape = FractalViewModel.FractalGenerator.Canvas.Parameters.Scape;
				Iterations = FractalViewModel.FractalGenerator.Canvas.Parameters.Iterations;
			}
		}

		/// <summary>
		///		Actualiza los parámetros del fractal
		/// </summary>
		internal void UpdateCanvas()
		{
			if (FractalViewModel.FractalGenerator.Canvas is not null)
			{
				FractalViewModel.FractalGenerator.Canvas.Parameters.XMin = XMin;
				FractalViewModel.FractalGenerator.Canvas.Parameters.YMin = YMin;
				FractalViewModel.FractalGenerator.Canvas.Parameters.XMax = XMax;
				FractalViewModel.FractalGenerator.Canvas.Parameters.YMax = YMax;
				FractalViewModel.FractalGenerator.Canvas.Parameters.XCenter = XCenter;
				FractalViewModel.FractalGenerator.Canvas.Parameters.YCenter = YCenter;
				FractalViewModel.FractalGenerator.Canvas.Parameters.Scape = Scape;
				FractalViewModel.FractalGenerator.Canvas.Parameters.Iterations = Iterations;
			}
		}

		/// <summary>
		///		ViewModel del fractal
		/// </summary>
		public FractalViewModel FractalViewModel { get; }

		/// <summary>
		///		X mínimo
		/// </summary>
		public double XMin
		{
			get { return _xMin; }
			set { CheckProperty(ref _xMin, value); }
		}

		/// <summary>
		///		Y mínimo
		/// </summary>
		public double YMin
		{
			get { return _yMin; }
			set { CheckProperty(ref _yMin, value); }
		}
		
		/// <summary>
		///		X máximo
		/// </summary>
		public double XMax
		{
			get { return _xMax; }
			set { CheckProperty(ref _xMax, value); }
		}
		
		/// <summary>
		///		Y máximo
		/// </summary>
		public double YMax
		{
			get { return _yMax; }
			set { CheckProperty(ref _yMax, value); }
		}

		/// <summary>
		///		X centro
		/// </summary>
		public double XCenter
		{
			get { return _xCenter; }
			set { CheckProperty(ref _xCenter, value); }
		}

		/// <summary>
		///		Y Centro
		/// </summary>
		public double YCenter
		{
			get { return _yCenter; }
			set { CheckProperty(ref _yCenter, value); }
		}

		/// <summary>
		///		Valor de escape
		/// </summary>
		public int Scape
		{
			get { return _scape; }
			set { CheckProperty(ref _scape, value); }
		}

		/// <summary>
		///		Iteraciones
		/// </summary>
		public int Iterations
		{
			get { return _iterations; }
			set { CheckProperty(ref _iterations, value); }
		}
	}
}
