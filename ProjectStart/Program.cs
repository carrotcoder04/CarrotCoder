using CarrotCoder.UI;

public class Program
{
    public static void Main()
    {
        Window window = new Window();
        window.AddChild(new ColorPicker());
        UIController.AddWindow(window);
        Task task = UIController.Start();
        task.Wait();
    }
}
