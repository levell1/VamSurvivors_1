using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 _moverDir = Vector2.zero;
    new float _speed = 5.0f;

    float EnvCollectDist { get; set; } = 1.0f;

    public Vector2 MoveDir 
    {
        get { return _moverDir; }
        set { _moverDir = value.normalized; }
    }
    void Start()
    {
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
    }

    void OnDestroy()
    {
        if (Managers.Game != null)
            Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
    }
    void HandleOnMoveDirChanged(Vector2 dir) 
    {
        _moverDir = dir;
    }

    void Update()
    {
        //UpdateInput();
        MovePlayer();
        CollectEnv();
    }


    void MovePlayer() {
        // Temp2 Joystick-Player (Manager(Static))
        //_moverDir = Managers.Game.MoveDir;

        Vector3 dir = _moverDir * _speed * Time.deltaTime;
        transform.position += dir;
    }

    void CollectEnv() 
    {
        float sqrCollectDis = EnvCollectDist * EnvCollectDist;
        List<GemController> gems = Managers.Object.Gems.ToList();
        foreach (GemController gem in gems)
        {
            Vector3 dir = gem.transform.position - transform.position;
            if (dir.sqrMagnitude <= EnvCollectDist)
            {
                Managers.Game.Gem += 1;
                Managers.Object.Despawn(gem);
            }   
        }

        var findGems = GameObject.Find("@Grid").GetComponent<GridController>().GatherObjects(transform.position, EnvCollectDist + 0.4f);

        Debug.Log($"GridSearch = {findGems.Count} Total = {gems.Count}");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController target = collision.gameObject.GetComponent<MonsterController>();
        if (target == null)
            return;
    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);
        Debug.Log($"Ondamage ! {attacker} {Hp}");

        // Temp ¹Ý°Ý
        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 10000);
    }
}
