using System;


namespace GameObjects
{

    public class GameMessageList : GameObjectList
    {
        public void AddMessageWithEvent(GameMessage message)
        {
            base.Add(message);
        }
    }
}

