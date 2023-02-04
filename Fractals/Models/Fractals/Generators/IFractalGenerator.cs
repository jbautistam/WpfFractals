using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fractals.Models.Fractals.Generators
{
	/// <summary>
	///		Interface para los generadores de fractales
	/// </summary>
	public interface IFractalGenerator
	{
		/// <summary>
		///		Obtiene los parámetros predeterminados
		/// </summary>
		ParametersModel GetDefault();

		/// <summary>
		///		Genera el fractal de forma asíncrona
		/// </summary>
		Task<FractalPointsModel> GenerateAsync(CanvasModel canvas, CancellationToken cancellationToken);
	}
}
