using UnityEngine;

namespace IO
{
    public abstract class AbstractTextInput: AbstractInput
    {
        public AbstractTextInput(GameActions gameActions) : base(gameActions)
        {
            this.Actions = gameActions;
        }
        
        public InputHandledBrailleTextObject Textbox;
    }
}

