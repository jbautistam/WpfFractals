using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Fractals.Models.Palletes;
using Fractals.ViewModels;

namespace Fractals.Controls.Palletes
{
	/// <summary>
	///		Control para visualización de una paleta
	/// </summary>
	public partial class PalleteControl : UserControl
	{
		public PalleteControl()
		{
			InitializeComponent();
		}

		/// <summary>
		///		Inicializa el control
		/// </summary>
		public void InitControl(MainViewModel mainViewModel)
		{
			// Asigna el contexto del control
			DataContext = ViewModel = mainViewModel.PalletesViewModel;
			ViewModel.InitViewModel();
			// Añade el manejador de eventos
			ViewModel.Updated += (_, _) => Draw();
			// Dibuja la paleta por primera vez
			Draw();
		}

		/// <summary>
		///		Actualiza la paleta
		/// </summary>
		internal void Update()
		{
			if (ViewModel?.Pallete is not null)
				ViewModel.UpdatePallete();
		}

		/// <summary>
		///		Dibuja la paleta
		/// </summary>
		private void Draw()
		{
			if (ViewModel?.Pallete is not null)
				imgPallete.Source = CreateBitmap(ViewModel.Pallete);
		}

		/// <summary>
		///		Crea una imagen a partir de una paleta
		/// </summary>
		private ImageSource? CreateBitmap(PalleteModel pallete)
		{
			if (ActualWidth > 0 && ActualHeight > 0)
			{
				int width = (int) ActualWidth;
				int height = (int) ActualHeight;
				WriteableBitmap bitmap = new(width, height, 96, 96, PixelFormats.Bgr32, null);

					// Escribe los pixels de la paleta
					bitmap.WritePixels(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight),
										ConvertPallete(pallete, width, height, height > width), bitmap.BackBufferStride, 0);
					// Devuelve el bitmap
					return bitmap;
			}
			else
				return null;
		}

		/// <summary>
		///		Convierte los puntos de la paleta en colores
		/// </summary>
		private int[] ConvertPallete(PalleteModel pallete, int width, int height, bool isVertical)
		{
			int[] buffer = new int[width * height];
			int index = 0;

				// Convierte los puntos de la paleta en colores
				if (isVertical)
				{
					for (int x = 0; x < width; x++)
						for (int y = 0; y < height; y++)
							buffer[index++] = pallete.GetInt(x);
				}
				else
				{
					for (int y = 0; y < height; y++)
						for (int x = 0; x < width; x++)
							buffer[index++] = pallete.GetInt(x);
				}
				// Devuelve el buffer convertido
				return buffer;
		}

		/// <summary>
		///		ViewModel principal
		/// </summary>
		public PalletesViewModel? ViewModel { get; private set; }

		private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Draw();
		}
	}
}
