using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : SkillBase
{
    CreatureController _owner;
    Vector3 _moveDir;
    float _speed = 10.0f;
    float _lifeTime = 5.0f;

    public ProjectileController() : base(SkillType.None)
    {
        
    }
    private void OnEnable()
    {
        Init();
    }

    public override bool Init()
    {
        base.Init();

        _coDestroy1 = StartCoroutine(CoDestroy1(_lifeTime));
        //StartDestroy(_lifeTime);

        return true;
    }
    

    public void SetInfo(int templateID, CreatureController owner, Vector3 moveDir) 
    {
        if (Managers.Data.SkillDic.TryGetValue(templateID, out Data.SkillData data) == false) 
        {
            Debug.Log($" ID : {templateID} ProjecteController SetInfo Failed ");
            return;
        }

        _owner = owner;
        _moveDir = moveDir;
        SkillData = data;
        //TODO : DATA Parsing

    }

    public override void UpdateController()
    {
        base.UpdateController();

        transform.position += _moveDir * _speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if (mc.IsValid() == false)
            return;
        if (this.IsValid() == false)
            return;

        mc.OnDamaged(_owner, SkillData.damage);

        StopDestroy1();
        Managers.Object.Despawn(this);
    }

    #region Destroy
    Coroutine _coDestroy1;

    public void StopDestroy1()
    {
        if (_coDestroy1 != null)
        {
            StopCoroutine(_coDestroy1);
            _coDestroy1 = null;
        }
    }

    IEnumerator CoDestroy1(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        if (this.IsValid())
        {
            Managers.Object.Despawn(this);
        }
    }
    #endregion

}
