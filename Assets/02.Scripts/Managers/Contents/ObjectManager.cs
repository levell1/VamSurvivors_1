using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;

public class ObjectManager 
{
    
    public PlayerController Player { get; private set; }
    public HashSet<MonsterController> Monster { get; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectile { get; } = new HashSet<ProjectileController>();
    public HashSet<GemController> Gems { get; } = new HashSet<GemController>();

    public T Spawn<T>(Vector3 position, int  templateID =0) where T : BaseController 
    {
        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            GameObject go = Managers.Resource.Instantiate(PrefabsName.Player, pooling: true);
            go.name = "Player";
            go.transform.position = position;

            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            Player = pc;
            pc.Init();
            return pc as T;
        }
        else if(type == typeof(MonsterController))
        {
            string name = (templateID == 0 ? PrefabsName.Goblin : PrefabsName.Snake);
            GameObject go = Managers.Resource.Instantiate(name, pooling : true);
            go.transform.position = position;

            MonsterController mc = go.GetOrAddComponent<MonsterController>();
            Monster.Add(mc);
            mc.Init();
            return mc as T;
        }
        else if (type == typeof(GemController))
        {
           
            GameObject go = Managers.Resource.Instantiate(PrefabsName.Gem, pooling: true);
            go.transform.position = position;

            GemController gc = go.GetOrAddComponent<GemController>();
            Gems.Add(gc);
            gc.Init();

            string key = Random.Range(0, 2) ==0 ? "EXPGem_01.sprite": "EXPGem_02.sprite";
            Sprite sprite = Managers.Resource.Load<Sprite>(key);
            go.GetComponent<SpriteRenderer>().sprite = sprite;

            //Temp
            GameObject.Find("@Grid").GetComponent<GridController>().Add(go);

            return gc as T;
        }
        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController 
    {
        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            //?
        }
        else if (type == typeof(MonsterController))
        {
            Monster.Remove(obj as MonsterController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(ProjectileController))
        {
            Projectile.Remove(obj as ProjectileController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(GemController))
        {
            Gems.Remove(obj as GemController);
            Managers.Resource.Destroy(obj.gameObject);

            //Temp
            GameObject.Find("@Grid").GetComponent<GridController>().Remove(obj.gameObject);
        }

    }
}
