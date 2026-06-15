using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace GameObjects.Influences;

[DataContract]
public class InfluenceKind : GameObject
{
    #region DataMember

    /// <summary>
    /// 种类
    /// </summary>
    [DataMember]
    public InfluenceType Type { get; set; }

    /// <summary>
    /// 战斗
    /// </summary>
    [DataMember]
    public bool Combat { get; set; }

    /// <summary>
    /// 武将AI值
    /// </summary>
    [DataMember]
    public float AIPersonValue { get; set; }

    /// <summary>
    /// 武将AI值乘幂
    /// </summary>
    [DataMember]
    public float AIPersonValuePow { get; set; }

    /// <summary>
    /// 主将有效
    /// </summary>
    [DataMember]
    public bool TroopLeaderValid { get; set; }

    #endregion

    private bool AppliesToArchitecture => Type == InfluenceType.建筑 || Type == InfluenceType.建筑战斗;
    private bool AppliesToTroop => Type == InfluenceType.战斗 || Type == InfluenceType.建筑战斗;
    private bool AppliesToPerson => Type == InfluenceType.个人;
    private bool AppliesToFaction => Type == InfluenceType.势力;

    public virtual void DoWork(Influence influence, Architecture architecture)
    {
    }

    public virtual int GetCredit(Influence influence, Troop source, Troop destination)
    {
        return 0;
    }

    public virtual int GetCreditWithPosition(Troop source, out Point? position)
    {
        position = new Point(0, 0);
        return 0;
    }

    public virtual bool IsVaild(Influence influence, Person person)
    {
        return true;
    }

    public virtual bool IsVaild(Influence influence, Troop troop)
    {
        return true;
    }

    public virtual double AIFacilityValue(Influence influence, Architecture arch)
    {
        return 0;
    }

    #region 应用影响

    public virtual void ApplyInfluenceKind(Influence influence, Architecture arch) {}

    public virtual void ApplyInfluenceKind(Influence influence, Faction faction) {}

    public virtual void ApplyInfluenceKind(Influence influence, Person person) {}

    public virtual void ApplyInfluenceKind(Influence influence, Troop troop) {}

    private bool TryApplyArchitecture(Influence influence, Architecture arch, Applier applier, int id)
    {
        return influence.ApplyArchitectures.Add(new ApplyArchitecture(arch, applier, id));
    }

    private bool TryApplyPerson(Influence influence, Person person, Applier applier, int id)
    {
        return influence.ApplyPersons.Add(new ApplyPerson(person, applier, id));
    }

    private bool TryApplyTroop(Influence influence, Troop troop, Applier applier, int id)
    {
        return influence.ApplyTroops.Add(new ApplyTroop(troop, applier, id));   
    }

    private bool AllowsRepeatedTroopApply => (ID >= 390 && ID <= 399) || ID == 720 || ID == 721;

    private bool TryApplyFaction(Influence influence, Faction faction, Applier applier, int id)
    {
        return influence.ApplyFactions.Add(new ApplyFaction(faction, applier, id));
    }

    private void ApplyToArchitecture(Influence influence, Architecture arch, Applier applier, int id)
    {
        if (TryApplyArchitecture(influence, arch, applier, id))
        {
            ApplyInfluenceKind(influence, arch);
        }
    }

    private void ApplyToPerson(Influence influence, Person person, Applier applier, int id)
    {
        if (TryApplyPerson(influence, person, applier, id))
        {
            ApplyInfluenceKind(influence, person);
        }
    }

    private void ApplyToTroop(Influence influence, Troop troop, Applier applier, int id)
    {
        if (TryApplyTroop(influence, troop, applier, id) || AllowsRepeatedTroopApply)
        {
            troop.InfluencesApplying.Add(influence);
            ApplyInfluenceKind(influence, troop);
        }
    }

    private void ApplyToFaction(Influence influence, Faction faction, Applier applier, int id)
    {
        if (TryApplyFaction(influence, faction, applier, id))
        {
            ApplyInfluenceKind(influence, faction);
        }
    }

    private void ApplyToPersons(Influence influence, PersonList persons, Applier applier, int applierID)
    {
        foreach (Person person in persons)
        {
            ApplyFromEntry(person, influence, applier, applierID);
        }
    }

    private void ApplyToTroops(Influence influence, TroopList troops, Applier applier, int id)
    {
        foreach (Troop troop in troops)
        {
            ApplyToTroop(influence, troop, applier, id);
        }
    }

    private void ApplyToArchitectures(Influence influence, ArchitectureList archs, Applier applier, int id)
    {
        foreach (Architecture arch in archs)
        {
            ApplyFromEntry(arch, influence, applier, id);
        }
    }

    public void ApplyFromEntry(Architecture arch, Influence influence, Applier applier, int id)
    {
        if (AppliesToArchitecture)
        {
            ApplyToArchitecture(influence, arch, applier, id);
            return;
        }

        if (AppliesToPerson)
        {
            ApplyToPersons(influence, arch.Persons, applier, id);
        }
    }

