namespace CustomControls.Controls.ToDoList
{
    public interface ITodoTask
    {
        string Content { get; set; }

        bool TaskFinished { get; set; }
    }
}