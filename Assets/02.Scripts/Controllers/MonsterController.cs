using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MonsterController : CreatureController
{

    #region State Pattern

    CreatureState _creatureState = CreatureState.Moving;
    public virtual CreatureState CreatureState 
    {
        get { return _creatureState; }
        set 
        {
            _creatureState = value;
            UpdateAnimation();
        }
    }

    protected Animator _animator;
    public virtual void UpdateAnimation() 
    {

    }

    public override void UpdateController()
    {
        base.UpdateController();

        switch (CreatureState) 
        {
            case CreatureState.Idle:
                UpdateIdle();
                break;
            case CreatureState.Skill:
                UpdateSkill();
                break;
            case CreatureState.Moving:
                UpdateMoving();
                break;
            case CreatureState.Dead:
                UpdateDead();
                break;
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateDead() { }


    #endregion

    public override bool Init()
    {
        if (base.Init())
            return false;

        ObjType = ObjectType.Monster;
        CreatureState = CreatureState.Moving;

        return true;
    }

    void FixedUpdate()
    {
        if (CreatureState != CreatureState.Moving)
            return;
            
        PlayerController pc = Managers.Object.Player;
        if (pc == null)
            return;

        Vector3 dir = pc.transform.position - transform.position;
        Vector3 newPos = transform.position + dir.normalized * Time.deltaTime * _speed; //fixedDeltaTime
        //transform.position = newPos;                            //포지션으로 이동
        GetComponent<Rigidbody2D>().MovePosition(newPos);     //물리적인 정보를 체크하면서 이동
        GetComponent<SpriteRenderer>().flipX = dir.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target.IsValid() == false)
            return;
        if (this.IsValid() == false)
            return;

        if (_coDotDamage != null) // 기존에 뭔가 있는걸 대비
            StopCoroutine(_coDotDamage);

        _coDotDamage = StartCoroutine(CoStartDotDamage(target));
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target.IsValid() == false)
            return;
        if (this.IsValid() == false)
            return;

        if (_coDotDamage != null)
            StopCoroutine(_coDotDamage);
        _coDotDamage = null;
    }

    Coroutine _coDotDamage;
    public IEnumerator CoStartDotDamage(PlayerController target) 
    {
        while (true) 
        {
            target.OnDamaged(this,2);
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();

        Managers.Game.KillCount++;

        if (_coDotDamage != null)
            StopCoroutine(_coDotDamage);
        _coDotDamage = null;

        //죽을 때 보석 스폰
        GemController gc = Managers.Object.Spawn<GemController>(transform.position);

        Managers.Object.Despawn(this);
    }
}
