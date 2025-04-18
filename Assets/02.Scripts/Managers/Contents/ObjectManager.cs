using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectManager 
{
    
    public PlayerController Player { get; private set; }
    public HashSet<MonsterController> Monster { get; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>();
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
            //pc.Init();
            return pc as T;
        }
        else if(type == typeof(MonsterController))
        {
            string name = "";
            switch (templateID) 
            {
                case MonsterID.GOBLIN_ID:
                    name = PrefabsName.Goblin;
                    break;
                case MonsterID.SNAKE_ID:
                    name = PrefabsName.Snake;
                    break;
                case MonsterID.BOSS_ID:
                    name = PrefabsName.Boss;
                    
                    break;
            }
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
        else if (type == typeof(ProjectileController))
        {
            GameObject go = Managers.Resource.Instantiate(SkillPrefabsName.FireProjectile, pooling: true);
            go.transform.position = position;

            ProjectileController pc = go.GetOrAddComponent<ProjectileController>();
            Projectiles.Add(pc);
            pc.Init();

            return pc as T;
        }
        else if (type == typeof(EgoSword))
        {
            GameObject go = Managers.Resource.Instantiate(SkillPrefabsName.EgoSword);
            go.transform.position = position;

            EgoSword es = go.GetOrAddComponent<EgoSword>();
            es.Init();

            return es as T;
        }
        else if (type == typeof(FireballSkill))
        {
            GameObject go = Managers.Resource.Instantiate(SkillPrefabsName.FireBallSpawn);
            go.transform.position = position;

            FireballSkill fs = go.GetOrAddComponent<FireballSkill>();
            fs.Init();

            return fs as T;
        }
        /*else if (typeof(T).IsSubclassOf(typeof(SkillBase)))
        {
            if (Managers.Data.SkillDic.TryGetValue(templateID,out Data.SkillData skillData)==false)
            {
                Debug.LogError($"ObjectManager Spawn Skill Failed{templateID}");
                return null;
            }
            GameObject go = Managers.Resource.Instantiate(skillData.prefab, pooling: true);
            go.transform.position = position;

            T t = go.GetOrAddComponent<T>();
            
            //t.Init();

            return t;
        }*/
        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController 
    {
        if (obj.IsValid()==false)
        {
            Debug.Log($"Object Pool Error Debuging{obj}");
            return;
        }
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
        else if (type == typeof(BossController))
        {
            Monster.Remove(obj as BossController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(GemController))
        {
            Gems.Remove(obj as GemController);
            Managers.Resource.Destroy(obj.gameObject);

            //Temp
            GameObject.Find("@Grid").GetComponent<GridController>().Remove(obj.gameObject);
        }
        else if (type == typeof(ProjectileController))
        {
            Projectiles.Remove(obj as ProjectileController);
            Managers.Resource.Destroy(obj.gameObject);
        }

    }

    public void DespawnAllMonsters() 
    {
        var mosters = Monster.ToList();

        foreach (var monster in mosters)
        {
            Despawn<MonsterController>(monster);
        }
    }
}
