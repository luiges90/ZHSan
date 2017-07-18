using GameObjects;
using System;
using System.Collections.Generic;


namespace GameObjects.Influences
{

    public class InfluenceList : GameObjectList
    {
        public List<string> LoadFromString(InfluenceTable allInfluences, string influenceIDs)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = influenceIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Influence influence = null;
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (allInfluences.Influences.TryGetValue(int.Parse(strArray[i]), out influence))
                    {
                        base.Add(influence);
                    }
                    else
                    {
                        errorMsg.Add("影响ID" + strArray[i] + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("影响列表应为半型空格分隔的影响ID");
            }
            return errorMsg;
        }
    }
}

