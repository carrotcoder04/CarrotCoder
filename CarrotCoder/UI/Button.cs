using ImGuiNET;

namespace CarrotCoder.UI;

public class Button : UIElement
{
    public string Text { get; set;}
    public Action OnClick { get; set; }
    public bool Enable { get; set; } = true;
    public Button(string text = "Button", Action onClick = null)
    {
        Text = text;
        OnClick = onClick;
    }

    protected override void Begin() 
    {
        base.Begin();
        ImGui.BeginDisabled(!Enable);
    }

    protected override void OnRender()
    {
        if (ImGui.Button(Text))
        {
            OnClick?.Invoke();
        }
    }
    
    protected override void End()
    {
        ImGui.EndDisabled();
    }
}