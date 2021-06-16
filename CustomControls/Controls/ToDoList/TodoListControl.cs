using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using CustomControls.Common;

namespace CustomControls.Controls.ToDoList
{
	public class TodoListControl : UserControl, IComponentConnector
	{
		public static readonly DependencyProperty HasBrowseProperty = DependencyProperty.Register("HasBrowse", typeof(bool), typeof(TodoListControl), new PropertyMetadata(false));

		public static readonly DependencyProperty NewButtonContentProperty = DependencyProperty.Register("NewButtonContent", typeof(string), typeof(TodoListControl), new PropertyMetadata("New Item"));

		public static readonly DependencyProperty NewItemCommandProperty = DependencyProperty.Register("NewItemCommand", typeof(ICommand), typeof(TodoListControl), newItemListCommandMetadata);

		public static readonly DependencyProperty TextBoxWidthProperty = DependencyProperty.Register("TextBoxWidth", typeof(int), typeof(TodoListControl), new PropertyMetadata(400));

		public static readonly DependencyProperty TodoBrowseCommandProperty = DependencyProperty.Register("TodoBrowseCommand", typeof(ICommand), typeof(TodoListControl), todoBrowseCommandMetadata);

		public static readonly DependencyProperty TodoListItemSourceProperty = DependencyProperty.Register("TodoListItemSource", typeof(ObservableCollection<TodoTaskViewModel>), typeof(TodoListControl), todoItemListSourceMetadata);

		private static readonly ICommand defaultNewItemCommand = new RelayCommand(delegate
		{
		}, (object o) => false);

		private static readonly PropertyMetadata newItemListCommandMetadata = new PropertyMetadata(defaultNewItemCommand, OnNewItemCommandChanged);

		private static readonly PropertyMetadata todoBrowseCommandMetadata = new PropertyMetadata(todoBrowseDefault, OnTodoBrowseCommandChanged);

		private static readonly ICommand todoBrowseDefault = new RelayCommand(delegate
		{
		}, (object o) => false);

		private static readonly ObservableCollection<TodoTaskViewModel> todoItemListSourceDefault = new ObservableCollection<TodoTaskViewModel>();

		private static readonly PropertyMetadata todoItemListSourceMetadata = new PropertyMetadata(todoItemListSourceDefault, OnTodoItemListChanged);

		internal TodoListControl todo_control;

		private bool _contentLoaded;

		public bool HasBrowse
		{
			get
			{
				return (bool)GetValue(HasBrowseProperty);
			}
			set
			{
				SetValue(HasBrowseProperty, value);
			}
		}

		public string NewButtonContent
		{
			get
			{
				return (string)GetValue(NewButtonContentProperty);
			}
			set
			{
				SetValue(NewButtonContentProperty, value);
			}
		}

		public ICommand NewItemCommand
		{
			get
			{
				return (ICommand)GetValue(NewItemCommandProperty);
			}
			set
			{
				SetValue(NewItemCommandProperty, value);
			}
		}

		public ICommand RemoveItemCommand { get; }

		public int TextBoxWidth
		{
			get
			{
				return (int)GetValue(TextBoxWidthProperty);
			}
			set
			{
				SetValue(TextBoxWidthProperty, value);
			}
		}

		public ICommand TodoBrowseCommand
		{
			get
			{
				return (ICommand)GetValue(TodoBrowseCommandProperty);
			}
			set
			{
				SetValue(TodoBrowseCommandProperty, value);
			}
		}

		public ObservableCollection<TodoTaskViewModel> TodoListItemSource
		{
			get
			{
				return (ObservableCollection<TodoTaskViewModel>)GetValue(TodoListItemSourceProperty);
			}
			set
			{
				SetValue(TodoListItemSourceProperty, value);
			}
		}

		public TodoListControl()
		{
			InitializeComponent();
			TodoBrowseCommand = todoBrowseDefault;
			RemoveItemCommand = new RelayCommand(RemoveItemExecute);
		}

		private static void OnNewItemCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			d.SetValue(NewItemCommandProperty, e.NewValue);
		}

		private static void OnTodoBrowseCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			d.SetValue(TodoBrowseCommandProperty, e.NewValue);
		}

		private static void OnTodoItemListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			d.SetValue(TodoListItemSourceProperty, e.NewValue);
		}

		private void RemoveItemExecute(object obj)
		{
			TodoListItemSource.Remove(obj as TodoTaskViewModel);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/CustomControls;component/controls/todolist/todolistcontrol.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				todo_control = (TodoListControl)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
}
