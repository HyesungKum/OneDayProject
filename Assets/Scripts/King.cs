using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Player
{
   
    public override void SpecialSkill()
    {
        StartCoroutine(SpecialSkill_timer(specialSkillCooltime));
    }
    public override IEnumerator SpecialSkill_timer(float time)
    {
        GameObject hitbox;
        isSpecial = true;
        animator.SetTrigger("isSpell2");
        yield return new WaitForSeconds(0.2f);
        if (left == true)
        {
            hitbox = Instantiate(specialSkill, (Vector2)this.transform.position + new Vector2(-1f, 0), Quaternion.identity);
            hitbox.GetComponentInChildren<ParticleSystemRenderer>().flip = new Vector2(1, 0);
            hitbox.SendMessage("Name", selectPlayer, SendMessageOptions.DontRequireReceiver);
            hitbox.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            Destroy(hitbox, 1f);
        }
        else
        {
            hitbox = Instantiate(specialSkill, (Vector2)this.transform.position + new Vector2(1f, 0), Quaternion.identity);
            hitbox.GetComponentInChildren<ParticleSystemRenderer>().flip = new Vector2(0, 0);
            hitbox.SendMessage("Name", selectPlayer, SendMessageOptions.DontRequireReceiver);
            hitbox.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            Destroy(hitbox, 1f);
        }
        yield return new WaitForSeconds(time - 0.4f);
        isSpecial = false;
        yield break;
    }

}
