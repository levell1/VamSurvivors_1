using Mono.Cecil;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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

    /*람다 테스트 
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
        _spawningPool = gameObject.AddComponent<SpawningPool>();

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

        //Data Test
        Managers.Data.Init();

        foreach (var playerData in Managers.Data.PlayerDic.Values)
        {
            Debug.Log($"Lv1 : {playerData.level}, HP : {playerData.maxHp}");
        }
    }

    void Update()
    {
        
    }
}
