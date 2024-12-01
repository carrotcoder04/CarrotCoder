using ImGuiNET;

namespace CarrotCoder.UI;

public class Label : UIElement
{
    public string Text { get; set; }
    
    public Label(string text = "Label")
    {
        Text = text;
    }
    protected override void Begin()
    {
        ImGui.BeginDisabled(true);
    }
    protected override void OnRender()
    {
        ImGui.Text(Text);
    }
    protected override void End()
    {
        ImGui.EndDisabled();
    }
}