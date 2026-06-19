using IO;

public abstract class AbstractInput
{
    protected GameActions Actions = new(); 
    public abstract void Enable();
    public abstract void Disable();
}
