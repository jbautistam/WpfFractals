using System;

namespace Fractals.ViewModels.ComboItems
{
	/// <summary>
	///		Elemento de un combo
	/// </summary>
	public class ComboItem
	{
		public ComboItem(int? id = null, string? text = null, object? tag = null)
		{
			Id = id;
			Text = text;
			Tag = tag;
		}

		/// <summary>
		///		ID del elemento
		/// </summary>
		public int? Id { get; set; }

		/// <summary>
		///		Texto del elemento
		/// </summary>
		public string? Text { get; set; }

		/// <summary>
		///		Objeto asociado al elemento
		/// </summary>
		public object? Tag { get; set; }
	}
}
