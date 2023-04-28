public enum GameState
{
    gameStarted,
    playingLevel,
    reachedBoss,
    fightingBoss,
    killedBoss,
    exploringLevel,
    levelWon,
    levelLost,
    gamePaused,
    enteredLoby,
}

public enum DamageType
{
    kinetic,
    burn,
    electrical,
    chemical,
    spiritual
}

public enum WeaponAttackType
{
    ranged,
    melee
}

public enum BulletDamageArea
{
    single,
    small,
    large
}

public enum BulletType
{
    common,
    piercing,
    blunt
}

public enum ItemType
{
    equipment,
    bounty,
    consumable,
    misc
}