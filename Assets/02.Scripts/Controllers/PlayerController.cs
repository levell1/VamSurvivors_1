using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 _moverDir = Vector2.zero;

    float EnvCollectDist { get; set; } = 1.0f;

    [SerializeField]
    Transform _indicator;
    [SerializeField]
    Transform _fireSocket;

    public Vector2 MoveDir 
    {
        get { return _moverDir; }
        set { _moverDir = value.normalized; }
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        _speed = 5.0f;

        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

        StartProjectile();
        StartEgoSword();

        return true;
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

        if (_moverDir != Vector2.zero)
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-dir.x, dir.y) * 180 / Mathf.PI);
        }
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    void CollectEnv() 
    {
        float sqrCollectDis = EnvCollectDist * EnvCollectDist;

        var findGems = GameObject.Find("@Grid").GetComponent<GridController>().GatherObjects(transform.position, EnvCollectDist + 0.4f);

        foreach (var go in findGems)
        {
            GemController gem = go.GetComponent<GemController>();

            Vector3 dir = gem.transform.position - transform.position;
            if (dir.sqrMagnitude <= EnvCollectDist)
            {
                Managers.Game.Gem += 1;
                Managers.Object.Despawn(gem);
            }   
        }

        
        //Debug.Log($"GridSearch = {findGems.Count} ");
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

        // Temp �ݰ�
        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 10000);
    }

    //Temp
    #region FireProjectile

    Coroutine _coFireProjectile;

    void StartProjectile() 
    {
        if (_coFireProjectile != null)
            StopCoroutine(_coFireProjectile);
        _coFireProjectile = StartCoroutine(CoFireProjectile());
    }

    IEnumerator CoFireProjectile() 
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        while (true)
        {
            ProjectileController pc = Managers.Object.Spawn<ProjectileController>(_fireSocket.position,1);
            pc.SetInfo(1, this, (_fireSocket.position-_indicator.position).normalized);
            yield return wait;
        }
        
    }

    #endregion

    #region EgoSword

    EgoSwordController _egoSword;
    void StartEgoSword() 
    {
        if (_egoSword.IsValid())
            return;

        _egoSword = Managers.Object.Spawn<EgoSwordController>(_indicator.position, SkillID.EGO_SWORD_ID);
        _egoSword.transform.SetParent(_indicator);

        _egoSword.ActivateSkill();
    }

    #endregion
}
