using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.SectionDetail
{
    [DataContract]
    public class SectionAIDetailTable
    {
        [DataMember]
        public Dictionary<int, SectionAIDetail> SectionAIDetails = new Dictionary<int, SectionAIDetail>();

        public bool AddSectionAIDetail(SectionAIDetail sectionAIDetail)
        {
            if (this.SectionAIDetails.ContainsKey(sectionAIDetail.ID))
            {
                return false;
            }
            this.SectionAIDetails.Add(sectionAIDetail.ID, sectionAIDetail);
            return true;
        }

        public void Clear()
        {
            this.SectionAIDetails.Clear();
        }

        public SectionAIDetail GetSectionAIDetail(int sectionAIDetailID)
        {
            SectionAIDetail detail = null;
            this.SectionAIDetails.TryGetValue(sectionAIDetailID, out detail);
            return detail;
        }

        public GameObjectList GetSectionAIDetailList()
        {
            GameObjectList list = new GameObjectList();
            foreach (SectionAIDetail detail in this.SectionAIDetails.Values)
            {
                list.Add(detail);
            }
            return list;
        }

        public GameObjectList GetSectionAIDetailsByConditions(SectionOrientationKind orientationKind, bool autoRun, bool valueOffensiveCampaign, bool allowOffensiveCampaign, bool allowMilitaryTransfer, bool valueRecruitment)
        {
            GameObjectList list = new GameObjectList();
            foreach (SectionAIDetail detail in this.SectionAIDetails.Values)
            {
                if (((((detail.OrientationKind == orientationKind) && (detail.AutoRun == autoRun)) && ((detail.ValueOffensiveCampaign == valueOffensiveCampaign) && (detail.AllowOffensiveCampaign == allowOffensiveCampaign))) && (detail.AllowMilitaryTransfer == allowMilitaryTransfer)) && (detail.ValueRecruitment == valueRecruitment))
                {
                    list.Add(detail);
                }
            }
            return list;
        }

        public GameObjectList GetSectionNoOrientationAutoAIDetailsByConditions(bool allowOffensiveCampaign, bool valueRecruitment)
        {
            GameObjectList list = new GameObjectList();
            foreach (SectionAIDetail detail in this.SectionAIDetails.Values)
            {
                if ((((detail.OrientationKind == SectionOrientationKind.无) && detail.AutoRun) && (detail.AllowOffensiveCampaign == allowOffensiveCampaign)) && (detail.ValueRecruitment == valueRecruitment))
                {
                    list.Add(detail);
                }
            }
            return list;
        }

        public void LoadFromString(SectionAIDetailTable allSectionAIDetails, string sectionAIDetailIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = sectionAIDetailIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            SectionAIDetail detail = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (allSectionAIDetails.SectionAIDetails.TryGetValue(int.Parse(strArray[i]), out detail))
                {
                    this.AddSectionAIDetail(detail);
                }
            }
        }

        public bool RemoveSectionAIDetail(int id)
        {
            if (!this.SectionAIDetails.ContainsKey(id))
            {
                return false;
            }
            this.SectionAIDetails.Remove(id);
            return true;
        }

        public string SaveToString()
        {
            string str = "";
            foreach (SectionAIDetail detail in this.SectionAIDetails.Values)
            {
                str = str + detail.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.SectionAIDetails.Count;
            }
        }
    }
}

