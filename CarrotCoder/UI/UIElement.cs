using System.Numerics;
using ImGuiNET;

namespace CarrotCoder.UI;

public class UIElement : BaseUIObject
{
    private Vector2 _position;
    public bool AutoPosition { get; set; } = true;
    public BaseUIObject Parent { get; private set; }
    public bool IsDestroy { get; private set; }
    public void SetParent(BaseUIObject parent)
    {
        if (Parent == parent)
        {
            return;
        }
        if (Parent is Window oldWindow)
        {
            oldWindow.RemoveChild(this);
        }
        if (parent is Window newWindow)
        {
            newWindow.AddChild(this);
        }
        Parent = parent;
    }
    public sealed override Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            AutoPosition = false;
        }
    }
    public void Destroy()
    {
        IsDestroy = true;
    }
    protected override void Begin()
    {
        if (AutoPosition)
        {
            _position = ImGui.GetCursorPos();
        }
        else
        {
            ImGui.SetCursorPos(_position);
        }
    }
}