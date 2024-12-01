using CarrotCoder.UI.Exception;
using ClickableTransparentOverlay;
using ImGuiNET;

namespace CarrotCoder.UI;

public static class UIController
{
    public static int WindowCount => _windows.Count;
    public static bool IsRunning { get; private set; }
    private static RenderOverlay _renderOverlay;
    private static List<Window> _windows = new List<Window>();
    private static Queue<Window> _windowsToClose = new Queue<Window>();
    private static Queue<Window> _windowsToAdd = new Queue<Window>();

    public static Task Start()
    {
        IsRunning = true;
        _renderOverlay = new RenderOverlay();
        return _renderOverlay.Start();
    }

    private static void Prepare()
    {
        while (_windowsToClose.Count > 0)
        {
            Window window = _windowsToClose.Dequeue();
            _windows.Remove(window);
        }

        while (_windowsToAdd.Count > 0)
        {
            Window window = _windowsToAdd.Dequeue();
            _windows.Add(window);
        }
    }

    internal static void Render()
    {
        Prepare();
        if (WindowCount == 0)
        {
            Close();
            return;
        }
        BaseUIObject.Style = ImGui.GetStyle();
        foreach (var window in _windows)
        {
            if (window.IsVisible && window is IRenderable renderable)
            {
                renderable.Render();
            }

            if (!window.IsOpen)
            {
                RemoveWindow(window);
            }
        }
    }
    public static void Close()
    {
        IsRunning = false;
    }
    public static void AddWindow(Window window)
    {
        if (!window.IsOpen) return;
        if (_windows.Contains(window)) return;
        if (IsRunning)
        {
            if (_windowsToAdd.Contains(window)) return;
            _windowsToAdd.Enqueue(window);
        }
        else
        {
            _windows.Add(window);
        }
    }
    public static void AddWindow(params Window[] windows)
    {
        foreach (var window in windows)
        {
            AddWindow(window);
        }
    }
    internal static void RemoveWindow(Window window)
    {
        if (IsRunning)
        {
            if (_windowsToClose.Contains(window)) return;
            _windowsToClose.Enqueue(window);
        }
        else
        {
            _windows.Remove(window);
        }
    }
    public static Window GetWindow(string title)
    {
        return _windows.FirstOrDefault(x => x.Title == title);
    }
    public static Window GetWindow(int index)
    {
        return _windows[index];
    }
}