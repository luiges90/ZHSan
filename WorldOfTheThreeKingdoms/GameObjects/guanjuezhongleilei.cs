using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;



namespace GameObjects
{
    [DataContract]
    public class guanjuezhongleilei : GameObject
	{
        [DataMember]
        public int shengwangshangxian
        {
            get;
            set;
        }
        [DataMember]
        public int xuyaogongxiandu
        {
            get;
            set;
        }
        [DataMember]
        public bool ShowDialog
        {
            get;
            set;
        }
        [DataMember]
        public int xuyaochengchi
        {
            get;
            set;
        }
        [DataMember]
        public int Loyalty
        {
            get;
            set;

        }
        
	}
}
