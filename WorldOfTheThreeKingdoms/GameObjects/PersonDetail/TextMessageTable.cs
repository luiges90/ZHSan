using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class TextMessageTable
	{
        [DataMember]
        public Dictionary<KeyValuePair<int, TextMessageKind>, List<string>> textMessages = new Dictionary<KeyValuePair<int, TextMessageKind>, List<string>>();

        public Dictionary<KeyValuePair<int, TextMessageKind>, List<string>> GetAllMessages()
        {
            return textMessages;
        }

        public bool AddTextMessages(int pid, TextMessageKind kind, List<string> messages)
        {
            KeyValuePair<int, TextMessageKind> key = new KeyValuePair<int, TextMessageKind>(pid, kind);
            if (this.textMessages.ContainsKey(key) || messages.Count == 0)
            {
                return false;
            }
            this.textMessages.Add(key, new List<string>(messages));
            return true;
        }

        public void Clear()
        {
            this.textMessages.Clear();
        }

        public List<string> GetTextMessage(int pid, TextMessageKind kind)
        {
            KeyValuePair<int, TextMessageKind> key = new KeyValuePair<int, TextMessageKind>(pid, kind);
            if (this.textMessages.ContainsKey(key))
            {
                return this.textMessages[key];
            }
            else
            {
                return new List<string>();
            }
        }

        public int Count
        {
            get
            {
                return this.textMessages.Count;
            }
        }
	}
}
