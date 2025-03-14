using Mono.Cecil;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;

public class GameScene : MonoBehaviour
{

    GameObject _slime;
    GameObject _goblin;
    GameObject _snake;
    GameObject _joystick;

    void Start()
    {
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
    

    void StartLoaded() 
    {
        // Resources폴더에서 찾는 방식
        // var a = Resources.Load<GameObject>("Prefabs/Snake_01");

        GameObject _slimePrefab = Managers.Resource.Load<GameObject>("Slime_01.prefab");
        _slime = GameObject.Instantiate(_slimePrefab);
        GameObject _snakePrefab = Managers.Resource.Load<GameObject>("Snake_01.prefab");
        _snake = GameObject.Instantiate(_snakePrefab);
        GameObject _goblinPrefab = Managers.Resource.Load<GameObject>("Goblin_01.prefab");
        _goblin = GameObject.Instantiate(_goblinPrefab);
        GameObject _joystickPrefab = Managers.Resource.Load<GameObject>("Joystick.prefab");
        _joystick = GameObject.Instantiate(_joystickPrefab);

        GameObject monsterObjects = new GameObject() { name = "@Monsters" };

        //_slime.transform.parent = monsterObjects.transform;
        _goblin.transform.parent = monsterObjects.transform;
        _snake.transform.parent = monsterObjects.transform;

        _slime.name = _slimePrefab.name;
        _goblin.name = _goblinPrefab.name;
        _snake.name = _snakePrefab.name;
        _joystick.name = _joystickPrefab.name;

        _slime.AddComponent<PlayerController>();

        Camera.main.GetComponent<CameraController>().Target = _slime;
    }
    void Update()
    {
        
    }
}
