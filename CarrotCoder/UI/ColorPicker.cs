using System.Numerics;
using ImGuiNET;

namespace CarrotCoder.UI;

public class ColorPicker : UIElement
{
    protected override void Begin()
    {
        base.Begin();
        ImGui.BeginTabBar("Test");
    }

    protected override void OnRender()
    {
        if (ImGui.BeginTabItem("Item 1"))
        {
            ImGui.Text("Hello");
            ImGui.EndTabItem();
        }

        if (ImGui.BeginTabItem("Item 2"))
        {
            ImGui.Text("World");
            ImGui.EndTabItem();
        }
    }

    protected override void End()
    {
        ImGui.EndTabBar();
    }
}