using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    public override bool Init()
    {
        base.Init();

        _animator = GetComponent<Animator>();
        //CreatureState = CreatureState.Moving;
        Hp = 1000000;

        CreatureState = CreatureState.Skill;

        Skills.AddSkill<Move>(transform.position);
        Skills.AddSkill<Dash>(transform.position);
        Skills.AddSkill<Dash>(transform.position);
        Skills.AddSkill<Dash>(transform.position);
        Skills.StartNextSequenceSkill();

        return true;
    }

    public override void UpdateAnimation()
    {
        switch (CreatureState) 
        {
            case CreatureState.Idle:
                _animator.Play("Idle");
                break;
            case CreatureState.Moving:
                _animator.Play("Moving");
                break;
            case CreatureState.Skill:
                //_animator.Play("Attack");
                break;
            case CreatureState.Dead:
                _animator.Play("Death");
                break;
        }
    }

    // 스킬로 수정
    /*float _range = 2.0f;
    protected override void UpdateMoving()
    {
        PlayerController pc = Managers.Game.Player;
        if (pc.IsValid() == false)
            return;

        Vector3 dir = pc.transform.position - transform.position;

        if (dir.magnitude <_range)
        {
            CreatureState = CreatureState.Skill;

            //float animLength = _animator.runtimeAnimatorController.animationClips.Length;
            float animLength = 0.41f;
            Wait(animLength);
        }
    }

    protected override void UpdateSkill()
    {
        if (_coWait ==null)
            CreatureState = CreatureState.Moving;

    }*/

    protected override void UpdateDead()
    {
        if (_coWait == null)
            Managers.Object.Despawn(this);
    }

    #region Wait Coroutine

    Coroutine _coWait;

    void Wait(float waitSecond) 
    {
        if (_coWait != null)
            StopCoroutine(_coWait);
        _coWait = StartCoroutine(CoStartWait(waitSecond));

    }

    IEnumerator CoStartWait(float waitSecond) 
    {
        yield return new WaitForSeconds(waitSecond);
        _coWait = null;
    }

    #endregion

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);
    }

    protected override void OnDead()
    {
        CreatureState = CreatureState.Dead;
        Wait(2.0f);
    }

}
