using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 _moverDir = Vector2.zero;
    float _speed = 5.0f;

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
    }


    void MovePlayer() {
        // Temp2 Joystick-Player (Manager(Static))
        //_moverDir = Managers.Game.MoveDir;

        Vector3 dir = _moverDir * _speed * Time.deltaTime;
        transform.position += dir;
    }
}
