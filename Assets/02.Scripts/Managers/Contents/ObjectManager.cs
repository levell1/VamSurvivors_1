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

    public T Spawn<T>(int  templateID =0) where T : BaseController 
    {
        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            GameObject go = Managers.Resource.Instantiate(PrefabsName.Player, pooling: true);
            go.name = "Player";

            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            Player = pc;
            return pc as T;
        }
        else if(type == typeof(MonsterController))
        {
            string name = (templateID == 0 ? PrefabsName.Goblin : PrefabsName.Snake);
            GameObject go = Managers.Resource.Instantiate(name, pooling : true);

            MonsterController mc = go.GetOrAddComponent<MonsterController>();
            Monster.Add(mc);
            return mc as T;
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

    }
}
