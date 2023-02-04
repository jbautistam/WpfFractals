using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Fractals.Models.Palletes;

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
		///		Dibuja la paleta
		/// </summary>
		private void InitControl()
		{
			// Crea la paleta
			Pallete = new Generators.Pallete.SmoothPalleteGenerator().Generate(1_000);
			// Pallete = new Generators.Pallete.GradiantPalleteGenerator().Generate(Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 255, 255, 0), 1_000);
			// Pallete = new Generators.Pallete.ComponentePalleteGenerator().Generate(false, false, true, 1_000);
			// Dibuja la paleta
			Draw();
		}

		/// <summary>
		///		Dibuja la paleta
		/// </summary>
		private void Draw()
		{
			if (Pallete is not null)
				imgPallete.Source = CreateBitmap(Pallete);
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

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			InitControl();
		}

		/// <summary>
		///		Paleta
		/// </summary>
		public PalleteModel? Pallete { get; private set; }

		private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Draw();
		}
	}
}
