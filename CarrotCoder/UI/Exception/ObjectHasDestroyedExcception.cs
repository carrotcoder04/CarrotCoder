namespace CarrotCoder.UI.Exception;

public class ObjectHasDestroyedExcception : System.Exception
{
    public ObjectHasDestroyedExcception() : base( "Object has been destroyed")
    {
    }
}