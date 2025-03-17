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

    void Awake()
    {
       
    }


    void Update()
    {
        
    }

    public virtual void OnDamaged(BaseController attacker, int damage) 
    {
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
