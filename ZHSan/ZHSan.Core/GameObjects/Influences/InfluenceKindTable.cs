using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GameObjects.Influences;

[DataContract]
public class InfluenceKindTable
{
    [DataMember]
    public Dictionary<int, InfluenceKind> InfluenceKinds = new Dictionary<int, InfluenceKind>();

    /// <summary>
    ///  新增
    /// </summary>
    /// <param name="influenceKind"></param>
    /// <returns></returns>
    public bool Add(InfluenceKind influenceKind)
    {
        return InfluenceKinds.TryAdd(influenceKind.ID, influenceKind);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Remove(int id)
    {
        return InfluenceKinds.Remove(id);
    }

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public InfluenceKind Get(int id)
    {
        InfluenceKinds.TryGetValue(id, out var kind);

        return kind;
    }

    public int Count => InfluenceKinds.Count;

    public void Clear() => InfluenceKinds.Clear();

    public GameObjectList GetInfluenceKindList() => [.. InfluenceKinds.Values];

    public bool HasInfluenceKind(int id) => InfluenceKinds.ContainsKey(id);

    public bool HasTroopLeaderValidInfluenceKind => InfluenceKinds.Values.Any(x => x.TroopLeaderValid);
}