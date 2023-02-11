using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Fractals.Models.Fractals;
using Fractals.Models.Palletes;

namespace Fractals.Controls.Fractal
{
    /// <summary>
    ///		Control para visualización de un fractal
    /// </summary>
    public partial class FractalImage : UserControl
	{
		// Eventos públicos
		public event EventHandler? DrawEnd;
        // Variables privadas
        private Point? dragStartPoint = null;

		public FractalImage()
		{
			InitializeComponent();
		}

		/// <summary>
		///		Inicializa el control
		/// </summary>
		public void InitControl(ViewModels.MainViewModel mainViewModel)
		{
			DataContext = ViewModel = mainViewModel.FractalViewModel;
			ViewModel.Initialize();
		}

		/// <summary>
		///		Dibuja el fractal sobre la imagen
		/// </summary>
		internal async Task DrawAsync(CancellationToken cancellationToken)
		{
			if (ViewModel is not null && ViewModel.CanDraw && Pallete is not null)
			{
				// Asigna el bitmap a la imagen
				imgFractal.Source = CreateBitmap(await ViewModel.ComputeFractalAsync(cancellationToken), Pallete);
				// Lanza el evento de final de dibujo
				DrawEnd?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		///		Crea un bitmap a partir de un fractal
		/// </summary>
		private WriteableBitmap CreateBitmap(FractalPointsModel? fractal, PalleteModel pallete)
		{
			if (fractal is not null)
			{
				WriteableBitmap bitmap = new(fractal.Canvas.Width, fractal.Canvas.Height, 96, 96, PixelFormats.Bgr32, null);

					// Escribe los pixels del fractal
					bitmap.WritePixels(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight),
									   ConvertFractal(fractal, pallete), bitmap.BackBufferStride, 0);
					// Devuelve el bitmap
					return bitmap;
			}
			else
				return new(100, 100, 96, 96, PixelFormats.Bgr32, null);;
		}

		/// <summary>
		///		Convierte los puntos del fractal en colores para la imager
		/// </summary>
		private int[] ConvertFractal(FractalPointsModel fractal, PalleteModel pallete)
		{
			int[] buffer = new int[fractal.Canvas.Width * fractal.Canvas.Height];
			int index = 0;

				// Convierte los puntos del fractal en colores
				for (int y = 0; y < fractal.Canvas.Height; y++)
					for (int x = 0; x < fractal.Canvas.Width; x++)
						buffer[index++] = pallete.GetInt(fractal.Points[x, y]);
				// Devuelve el buffer convertido
				return buffer;
		}

		/// <summary>
		///		Trata la selección en la banda
		/// </summary>
		private async Task TreatRuberSelectionAsync(Rect rectangle)
		{
			if (ViewModel is not null)
			{
				// Redimensiona las coordenadas
				ViewModel.Resize(rectangle.TopLeft, rectangle.BottomRight, imgFractal.ActualWidth, imgFractal.ActualHeight);
				// Dibuja de nuevo
				await DrawAsync(CancellationToken.None);
			}
		}

		private void imgFractal_MouseDown(object sender, MouseButtonEventArgs e)
		{
            // Selecciona el punto de inicio
            dragStartPoint = new Point?(e.GetPosition(imgFractal));
            // Indica que se está manejando el evento
            e.Handled = true;
		}

		private void imgFractal_MouseMove(object sender, MouseEventArgs e)
		{
            // Si seguimos pulsando el botón izquierdo
            if (e.LeftButton != MouseButtonState.Pressed)
                dragStartPoint = null;
            // Si teníamos un punto de inicio
            if (dragStartPoint is not null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(imgFractal);

                    // Si tenemos una capa decoradora
                    if (adornerLayer is not null)
                    {
                        RubberAddorner adorner = new RubberAddorner(imgFractal, dragStartPoint);

							// Crea el evento de selección
							adorner.RubberSelectionEnd += async (sender, args) => await TreatRuberSelectionAsync(args.Rectangle);
                            // Si tenemos el decorador de la banda
                            if (adorner is not null)
                                adornerLayer.Add(adorner);
                    }
                    // Indica que se está manejando el evento
                    e.Handled = true;
            }
		}

		/// <summary>
		///		ViewModel
		/// </summary>
		internal ViewModels.FractalViewModel? ViewModel { get; private set; }

		/// <summary>
		///		Paleta
		/// </summary>
		public PalleteModel? Pallete { get; set; }
	}
}