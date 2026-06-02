using IO;

public abstract class AbstractInput
{
    protected GameActions _actions = new(); 
    public abstract void Enable();
    public abstract void Disable();
}
