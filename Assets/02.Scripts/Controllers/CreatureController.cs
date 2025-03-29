using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    public ObjectType ObjType { get; protected set; }
    
    protected float _speed = 2.0f;
    public int Hp { get; set; } = 100;
    public int MaxHP { get; set; } = 100;

    public SkillBook Skills { get; protected set; }

    public override bool Init()
    {
        base.Init();

        Skills = gameObject.GetOrAddComponent<SkillBook>();

        return true;

    }


    public virtual void OnDamaged(BaseController attacker, int damage) 
    {
        if (Hp <= 0)
            return;

        Hp -= damage;
        if (Hp <=0)
        {
            Hp = 0;
            OnDead();
        }
    }

    protected virtual void OnDead()
    {
        
    }
}
