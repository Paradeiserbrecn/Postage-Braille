using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace IO
{
    public class ActionRebinder
    {
        private GameActions _actions;
        private RebindUI _rebindUI;
        

        public ActionRebinder(GameActions actions, RebindUI rebindUI)
        {
            _actions = actions;
            _rebindUI = rebindUI;
            
            StringBuilder actionsString = new();
            foreach (var action in _actions)
            {
                actionsString.Append(action + "\n");
            }
            Debug.Log(actionsString.ToString());
        }


        private void InitializeInputTypes()
        {
            
        } 
    }
}

