using System;
using System.Collections.ObjectModel;

namespace Fractals.ViewModels.ComboItems
{
	/// <summary>
	///		Colección observable de <see cref="ComboItem"/>
	/// </summary>
	public class ComboItemsCollection : ObservableCollection<ComboItem>
	{
		/// <summary>
		///		Añade un elemento
		/// </summary>
		public void Add(int? id, string text, object? tag = null)
		{
			Add(new ComboItem(id, text, tag));
		}
	}
}
