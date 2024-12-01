using ImGuiNET;

namespace CarrotCoder.UI;

public class SliderInt : UIElement
{
    public string Label {get; set;} = "Slider int";
    private int _value = 0;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
        }
    }
    public int MinValue { get; set; } = 0;
    public int MaxValue { get; set; } = 1;
    
    protected override void OnRender()
    {
        ImGui.SliderInt(Label, ref _value, MinValue, MaxValue);
    }
}