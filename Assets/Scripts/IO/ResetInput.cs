using Braille;
using UnityEngine.InputSystem;

namespace IO
{
    public class ResetInput : AbstractInput
    {
        public ResetInput(GameActions gameActions) : base(gameActions)
        {
            this.Actions = gameActions;
        }

        public override void Enable()
        {
            Actions.ResetInput.reset.started += OnReset;
            Actions.ResetInput.Enable();
        }

        public override void Disable()
        {
            Actions.ResetInput.reset.started -= OnReset;
            Actions.ResetInput.Disable();
        }

        private void OnReset(InputAction.CallbackContext callbackContext)
        {
            ActionRebinder.ResetRebinds();
            IOEventManager.AssistiveOutput("Tastenbelegung zurückgesetzt", AssistiveOutput.OutputType.Both);
        }
    }
}
