using IO;

public abstract class AbstractInput
{
    protected GameActions Actions;

    protected AbstractInput(GameActions actions)
    {
        Actions = actions;
    }

    public abstract void Enable();
    public abstract void Disable();
}