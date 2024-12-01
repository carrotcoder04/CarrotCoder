using System.Numerics;
using System.Runtime.CompilerServices;
using ImGuiNET;

namespace CarrotCoder.UI;

public class BaseUIObject : IRenderable
{
    private static int _id = 0;
    protected int Id { get;private set; }
    protected internal static ImGuiStylePtr Style { get; internal set; }
    public bool IsVisible { get; set; } = true;
    public virtual Vector2 Position { get; set; }
    protected BaseUIObject()
    {
        Id = _id++;
    }
    protected virtual void Begin() {}
    void IRenderable.Render()
    {
        if (IsVisible)
        {
            ImGui.PushID(Id);
            Begin();
            OnRender();
            End();
            ImGui.PopID();
        }
    }

    protected void CheckAndShowToolTip(string text)
    {
        if (ImGui.IsItemHovered())
        {
            ImGui.BeginTooltip();
            ImGui.Text(text);
            ImGui.EndTooltip();
        }
    }
    protected virtual void End() {}
    protected virtual void OnRender() {}
}