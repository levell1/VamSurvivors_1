using System;
using UnityEngine;


public class GameManager 
{
    public PlayerController Player { get { return Managers.Object?.Player; } }
    Vector2 _moveDir;

    public event Action<Vector2> OnMoveDirChanged;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set 
        {   
            _moveDir = value;
            OnMoveDirChanged?.Invoke(_moveDir);
        }
    }
}
