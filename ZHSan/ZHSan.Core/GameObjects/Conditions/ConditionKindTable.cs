using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.Conditions;

[DataContract]
public class ConditionKindTable
{
    [DataMember]
    public Dictionary<int, ConditionKind> ConditionKinds = new Dictionary<int, ConditionKind>();

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="conditionKind"></param>
    /// <returns></returns>
    public bool Add(ConditionKind conditionKind)
    {
        return ConditionKinds.TryAdd(conditionKind.ID, conditionKind);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Remove(int id)
    {
        return ConditionKinds.Remove(id);
    }

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ConditionKind Get(int id)
    {
        ConditionKinds.TryGetValue(id, out var kind);

        return kind;
    }

    public int Count => ConditionKinds.Count;

    public void Clear() => ConditionKinds.Clear();

    public GameObjectList GetConditionKindList() => [.. ConditionKinds.Values];
}