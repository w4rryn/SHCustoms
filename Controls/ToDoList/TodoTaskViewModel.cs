using System.ComponentModel;

namespace SHCustoms.Controls.ToDoList
{
    public class TodoTaskViewModel : INotifyPropertyChanged
    {
        private ITodoTask taskItem;

        public TodoTaskViewModel(ITodoTask taskItem)
        {
            TaskItem = taskItem;
            OnPropertyRaised("Content");
            OnPropertyRaised("IsChecked");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Content
        {
            get
            {
                return TaskItem.Content;
            }
            set
            {
                TaskItem.Content = value;
                OnPropertyRaised("Content");
            }
        }

        public bool IsChecked
        {
            get
            {
                return TaskItem.TaskFinished;
            }
            set
            {
                TaskItem.TaskFinished = value;
                OnPropertyRaised("IsChecked");
            }
        }

        public ITodoTask TaskItem
        {
            get
            {
                return taskItem;
            }
            set
            {
                taskItem = value;
                OnPropertyRaised("TaskItem");
                OnPropertyRaised("Content");
                OnPropertyRaised("IsChecked");
            }
        }

        private void OnPropertyRaised(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}