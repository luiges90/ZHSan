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
        public int zainanleixing
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

        public string SavezainantoString()
        {
            string result = "";
            result = zainanleixing.ToString() + " " + shengyutianshu.ToString()+" ";
            return result;
        }
    }
}
