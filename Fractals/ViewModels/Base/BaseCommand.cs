using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Fractals.ViewModels.Base
{
	/// <summary>
	///		Clase base para los comandos
	/// </summary>
	public class BaseCommand : ICommand
	{ 
		// Eventos públicos
		public event EventHandler? CanExecuteChanged;
		// Variables privadas
		private readonly Action<object>? _executeMethod = null;
		private readonly Predicate<object>? _canExecuteMethod = null;

		public BaseCommand(Action<object>? execute, Predicate<object>? canExecute = null,
						   INotifyPropertyChanged? source = null, string? propertyName = null) : this(null, execute, canExecute, source, propertyName)
		{
		}

		public BaseCommand(string? caption, Action<object>? execute, Predicate<object>? canExecute = null,
						   INotifyPropertyChanged? source = null, string? propertyName = null)
		{
			Caption = caption;
			_executeMethod = execute;
			_canExecuteMethod = canExecute;
			if (source != null)
				AddListener(source, propertyName);
		}

		/// <summary>
		///		Ejecuta un comando
		/// </summary>
		public void Execute(object? parameter)
		{
			_executeMethod?.Invoke(parameter);
		}

		/// <summary>
		///		Comprueba si se puede ejecutar un comando
		/// </summary>
		public bool CanExecute(object? parameter)
		{
			if (_canExecuteMethod != null)
				return _canExecuteMethod(parameter);
			else
				return true;
		}

		/// <summary>
		///		Añade un listener de eventos al comando para un nombre de propiedad
		/// </summary>
		public BaseCommand AddListener(INotifyPropertyChanged source, string? propertyName)
		{ 
			// Añade el manejador de eventos
			source.PropertyChanged += (sender, args) => OnCanExecuteChanged();
			// Devuelve este objeto (permite un interface fluent)
			return this;
		}

		/// <summary>
		///		Rutina a la que se llama para lanzar el evento de modificación de CanExecute
		/// </summary>
		public void OnCanExecuteChanged()
		{
			try
			{
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex) 
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		/// <summary>
		///		Título del comando
		/// </summary>
		public string? Caption { get; }
	}
}
