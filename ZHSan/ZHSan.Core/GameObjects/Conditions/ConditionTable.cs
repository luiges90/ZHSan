using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.Conditions;

[DataContract]
public class ConditionTable
{
    [DataMember]
    public Dictionary<int, Condition> Conditions = new Dictionary<int, Condition>();

    public ConditionTable() {}

    public ConditionTable(Dictionary<int, Condition> conditions)
    {
        Conditions = conditions;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public bool Add(Condition condition)
    {
        return Conditions.TryAdd(condition.ID, condition);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Remove(int id)
    {
        return Conditions.Remove(id);
    }

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Condition Get(int id)
    {
        Conditions.TryGetValue(id, out var condition);

        return condition;
    }

    public int Count => Conditions.Count;

    public void Clear() => Conditions.Clear();

    public IEnumerable<Condition> Values => Conditions.Values;

    public bool CheckCondition(Person person)
    {
        return Condition.CheckConditionList(Conditions.Values, person);
    }
}