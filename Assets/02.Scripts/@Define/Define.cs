
public enum Scene
{
    Unknown,
    DevScene,
    GameScene,
}

public enum Sound 
{
    Bgm,
    Effect,
}

public enum UIEvent
{
    Click,
    Pressed,
    PointerDown,
    PointerUp,
    Drag,
    BeginDrag,
    EndDrag,
}


public enum ObjectType
{
    Player,
    Monster,
    Projectile,
    Env,
}

public enum SkillType
{
    None,
    Sequence,
    Repeat,
}
public struct SkillID
{
    public const int EGO_SWORD_ID = 10;
    public const int FIRE_BALL_ID = 1;

}

public struct MonsterID
{
    public const int GOBLIN_ID = 1;
    public const int SNAKE_ID = 2;
    public const int BOSS_ID = 3;
}

public enum StageType
{
    Normal,
    Boss,
}

public enum CreatureState
{
    Idle,
    Moving,
    Skill,
    Dead,
}


public struct PrefabsName
{
    public const string Goblin = "Goblin_01.prefab";
    public const string Snake = "Snake_01.prefab";
    public const string Player = "Slime_01.prefab";
    public const string Boss = "Boss_01.prefab";

    public const string Map = "Map.prefab";
    public const string Gem = "EXPGem.prefab";

    public const string UI_Joystick = "UI_Joystick.prefab";
    public const string UI_SkillSelectPopup = "UI_SkillSelectPopup.prefab";
    public const string UI_SkillCardItem = "UI_SkillCardItem.prefab";
}

public struct SkillPrefabsName
{
    public const string FireProjectile = "FireProjectile.prefab";
    public const string EgoSword = "EgoSword.prefab";
    public const string FireBallSpawn = "FireBallSpawn.prefab";
}


