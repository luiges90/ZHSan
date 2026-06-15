using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GameObjects.Influences;

[DataContract]
public class InfluenceTable
{
    [DataMember]
    public Dictionary<int, Influence> Influences = new();

    public InfluenceTable() {}

    public InfluenceTable(Dictionary<int, Influence> influences)
    {
        Influences = influences;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="influence"></param>
    /// <returns></returns>
    public bool Add(Influence influence)
    {
        return Influences.TryAdd(influence.ID, influence);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Remove(int id)
    {
        return Influences.Remove(id);
    }

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Influence Get(int id)
    {
        Influences.TryGetValue(id, out var influence);

        return influence;
    }

    public void Clear() => Influences.Clear();

    public int Count => Influences.Count;

    public IEnumerable<Influence> Values => Influences.Values;

    public bool HasInfluence(int influenceID) => Influences.ContainsKey(influenceID);

    public bool HasInfluenceKind(int id) => Influences.Values.Any(x => x.Kind.ID == id);

    public IEnumerable<Influence> GetInfluenceByKind(int kindId)
    {
        var result = new List<Influence>();

        foreach (var influence in Influences.Values)
        {
            if (influence.Kind.ID == kindId)
            {
                result.Add(influence);
            }
        }
        
        return result;
    }

    public bool HasTroopLeaderValidInfluence => Influences.Values.Any(x => x.TroopLeaderValid);


    public void ApplyInfluence(Architecture architecture, Applier applier, int applierID)
    {
        foreach (var influence in Influences.Values)
        {
            influence.ApplyInfluence(architecture, applier, applierID);
        }
    }

    public void ApplyInfluence(Faction faction, Applier applier, int applierID)
    {
        foreach (var influence in Influences.Values)
        {
            influence.ApplyInfluence(faction, applier, applierID);
        }
    }

    public void ApplyInfluence(Person person, Applier applier, int applierID)
    {
        bool flag = false;
        bool flag2 = false;
        foreach (var influence in Influences.Values)
        {
            if ((influence.Type != InfluenceType.前提) && (influence.Type != InfluenceType.多选一))
            {
                if (!flag || flag2)
                {
                    influence.ApplyInfluence(person, applier, applierID);
                }
                continue;
            }
            if (!(flag || (influence.Type != InfluenceType.多选一)))
            {
                flag = true;
            }
            if (influence.IsVaild(person))
            {
                if (influence.Type == InfluenceType.多选一)
                {
                    flag2 = true;
                    continue;
                }
            }
            else if (influence.Type == InfluenceType.前提)
            {
                break;
            }
        }
    }

    public void DirectlyApplyInfluence(Troop troop, Applier applier, int applierID)
    {
        foreach (var influence in Influences.Values)
        {
            influence.ApplyInfluence(troop, applier, applierID);
        }
    }

    public void DirectlyPurifyInfluence(Troop troop, Applier applier, int applierID)
    {
        foreach (var influence in Influences.Values)
        {
            influence.PurifyInfluence(troop, applier, applierID);
        }
    }

    public void PurifyInfluence(Architecture architecture, Applier applier, int applierID)
    {
        foreach (var influence in Influences.Values)
        {
            influence.PurifyInfluence(architecture, applier, applierID);
        }
    }

    public void PurifyInfluence(Faction faction, Applier applier, int applierID)
    {
        foreach (var influence in Influences.Values)
        {
            influence.PurifyInfluence(faction, applier, applierID);
        }
    }

    public void PurifyInfluence(Person p, Applier applier, int applierID)
    {
        foreach (var influence in Influences.Values)
        {
            influence.PurifyInfluence(p, applier, applierID);
        }
    }
}