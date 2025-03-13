using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static bool s_init = false;

    #region Contents
    GameManager _game = new GameManager();
    ObjectManager _object = new ObjectManager();
    PoolManager _pool = new PoolManager();

    public static GameManager Game { get { return instance?._game; } }
    public static ObjectManager Object { get { return instance?._object; } }
    public static PoolManager Pool { get { return instance?._pool; } }
    #endregion

    #region Core
    DataManager _data = new DataManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return instance?._data; } }
    public static ResourceManager Resource { get { return instance?._resource; } }
    public static SceneManagerEx Scene { get { return instance?._scene; } }
    public static SoundManager Sound { get { return instance?._sound; } }
    public static UIManager UI { get { return instance?._ui; } }
    #endregion

    public static Managers instance 
    {
        get 
        {
            if (s_init ==false)
            {
                s_init = true;

                GameObject go = GameObject.Find("@Managers");
                if (go == null) 
                {
                    go = new GameObject() { name = "@Managers" };
                    go.AddComponent<Managers>();
                }
                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Managers>();
                // TODO 초기화 코드
                // ex) _instance._game.Inint();

            }
            return s_instance;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
