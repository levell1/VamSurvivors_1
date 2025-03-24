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

    void Start()
    {
        
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        _speed = 5.0f;

        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

        StartProjectile();

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

        //Debug.Log($"GridSearch = {findGems.Count} Total = {gems.Count}");
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
        //Debug.Log($"Ondamage ! {attacker} {Hp}");

        // Temp ¹Ý°Ý
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
}
