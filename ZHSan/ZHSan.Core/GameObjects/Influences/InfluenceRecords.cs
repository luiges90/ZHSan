
namespace GameObjects.Influences;

public record ApplyArchitecture(Architecture arch, Applier applier, int id);

public record ApplyPerson(Person person, Applier applier, int id);

public record ApplyFaction(Faction faction, Applier applier, int id);

public record ApplyTroop(Troop troop, Applier applier, int id);