using System;


namespace GameObjects
{

    public class GameMessage : GameObject
    {
        private string message1;
        private string message2;
        private string message3;
        private string message4;
        private string message5;

        public string Message1
        {
            get
            {
                return this.message1;
            }
            set
            {
                this.message1 = value;
            }
        }

        public string Message2
        {
            get
            {
                return this.message2;
            }
            set
            {
                this.message2 = value;
            }
        }

        public string Message3
        {
            get
            {
                return this.message3;
            }
            set
            {
                this.message3 = value;
            }
        }

        public string Message4
        {
            get
            {
                return this.message4;
            }
            set
            {
                this.message4 = value;
            }
        }

        public string Message5
        {
            get
            {
                return this.message5;
            }
            set
            {
                this.message5 = value;
            }
        }
    }
}

