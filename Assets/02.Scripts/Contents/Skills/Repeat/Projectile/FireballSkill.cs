using UnityEngine;

public class FireballSkill : RepeatSkill
{
    
    public FireballSkill()
    {
        
    }
    protected override void DoSkillJob()
    {
        PlayerController pc = Managers.Game.Player;
        if (pc == null)
            return;

        Vector3 spawnPos = pc.FireSocket;
        Vector3 dir = pc.ShootDir;

        GenerateProjectile(SkillID.FIRE_BALL_ID, pc, spawnPos, dir, Vector3.zero);
       
    }

}
