using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;



namespace GameObjects
{
    [DataContract]
    public class zainanlei : GameObject
	{
        [DataMember]
        public int DisasterType
        {
            get;
            set;
        }

        public zainanzhongleilei zainanzhonglei=new zainanzhongleilei() ;

        [DataMember]
        public int shengyutianshu
        {
            get;
            set;
        }
	}
}
