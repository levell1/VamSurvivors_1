using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 _moverDir = Vector2.zero;
    float _speed = 5.0f;


    void Start()
    {
        
    }


    void Update()
    {
        UpdateInput();
        MovePlayer();
    }

    void UpdateInput() 
    {
        Vector2 moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            moveDir.y += 1;
        if (Input.GetKey(KeyCode.A))
            moveDir.x -= 1;
        if (Input.GetKey(KeyCode.S))
            moveDir.y -= 1;
        if (Input.GetKey(KeyCode.D))
            moveDir.x += 1;

        _moverDir = moveDir.normalized; // normalized 방향정보

    }

    void MovePlayer() {
        Vector3 dir = _moverDir * _speed * Time.deltaTime;
        transform.position += dir;
    }
}
