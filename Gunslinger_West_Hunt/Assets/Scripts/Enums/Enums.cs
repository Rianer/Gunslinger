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
    spiritual,
    elemental
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

public enum MovingDirection
{
    E,
    S,
    N,
    W,
    NE,
    NW,
    SE,
    SW
}

public enum AILogicState
{
    followingPath,
    followingPlayer,
    idle,
    sleeping,
    wandering
}