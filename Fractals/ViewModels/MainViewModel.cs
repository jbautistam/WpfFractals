using System;

using Fractals.ViewModels.ComboItems;
using Fractals.Models.Fractals.Generators;

namespace Fractals.ViewModels
{
	/// <summary>
	///		ViewModel de la ventana principal
	/// </summary>
	public class MainViewModel : Base.BaseObservableObject
	{
		// Variables privadas
		private ComboViewModel? _comboFractalTypes;

		public MainViewModel()
		{
			FractalViewModel = new(this);
		}

		/// <summary>
		///		Inicializa el viewModel
		/// </summary>
		public void InitViewModel()
		{
			LoadCombos();
		}

		/// <summary>
		///		Carga los combos
		/// </summary>
		private void LoadCombos()
		{
			// Inicializa el combo
			ComboFractalTypes = new ComboViewModel(this);
			// Añade los valores
			ComboFractalTypes.AddItem((int) FractalGenerator.FractalType.Mandelbrot, "Mandelbrot set");
			ComboFractalTypes.AddItem((int) FractalGenerator.FractalType.Julia, "Julia set");
			// Selecciona un elemento
			ComboFractalTypes.SelectedId = (int) FractalGenerator.FractalType.Mandelbrot;
			// Cuando se cambia el elemento seleccionado, se cambia el tipo de fractal
			ComboFractalTypes.PropertyChanged += (sender, args) => 
													{
														if (args.PropertyName is not null &&
																args.PropertyName.Equals(nameof(ComboViewModel.SelectedItem), StringComparison.CurrentCultureIgnoreCase))
															FractalViewModel.UpdateFractal(GetSelectedFractalType());
													};
		}

		/// <summary>
		///		Obtiene el tipo de fractal seleccionado
		/// </summary>
		private FractalGenerator.FractalType GetSelectedFractalType()
		{
			FractalGenerator.FractalType type = FractalGenerator.FractalType.Mandelbrot;

				// Obtiene el tipo
				if (ComboFractalTypes?.SelectedId is not null)
					type = (FractalGenerator.FractalType) ComboFractalTypes.SelectedId;
				// Devuelve el tipo
				return type;
		}

		/// <summary>
		///		Inicializa los viewmodel
		/// </summary>
		public void Reset()
		{
			FractalViewModel.Reset();
		}

		/// <summary>
		///		ViewModel para el dibujo del fractal
		/// </summary>
		public FractalViewModel FractalViewModel { get; }

		/// <summary>
		///		Tipos de fractales
		/// </summary>
		public ComboViewModel? ComboFractalTypes
		{
			get { return _comboFractalTypes; }
			set { CheckObject(ref _comboFractalTypes, value); }
		}
	}
}
