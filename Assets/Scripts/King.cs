using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Player
{
    public override void UniqueSkill()
    {
        StartCoroutine(UniqueSkill_timer(uniqueSkillCooltime));
    }
}
