using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class zainanzhongleilei : GameObject
	{
        [DataMember]
        public int shijianxiaxian
        {
            get;
            set;
        }
        [DataMember]
        public int shijianshangxian
        {
            get;
            set;
        }
        [DataMember]
        public int renkoushanghai
        {
            get;
            set;
        }
        [DataMember]
        public int TroopDamage
        {
            get;
            set;
        }
        [DataMember]
        public int FundDamage
        {
            get;
            set;
        }
        [DataMember]
        public int FoodDamage
        {
            get;
            set;
        }
        [DataMember]
        public int OfficerDamage
        {
            get;
            set;
        }
        [DataMember]
        public int tongzhishanghai
        {
            get;
            set;
        }
        [DataMember]
        public int naijiushanghai
        {
            get;
            set;
        }
        [DataMember]
        public int nongyeshanghai
        {
            get;
            set;
        }
        [DataMember]
        public int shangyeshanghai
        {
            get;
            set;
        }
        [DataMember]
        public int jishushanghai
        {
            get;
            set;
        }
        [DataMember]
        public int minxinshanghai
        {
            get;
            set;
        }
	}
}
