using ImGuiNET;

namespace CarrotCoder.UI;

public class SliderFloat : UIElement
{
    public string Label {get; set;} = "Slider float";
    private float _value = 0;

    public float Value
    {
        get => _value;
        set
        {
            _value = value;
        }
    }

    public float MinValue { get; set; } = 0;
    public float MaxValue { get; set; } = 1;
    
    protected override void OnRender()
    {
        ImGui.SliderFloat(Label, ref _value, MinValue, MaxValue);
    }
}