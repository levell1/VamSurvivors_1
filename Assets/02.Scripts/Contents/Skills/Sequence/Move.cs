using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : SequenceSkill
{
    Rigidbody2D _rb;
    Coroutine _coroutine;

    public override void DoSkill(Action callback = null)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CoMove(callback));

    }

    float Speed { get; } = 2.0f;
    string AnimationName { get; } = "Moving";

    IEnumerator CoMove(Action callback = null)
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().Play(AnimationName);
        float elapsed = 0; // Move ½Ã°£

        while (true)
        {
            elapsed += Time.deltaTime;
            if (elapsed > 3.0f)
                break;

            Vector3 dir = ((Vector2)Managers.Game.Player.transform.position - _rb.position).normalized;
            Vector2 targetPosition = Managers.Game.Player.transform.position + dir * UnityEngine.Random.Range(1, 4);

            if (Vector3.Distance(_rb.position, targetPosition) <= 0.2f)
                continue;
                
            
            Vector2 dirvec = (targetPosition - _rb.position).normalized;
            Vector2 nextVec = dirvec * Speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + nextVec);

            yield return null;
        }

        callback?.Invoke();
    }
}
