
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

public enum ObjectType
{
    Player,
    Monster,
    Projectile,
    Env,
}


public struct PrefabsName
{
    public const string Goblin = "Goblin_01.prefab";
    public const string Snake = "Snake_01.prefab";
    public const string Player = "Slime_01.prefab";
    public const string UI_Joystick = "UI_Joystick.prefab";
    public const string Map = "Map.prefab";
    public const string Gem = "EXPGem.prefab";
}

public struct SceneName
{
    public const string TitleScene = "TitleScene";
    public const string LoadingScene = "LoadingScene";
    public const string VillageScene = "VillageScene";

}

public enum Region
{
    초원,
    사막,
    설산,
    돌산
}

