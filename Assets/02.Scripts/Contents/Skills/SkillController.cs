using System.Collections;
using UnityEngine;

// EgoSword : 평타
// FireProjectile : 투사체
// PoisonField : 바닥
public class SkillController : BaseController
{
    public SkillType SkillType {get; set;}
    public Data.SkillData SkillData { get; protected set; }



    #region Destroy
    Coroutine _coDestroy;

    public void StartDestroy(float delaySeconds) 
    {
        StopDestroy();
        _coDestroy = StartCoroutine(CoDestroy(delaySeconds));
    }

    public void StopDestroy() 
    {
        if (_coDestroy!=null)
        {
            StopCoroutine(_coDestroy);
            _coDestroy = null;
        }
    }

    IEnumerator CoDestroy(float delaySecond) 
    {
        yield return new WaitForSeconds(delaySecond);
        if (this.IsValid())
        {
            Managers.Object.Despawn(this);
        }
    }
    #endregion


}
