using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractals.Controls.Fractal
{
    /// <summary>
    ///     Decorador que muestra un rectángulo de selección
    /// </summary>
    public class RubberAddorner : Adorner
    {
        // Eventos públicos
        public event EventHandler<RubberSelectionEventArgs>? RubberSelectionEnd;
        // Variables privadas
        private Point? _startPoint, _endPoint;
        private Rectangle _rubberband;
        private VisualCollection _visuals;
        private Canvas _adornerCanvas;

        public RubberAddorner(Image designerCanvas, Point? dragStartPoint) : base(designerCanvas)
        {
            // Guarda las propiedades
            ControlParent = designerCanvas;
            _startPoint = dragStartPoint;
            // Crea el canvas donde se va a crear el rectángulo
            _adornerCanvas = new()
                                {
                                    Background = Brushes.Transparent
                                };
            // Crea la colección de elementos visuales sobre el canvas donde se va a crear el rectángulo
			_visuals = new VisualCollection(this)
			                    {
				                    _adornerCanvas
			                    };
            // Crea el rectángulo que se va a dibujar
			_rubberband = new()
                                {
                                    Stroke = Brushes.Navy,
                                    StrokeThickness = 1,
                                    StrokeDashArray = new DoubleCollection(new double[] { 2 })
                                };
            // Añade el rectángulo al canvas
            _adornerCanvas.Children.Add(_rubberband);
        }

        /// <summary>
        ///     Pinta el rectágulo de selección
        /// </summary>
        private void PaintRubberband()
        {
            if (_startPoint is not null && _endPoint is not null)
            {
                double left = Math.Min(_startPoint.Value.X, _endPoint.Value.X);
                double top = Math.Min(_startPoint.Value.Y, _endPoint.Value.Y);
                double width = Math.Abs(_startPoint.Value.X - _endPoint.Value.X);
                double height = Math.Abs(_startPoint.Value.Y - _endPoint.Value.Y);

                    // Asigna el ancho y alto al rectángulo
                    _rubberband.Width = width;
                    _rubberband.Height = height;
                    // Asigna la posición al rectángulo
                    Canvas.SetLeft(_rubberband, left);
                    Canvas.SetTop(_rubberband, top);
            }
        }

        /// <summary>
        ///     Lanza el evento de fin de selección
        /// </summary>
        private void RaiseEventSelectionEnd()
        {
            if (_startPoint is not null && _endPoint is not null)
            {
                (Point start, Point end) = Swap(_startPoint.Value, _endPoint.Value);

                    // Lanza el evento
				    RubberSelectionEnd?.Invoke(this, new RubberSelectionEventArgs(new Rect(start, end)));
            }

            // Deja el punto de inicio como la parte superior izquierda del rectángulo
            (Point start, Point end) Swap(Point startPoint, Point endPoint)
            {
                // Cambia los puntos si es necesario
                if (startPoint.X > endPoint.X || startPoint.Y > endPoint.Y)
                {
                    Point temp = new Point(startPoint.X, startPoint.Y);

                        // Intercambia los valores
                        startPoint = endPoint;
                        endPoint = temp;
                }
                // Devuelve los valores
                return (startPoint, endPoint);
            }
		}

        /// <summary>
        ///     Trata el evento de movimiento del ratón
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Captura el ratón
                if (!IsMouseCaptured)
                    CaptureMouse();
                // Cambia el punto final
                _endPoint = e.GetPosition(this);
                // Pinta el rectángulo
                PaintRubberband();
                // Indica que se ha tratado el evento
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Trata el evento de soltar el ratón
        /// </summary>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            // Lanza el evento de fin de selección
            RaiseEventSelectionEnd();
            // Libera el ratón
            if (IsMouseCaptured)
                ReleaseMouseCapture();
            // Elimina el decorador
            if (Parent is AdornerLayer adornerLayer)
                adornerLayer.Remove(this);
        }

        /// <summary>
        ///     Sobrescribe el evento de cambio de tamaño
        /// </summary>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {  
            // Reorganiza el canvas
            _adornerCanvas.Arrange(new Rect(arrangeBounds));
            // Devuelve el nuevo tamaño
            return arrangeBounds;
        }

        /// <summary>
        ///     Obtiene un elemento visual
        /// </summary>
        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }

        /// <summary>
        ///     Número de elementos visuales
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return _visuals.Count; }
        }
        
        /// <summary>
        ///     Canvas al que se asocia el decorador
        /// </summary>
        public Image ControlParent { get; }
    }
}
