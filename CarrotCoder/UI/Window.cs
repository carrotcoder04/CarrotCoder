using System.Numerics;
using CarrotCoder.UI.Enum;
using CarrotCoder.UI.Exception;
using ImGuiNET;
using Vulkan;

namespace CarrotCoder.UI;

public class Window : BaseUIObject
{
    #region Fields
    private bool _isSizeChanged;
    private Vector2 _size;
    private bool _isOpen = true;
    private List<UIElement> _childs = new List<UIElement>();
    private bool _isPositonChanged;
    private Vector2 _position;
    private Queue<UIElement> _childsToAdd = new Queue<UIElement>();
    private Queue<UIElement> _childsToRemove = new Queue<UIElement>();
    #endregion
    
    #region Properties
    public bool IsOpen => _isOpen;
    public string Title { get; set; } = "Window";
    public override Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            _isPositonChanged = true;
        }
    }
    public Vector2 Size
    {
        get => _size;
        set
        {
            _size = value;
            _isSizeChanged = true;
        }
    }
    public float Alpha { get; set; } = 1;
    public WindowFlags WindowFlags { get; set; } = WindowFlags.None;
    public int WindowRounding { get; set; } = 5;
    public Vector2 WindowPadding { get; set; } = new Vector2(10, 10);
    public int WindowBorderSize { get; set; } = 1;
    public Vector2 DisplayWindowPadding { get; set; } = Vector2.Zero;
    public Direction WindowMenuButtonPosition { get; set; } = Direction.Left;
    public Vector2 WindowTitleAlign { get; set; } = Vector2.Zero;
    public Vector2 WindowMinSize { get; set; } = new Vector2(200, 200);
    public Vector4 WindowBgColor { get; set; } = new Vector4(0.06f,0.06f,0.06f,0.94f);
    public Vector4 NavWindowingHighlightColor { get; set; } = new Vector4(1f,1f,1f,0.7f);
    public Vector4 NavWindowingDimBgColor { get; set; } = new Vector4(0.8f,0.8f,0.8f,0.2f);
    public Vector4 ModalWindowDimBgColor { get; set; } = new Vector4(0.8f,0.8f,0.8f,0.35f);
    #endregion
    
    public void AddChild(UIElement child)
    {
        if(child.Parent == this) return;
        if (UIController.IsRunning)
        {
            if (child.IsDestroy)
            {
                throw new ObjectHasDestroyedExcception();
            }
            if(_childsToAdd.Contains(child)) return;
            _childsToAdd.Enqueue(child);
        }
        else
        {
            if(_childs.Contains(child)) return;
            _childs.Add(child);
            child.SetParent(this);
        }
    }
    public void AddChild(params UIElement[] childs)
    {
        foreach (var child in childs)
        {
            AddChild(child);
        }
    }
    public void RemoveChild(UIElement child)
    {
        if (child.Parent != this) return;
        if (UIController.IsRunning)
        {
            if (_childsToRemove.Contains(child)) return;
            _childsToRemove.Enqueue(child);
        }
        else
        {
            if(!_childs.Contains(child)) return;
            _childs.Remove(child);
            child.SetParent(null);
        }
    }
    public T GetChild<T>() where T : UIElement
    {
        foreach (var child in _childs)
        {
            if (child is T t)
            {
                return t;
            }
        }
        return null;
    }
    public List<T> GetChilds<T>() where T : UIElement
    {
        var list = new List<T>();
        foreach (var child in _childs)
        {
            if (child is T t)
            {
                list.Add(t);
            }
        }
        return list;
    }
    public UIElement GetChild(int index)
    {
        return _childs[index];
    }
    private void Prepare()
    {
        while (_childsToRemove.Count > 0)
        {
            var child = _childsToRemove.Peek();
            _childs.Remove(child);
            child.SetParent(null);
            _childsToRemove.Dequeue();
        }
        while (_childsToAdd.Count > 0)
        {
            UIElement child = _childsToAdd.Peek();
            child.SetParent(this);
            _childs.Add(child);
            _childsToAdd.Dequeue();
        }
    }
    private void UpdateStyle()
    {
        Style.Alpha = Alpha;
        Style.WindowRounding = WindowRounding;
        Style.WindowPadding = WindowPadding;
        Style.WindowBorderSize = WindowBorderSize;
        Style.DisplayWindowPadding = DisplayWindowPadding;
        Style.WindowMenuButtonPosition = (ImGuiDir)WindowMenuButtonPosition;
        Style.WindowTitleAlign = WindowTitleAlign;
        Style.WindowMinSize = WindowMinSize;
        Style.Colors[(int)ImGuiCol.WindowBg] = WindowBgColor;
        Style.Colors[(int)ImGuiCol.NavWindowingHighlight] = NavWindowingHighlightColor;
        Style.Colors[(int)ImGuiCol.NavWindowingDimBg] = NavWindowingDimBgColor;
        Style.Colors[(int)ImGuiCol.ModalWindowDimBg] = ModalWindowDimBgColor;
    }
    protected override void Begin()
    {
        Prepare();
        UpdateStyle();
        if (_isSizeChanged)
        {
            ImGui.SetNextWindowSize(Size);
            _isSizeChanged = false;
        }
        if (_isPositonChanged)
        {
            ImGui.SetNextWindowPos(_position);
            _isPositonChanged = false;
        }
        ImGui.Begin(Title, ref _isOpen, (ImGuiWindowFlags)WindowFlags);
        _position = ImGui.GetWindowPos();
        _size = ImGui.GetWindowSize();
    }
    protected override void OnRender()
    {
        CheckAndShowToolTip(Title + ", Position: " + Position);
        foreach (var child in _childs)
        {
            if (child.IsVisible && child is IRenderable renderable)
            {
                renderable.Render();
            }
            if (child.IsDestroy)
            {
                RemoveChild(child);
            }
        }
    }
    protected override void End()
    {
        ImGui.End();
    }
    public void Close()
    {
        if (UIController.IsRunning)
        {
            _isOpen = false;
        }
        else
        {
            UIController.RemoveWindow(this);
        }
    }
}