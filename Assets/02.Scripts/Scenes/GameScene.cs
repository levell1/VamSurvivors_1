using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameObject _slimePrefab;
    public GameObject _goblinPrefab;
    public GameObject _snakePrefab;
    public GameObject _joystickPrefab;

    GameObject _slime;
    GameObject _goblin;
    GameObject _snake;
    GameObject _joystick;

    void Start()
    {
        GameObject monsterObjects = new GameObject() { name = "@Monsters" };

        _slime = GameObject.Instantiate(_slimePrefab);
        _goblin = GameObject.Instantiate(_goblinPrefab);
        _snake = GameObject.Instantiate(_snakePrefab);
        _joystick = GameObject.Instantiate(_joystickPrefab);

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
