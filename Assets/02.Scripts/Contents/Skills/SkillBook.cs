using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    // 스킬 매니저.

    public List<SkillBase> Skills { get; } = new List<SkillBase>();

    public List<SkillBase> RepeatedSkills { get; } = new List<SkillBase>();
    public List<SequenceSkill> SequenceSkills { get; } = new List<SequenceSkill>();

    public T AddSkill<T>(Vector3 position, Transform parent = null) where T : SkillBase 
    {
        System.Type type = typeof(T);

        if (type == typeof(EgoSword))
        {
            var egoSword = Managers.Object.Spawn<EgoSword>(position, SkillID.EGO_SWORD_ID);
            egoSword.transform.SetParent(parent);
            egoSword.ActivateSkill();

            Skills.Add(egoSword);
            RepeatedSkills.Add(egoSword);
            return egoSword as T;
        }
        else if (type == typeof(FireballSkill))
        {
            var fireBall = Managers.Object.Spawn<FireballSkill>(position,SkillID.EGO_SWORD_ID);
            fireBall.transform.SetParent(parent);
            fireBall.ActivateSkill();

            Skills.Add(fireBall);
            RepeatedSkills.Add(fireBall);
            return fireBall as T;
        }
        else if (type.IsSubclassOf(typeof(SequenceSkill)))
        {
            var skill = gameObject.GetOrAddComponent<T>();
            Skills.Add(skill);
            SequenceSkills.Add(skill as SequenceSkill);

            return skill as T;
        }

        return null;
    }

    int _sequenceIndex = 0;
    public void StartNextSequenceSkill() 
    {
        if (_stopped)
            return;
        if (SequenceSkills.Count == 0)
            return;

        SequenceSkills[_sequenceIndex].DoSkill(OnFinishedSequenceSkill);
    }

    void OnFinishedSequenceSkill() 
    {
        _sequenceIndex = (_sequenceIndex + 1) % SequenceSkills.Count;
        StartNextSequenceSkill();
    }


    bool _stopped = false;

    public void StopSkills() 
    {
        _stopped = true;

        foreach (var skill in RepeatedSkills)
        {
            skill.StopAllCoroutines();
        }
    }



}
