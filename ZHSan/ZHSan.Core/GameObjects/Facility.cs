using GameManager;
using GameObjects.ArchitectureDetail;
using GameObjects.Conditions;
using GameObjects.Influences;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects
{
    /// <summary>
    /// 设施
    /// </summary>
    [DataContract]
    public class Facility : GameObject
    {
        # region DataMember

        /// <summary>
        /// 种类ID
        /// </summary>
        [DataMember]
        public int KindID { get; private set; }

        /// <summary>
        /// 耐久
        /// </summary>
        [DataMember]
        public int Endurance { get; private set; }

        # endregion

        public Facility(int id, int kindId, int endurance)
        {
            ID = id;
            KindID = kindId;
            Endurance = endurance;
        }

        private FacilityKind _kind;

        private FacilityKind Kind
        {
            get
            {
                // 懒加载
                if (_kind == null)
                {
                    _kind = Session.Current.Scenario.GameCommonData.AllFacilityKinds.Get(KindID);
                }

                return _kind;
            }
        }

        /// <summary>
        /// 耐久下降
        /// </summary>
        /// <param name="decrement"></param>
        public void DecreaseEndurance(int decrement)
        {
            Endurance -= decrement;
        }

        /// <summary>
        /// 耐久恢复
        /// </summary>
        /// <param name="extraInc"></param>
        public void RecoverEndurance(int extraInc)
        {
            // 耐久小于上限时才需要恢复
            if (Endurance < EnduranceCeiling)
            {
                int increase = EnduranceCeiling / Kind.Days / 2 + extraInc;

                Endurance += Math.Max(1, increase);

                Endurance = Math.Max(Endurance, EnduranceCeiling);
            }
        }

        public void DoWork(Architecture architecture)
        {
            foreach (var influence in Kind.Influences.Values)
            {
                influence.DoWork(architecture);
            }
        }

        public int DaysText => Kind.Days * Session.Parameters.DayInTurn;

        public string Description => Kind.Description;

        public int EnduranceCeiling => Kind.Endurance;

        public InfluenceTable Influences => Kind.Influences;
        
        public int MaintenanceCost => Kind.MaintenanceCost;

        public new string Name => Kind.Name;

        public int PositionOccupied => Kind.PositionOccupied;

        public int ArchitectureLimit => Kind.ArchitectureLimit;

        public bool bukechaichu => Kind.bukechaichu;

        public double AIValue(Architecture architecture) => Kind.AIValue(architecture);

        public Dictionary<Condition, float> AIBuildConditionWeight => Kind.AIBuildConditionWeight;

        public int rongna => Kind.rongna;

        public bool IsProfitable => Kind.IsProfitable;
    }

    public class FacilityFactory
    {
        public Facility Create(int kindId)
        {
            var facilityKind = Session.Current.Scenario.GameCommonData.AllFacilityKinds.Get(kindId) 
                               ?? throw new Exception($"未找到id为{kindId}的设施种类，无法建造设施！");

            var id = Session.Current.Scenario.Facilities.GetFreeGameObjectID();

            var facility = new Facility(id, kindId, facilityKind.Endurance);

            return facility;
        }
    }
}