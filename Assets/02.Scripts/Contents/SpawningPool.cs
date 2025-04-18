using System.Collections;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    // 리스폰 주기는?
    // 몬스터 최대 개수는?
    // 스톱?
    float _spawnInterval = 0.5f;
    int _maxMonsterCount = 100;
    Coroutine _coUpdateSpawningPool;

    public bool Stopped { get; set; } = false;  

    void Start()
    {
        _coUpdateSpawningPool = StartCoroutine(CoUpdateSpawningPool());
    }

    IEnumerator CoUpdateSpawningPool() 
    {
        while (true) 
        {
            TrySpawn();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void TrySpawn()
    {
        if (Stopped)
            return;
            
        int monsterCount = Managers.Object.Monster.Count;
        if (monsterCount > _maxMonsterCount)
            return;

        Vector3 randPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position,10,15);
        MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, 1 + Random.Range(0, 2));
        
    }

}
