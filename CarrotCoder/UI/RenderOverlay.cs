using ClickableTransparentOverlay;

namespace CarrotCoder.UI;

internal class RenderOverlay : Overlay
{
    protected override void Render()
    {
         UIController.Render();
         if (!UIController.IsRunning)
         {
             ProgramEnded();
         }
    }
    private void ProgramEnded()
    {
        Close();
    }
}