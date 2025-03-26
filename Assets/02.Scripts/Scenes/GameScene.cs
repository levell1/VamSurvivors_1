using Mono.Cecil;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;

public class GameScene : MonoBehaviour
{

    void Start()
    {
        //Managers.Resource.LoadAllAsync<GameObject>("Prefabs", OnLoadProgress);
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key,count,totalCount) =>
        {
            //Debug.Log($"{key} {count}/{totalCount}");
            Debug.Log($"{key} {count}/{totalCount}");
            if (count == totalCount)
            {
                StartLoaded();
            }
        });
    }

    /*���� �׽�Ʈ 
    Managers.Resource.LoadAllAsync<GameObject>("Prefabs", OnLoadProgress);
    private void LambdaTest(string key, int count, int totalCount)
    {
        Debug.Log($"{key} {count}/{totalCount}");

        if (count == totalCount)
        {
            StartLoaded();
        }
    }*/


    /*void StartLoaded() 
    {
        var player = Managers.Resource.Instantiate("Slime_01.prefab");
        player.AddComponent<PlayerController>();

        GameObject monsterObjects = new GameObject() { name = "@Monsters" };
        var snake = Managers.Resource.Instantiate("Snake_01.prefab", monsterObjects.transform);
        var goblin = Managers.Resource.Instantiate("Goblin_01.prefab", monsterObjects.transform);
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("Map.prefab");

        map.name = "@Map";
        Camera.main.GetComponent<CameraController>().Target = player;
    }*/

    SpawningPool _spawningPool;
    void StartLoaded()
    {
        Managers.Data.Init();

        Managers.UI.ShowSceneUI<UI_GameScene>();

        _spawningPool = gameObject.AddComponent<SpawningPool>();

        foreach (var skilldata in Managers.Data.SkillDic.Values)
        {
            Debug.Log($" ��ų id : {skilldata.templateID}");
        }

        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

        for (int i = 0; i < 10; i++)
        {
            Vector3 randPos = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
            MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos,Random.Range(0,2));
        }

        var joystick = Managers.Resource.Instantiate(PrefabsName.UI_Joystick);
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate(PrefabsName.Map);
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().Target = player.gameObject;

        Managers.Game.OnkillCountChanged -= HandleOnkillCountChanged;
        Managers.Game.OnkillCountChanged += HandleOnkillCountChanged;
        Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
        Managers.Game.OnGemCountChanged += HandleOnGemCountChanged;
    }

    int _collectedGemCount = 0;
    int _remainingTotalGemCount = 10;
    public void HandleOnGemCountChanged(int gemCount) 
    {
        _collectedGemCount++;

        if (_collectedGemCount == _remainingTotalGemCount)
        {
            Managers.UI.ShowPopup<UI_SkillSelectPopup>();
            _collectedGemCount = 0;
            _remainingTotalGemCount *= 2; 
        }

        Managers.UI.GetSceneUI<UI_GameScene>().SetGemCountRatio((float)_collectedGemCount / _remainingTotalGemCount);
    }

    public void HandleOnkillCountChanged(int killCount)
    {
        Managers.UI.GetSceneUI<UI_GameScene>().SetKillCount(killCount);

        if (killCount==5)
        {
            //����? ������
        }
    }
    private void OnDestroy()
    {
        if (Managers.Game != null)
        {
            Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
            Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
        }
            
    }
}
