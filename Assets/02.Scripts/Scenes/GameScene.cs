using Mono.Cecil;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;

public class GameScene : MonoBehaviour
{

    void Start()
    {
        //Managers.Resource.LoadAllAsync<GameObject>("Prefabs", OnLoadProgress);
        Managers.Resource.LoadAllAsync<GameObject>("Prefabs", (key,count,totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count==totalCount)
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

        var player = Managers.Object.Spawn<PlayerController>();

        for (int i = 0; i < 10; i++)
        {
            MonsterController mc = Managers.Object.Spawn<MonsterController>(Random.Range(0,2));
            mc.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        }

        var joystick = Managers.Resource.Instantiate(PrefabsName.UI_Joystick);
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate(PrefabsName.Map);
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().Target = player.gameObject;
    }

    void Update()
    {
        
    }
}
