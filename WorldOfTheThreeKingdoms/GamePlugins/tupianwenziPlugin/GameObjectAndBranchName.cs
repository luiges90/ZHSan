using GameGlobal;
using GameObjects;
using PluginInterface;
using System;
using System.Collections.Generic;
using GameFreeText;

namespace tupianwenziPlugin
{

    internal class GameObjectAndBranchName
    {
        internal string branchName;
        internal IConfirmationDialog iConfirmationDialog;
        internal GameDelegates.VoidFunction NoFunction;
        internal Person person;
        internal List<SimpleText> texts = new List<SimpleText>();
        internal GameDelegates.VoidFunction YesFunction;

        internal GameObjectAndBranchName(GameObject   p,  List<SimpleText> list, string name, IConfirmationDialog confirmationDialog, GameDelegates.VoidFunction yesFunction, GameDelegates.VoidFunction noFunction, string TryToShowString = "")
        {
            this.person = p as Person ;
            this.texts.AddRange(list);
            this.branchName = name;
            this.iConfirmationDialog = confirmationDialog;
            this.YesFunction = yesFunction;
            this.NoFunction = noFunction;
            this.TryToShowString = TryToShowString;
        }

        public string TryToShowString = "";
    }
}

