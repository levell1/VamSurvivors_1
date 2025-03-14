
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

public struct UIName
{
    public const string ControlKeyUI = "ControlKeyUI";
    public const string GoDungeonUI = "GoDungeonUI";

}

public struct SceneName
{
    public const string TitleScene = "TitleScene";
    public const string LoadingScene = "LoadingScene";
    public const string VillageScene = "VillageScene";

}

public struct TagName
{
    public const string Respawn = "Respawn";
    public const string Player = "Player";
    public const string CookedFood = "CookedFood";
    public const string Enemy = "Enemy";
    public const string Item = "Item";
    public const string Door = "Door";
    public const string AI = "AI";
    public const string PotionShop = "PotionShop";
    public const string Enhancement = "Enhancement";
    public const string RealDoor = "RealDoor";
    public const string QuestNPC = "QuestNPC";
    public const string DungeonDoor = "DungeonDoor";
    public const string DungeonNPC = "DungeonNPC";
    public const string Wall = "Wall";
}

public struct LayerName
{
    public const string UI = "UI";
    public const string Ground = "Ground";
    public const string Item = "Item";
    public const string NpcInteract = "NpcInteract";
    public const string Player = "Player";
    public const string Enemy = "Enemy";
    public const string Title = "Title";
}


public struct PoolingObjectName
{
    public const string Customer = "Customer";
}

public struct JsonDataName
{
    public const string PlayerData = "JSON/PlayerData";
    public const string PlayerSkillData = "JSON/PlayerSkillData";
    public const string PlayerLevelData = "JSON/LevelData";
    public const string SaveFile = "SaveFile";
}

public struct AnimationParameterName
{
    public const string TycoonGetFood = "GetFood";
    public const string TycoonAngry = "Angry";
    public const string TycoonIsWalk = "IsWalk";
    public const string TycoonIsEat = "IsEat";
    public const string TycoonIsIdle = "IsIdle";
    public const string BossIdle = "Idle";
    public const string BossWalk = "Walk";
    public const string BossAttack = "Attack";
    public const string BossSpin = "Spin";
    public const string BossFear = "Fear";
    public const string BossRun = "Run";
    public const string BossIdleC = "IdleC";
    public const string BossRoll = "Roll";
    public const string BossHit = "Hit";
    public const string BossFly = "Fly";
    public const string BossSit = "Sit";
    public const string BossDead = "Die";
}

public struct CoolTimeObjName
{
    public const string Dash = "Dash";
    public const string ThrowSkill = "ThrowSkill";
    public const string SpreadSkill = "SpreadSkill";
    public const string HealthPotion = "HealthPotion";
    public const string StaminaPotion = "StaminaPotion";
}

public struct BGMSoundName
{
    public const string VillageBGM1 = "VillageBGM1";
    public const string VillageBGM2 = "VillageBGM2";

    public const string TycoonBGM1 = "TycoonBGM1";
    public const string TycoonBGM2 = "TycoonBGM2";

}

public struct ErrorMessageTxt
{
    public const string DontSceneMoveErrorMessage = "입장가능시간이 아닙니다!";
    public const string TenMinutesLeft = "10분 남았습니다.";
    public const string TwentyMinutesLeft = "20분 남았습니다.";
}
public struct SFXSoundPathName
{
    public const string ClickSound = "Click";
    public const string UISound = "UISound";

    public const string Eat = "Eat";
    public const string Money = "Money";
    public const string PutDownFood = "PutDownFood";
}

public struct ColorsSetting
{
    public const string ClickSound = "Click";
    public const string UISound = "UISound";

    public const string Eat = "Eat";
    public const string Money = "Money";
    public const string PutDownFood = "PutDownFood";
}
public enum Region
{
    초원,
    사막,
    설산,
    돌산
}