    public void ApplyFromEntry(Faction faction, Influence influence, Applier applier, int id)
    {
        if (AppliesToFaction)
        {
            ApplyToFaction(influence, faction, applier, id);
            return;
        }

        // 建筑战斗从势力入口先走建筑展开，不继续走部队展开。
        if (AppliesToArchitecture)
        {
            ApplyToArchitectures(influence, faction.Architectures, applier, id);
            return;
        }

        if (AppliesToTroop)
        {
            ApplyToTroops(influence, faction.Troops, applier, id);
        }

        if (AppliesToPerson)
        {
            ApplyToPersons(influence, faction.Persons, applier, id);
        }
    }

    public void ApplyFromEntry(Person person, Influence influence, Applier applier, int id)
    {
        if (AppliesToPerson)
        {
            ApplyToPerson(influence, person, applier, id);
        }

        if (AppliesToTroop && person.LocationTroop != null)
        {
            ApplyToTroop(influence, person.LocationTroop, applier, id);
        }

        if (AppliesToArchitecture && person.LocationArchitecture != null)
        {
            ApplyToArchitecture(influence, person.LocationArchitecture, applier, id);
        }
    }

    public void ApplyFromEntry(Troop troop, Influence influence, Applier applier, int id)
    {
        if (AppliesToTroop)
        {
            ApplyToTroop(influence, troop, applier, id);
        }
    }

    #endregion

    #region 移除影响

    public virtual void PurifyInfluenceKind(Influence influence, Architecture architecture) {}

    public virtual void PurifyInfluenceKind(Influence influence, Faction faction) {}

    public virtual void PurifyInfluenceKind(Influence influence, Person person) {}

    public virtual void PurifyInfluenceKind(Influence influence, Troop troop) {}

    private bool TryPurifyArchitecture(Influence influence, Architecture arch, Applier applier, int id)
    {
        return influence.ApplyArchitectures.Remove(new ApplyArchitecture(arch, applier, id));
    }

    private bool TryPurifyPerson(Influence influence, Person person, Applier applier, int id)
    {
        return influence.ApplyPersons.Remove(new ApplyPerson(person, applier, id));
    }

    private bool TryPurifyTroop(Influence influence, Troop troop, Applier applier, int id)
    {
        return influence.ApplyTroops.Remove(new ApplyTroop(troop, applier, id));
    }

    private bool TryPurifyFaction(Influence influence, Faction faction, Applier applier, int id)
    {
        return influence.ApplyFactions.Remove(new ApplyFaction(faction, applier, id));
    }

    private void PurifyFromArchitecture(Influence influence, Architecture arch, Applier applier, int id)
    {
        if (TryPurifyArchitecture(influence, arch, applier, id))
        {
            PurifyInfluenceKind(influence, arch);
        }
    }

    private void PurifyFromPerson(Influence influence, Person person, Applier applier, int id )
    {
        if (TryPurifyPerson(influence, person, applier, id))
        {
            PurifyInfluenceKind(influence, person);
        }
    }

    private void PurifyFromTroop(Influence influence, Troop troop, Applier applier, int id)
    {
        if (TryPurifyTroop(influence, troop, applier, id))
        {
            PurifyInfluenceKind(influence, troop);
        }
    }
    
    private void PurifyFromFaction(Influence influence, Faction faction, Applier applier, int id)
    {
        if (TryPurifyFaction(influence, faction, applier, id))
        {
            PurifyInfluenceKind(influence, faction);
        }
    }

    private void PurifyFromArchitectures(Influence influence, ArchitectureList archs, Applier applier, int id)
    {
        foreach (Architecture arch in archs)
        {
            PurifyFromArchitecture(influence, arch, applier, id);
        }
    }

    private void PurifyFromPersons(Influence influence, PersonList persons, Applier applier, int id)
    {
        foreach (Person person in persons)
        {
            PurifyFromPerson(influence, person, applier, id);
        }
    }

    private void PurifyFromTroops(Influence influence, TroopList troops, Applier applier, int id)
    {
        foreach (Troop troop in troops)
        {
            PurifyFromTroop(influence, troop, applier, id);
        }
    }

    public void PurifyFromEntry(Architecture arch, Influence influence, Applier applier, int id)
    {
        if (AppliesToArchitecture)
        {
            PurifyFromArchitecture(influence, arch, applier, id);
            return;
        }

        if (AppliesToPerson)
        {
            PurifyFromPersons(influence, arch.Persons, applier, id);
        }
    }

    public void PurifyFromEntry(Faction faction, Influence influence, Applier applier, int id)
    {
        if (!AppliesToFaction) return;

        PurifyFromFaction(influence, faction, applier, id);
        PurifyFromArchitectures(influence, faction.Architectures, applier, id);
        PurifyFromTroops(influence, faction.Troops, applier, id);
    }

    public void PurifyFromEntry(Person person, Influence influence, Applier applier, int id)
    {
        if (AppliesToPerson)
        {
            PurifyFromPerson(influence, person, applier, id);
            return;
        }

        // 建筑战斗从个人 Purify 优先按部队移除，不继续按建筑移除。
        if (AppliesToTroop && person.LocationTroop != null)
        {
            PurifyFromTroop(influence, person.LocationTroop, applier, id);
            return;
        }

        if (AppliesToArchitecture && person.LocationArchitecture != null)
        {
            PurifyFromArchitecture(influence, person.LocationArchitecture, applier, id);
        }
    }

    public void PurifyFromEntry(Troop troop, Influence influence, Applier applier, int id)
    {
        if (AppliesToTroop)
        {
            PurifyFromTroop(influence, troop, applier, id);
        }
    }

    #endregion
}