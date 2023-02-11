using System;
using System.Windows.Media;

using Fractals.ViewModels.ComboItems;
using Fractals.Models.Palletes;

namespace Fractals.ViewModels
{
	/// <summary>
	///		ViewModel para el manejo de paletas
	/// </summary>
	public class PalletesViewModel : Base.BaseObservableObject
	{
		// Enumerados públicos
		/// <summary>
		///		Tipo de paleta
		/// </summary>
		public enum PalleteType
		{
			Smooth,
			Gradient,
			Component
		}
		// Eventos públicos
		public event EventHandler? Updated;
		// Variables privadas
		private ComboViewModel? _comboPalletesTypes;

		public PalletesViewModel(MainViewModel mainViewModel)
		{
			MainViewModel = mainViewModel;
			Pallete = new Generators.Pallete.SmoothPalleteGenerator().Generate(1_000);
		}

		/// <summary>
		///		Inicializa el ViewModel
		/// </summary>
		public void InitViewModel()
		{
			// Carga los combos de la ventana
			LoadCombos();
			// Actualiza la paleta
			UpdatePallete();
		}

		/// <summary>
		///		Carga los combos
		/// </summary>
		private void LoadCombos()
		{
			// Inicializa el combo
			ComboPalleteTypes = new ComboViewModel(this);
			// Añade los valores
			ComboPalleteTypes.AddItem((int) PalleteType.Smooth, "Smooth");
			ComboPalleteTypes.AddItem((int) PalleteType.Gradient, "Gradient");
			ComboPalleteTypes.AddItem((int) PalleteType.Component, "Component");
			// Selecciona un elemento
			ComboPalleteTypes.SelectedId = (int) PalleteType.Smooth;
			// Cuando se cambia el elemento seleccionado, se cambia el tipo de fractal
			ComboPalleteTypes.PropertyChanged += (sender, args) => 
													{
														if (args.PropertyName is not null &&
																args.PropertyName.Equals(nameof(ComboViewModel.SelectedItem), StringComparison.CurrentCultureIgnoreCase))
															UpdatePallete();
													};
		}

		/// <summary>
		///		Actualiza la paleta
		/// </summary>
		public void UpdatePallete()
		{
			// Modifica la paleta
			switch ((PalleteType) (ComboPalleteTypes?.SelectedId ?? 0))
			{
				case PalleteType.Gradient:
						Pallete = new Generators.Pallete.GradiantPalleteGenerator().Generate(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 255, 255, 0), 
																							 MainViewModel.FractalViewModel.ParametersViewModel.Iterations);
					break;
				case PalleteType.Component:
						Pallete = new Generators.Pallete.ComponentePalleteGenerator().Generate(false, false, true, 
																							   MainViewModel.FractalViewModel.ParametersViewModel.Iterations);
					break;
				default:
						Pallete = new Generators.Pallete.SmoothPalleteGenerator().Generate(MainViewModel.FractalViewModel.ParametersViewModel.Iterations);
					break;
			}
			// Lanza el evento de modificación
			Updated?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		///		ViewModel principal
		/// </summary>
		public MainViewModel MainViewModel { get; }

		/// <summary>
		///		Paleta actual
		/// </summary>
		public PalleteModel Pallete { get; private set; }

		/// <summary>
		///		Tipos de fractales
		/// </summary>
		public ComboViewModel? ComboPalleteTypes
		{
			get { return _comboPalletesTypes; }
			set { CheckObject(ref _comboPalletesTypes, value); }
		}
	}
}
