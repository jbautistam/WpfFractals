using System;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;

namespace Fractals
{
	/// <summary>
	///		Ventana principal de la aplicación
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			// Inicializa el componente
			InitializeComponent();
			// Inicializa el Datacontext
			ViewModel = new();
			DataContext = ViewModel;
		}

		/// <summary>
		///		Inicializa la ventana
		/// </summary>
		private void InitWindow()
		{
			// Inicializa el viewmodel de la ventana principal
			ViewModel.InitViewModel();
			// Inicializa los controles
			imgFractal.InitControl(ViewModel);
			imgFractal.DrawEnd += (_, _) => prgProgres.Value = 0;
			imgPallete.InitControl(ViewModel);
		}

		/// <summary>
		///		Dibuja la imagen
		/// </summary>
		private async Task DrawAsync()
		{
			if (imgPallete.ViewModel?.Pallete is not null)
			{
				imgFractal.Pallete = imgPallete.ViewModel.Pallete;
				await imgFractal.DrawAsync(CancellationToken.None);
			}
		}

		/// <summary>
		///		ViewModel
		/// </summary>
		internal ViewModels.MainViewModel ViewModel { get; }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			InitWindow();
		}

		private async void cmdDraw_Click(object sender, RoutedEventArgs e)
		{
			await DrawAsync();
		}

		private void cmdReset_Click(object sender, RoutedEventArgs e)
		{
			ViewModel.Reset();
		}
	}
}
