using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state
{
    None,
    Stun,
    Slow,
    invincibility,
}
public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    [Header("PlayerPick")]
    public int selectPlayer;
    [Header("PlayerInfo")]
    [SerializeField] public float maxHp;
    [SerializeField] public float curHp;
    [SerializeField] public float basicAttackDamage;
    [SerializeField] public float basicAttackSpeed;
    [SerializeField] public float speed;
    [SerializeField] public float uniqueSkillCooltime;
    [SerializeField] public float specialSkillCooltime;
    [Header("PlayerState")]
    [SerializeField] public state playerStun;
    [SerializeField] public state playerSlow;
    [SerializeField] public state playerInvincibility;
    [Header("HitBoxPrefab")]
    [SerializeField] public GameObject basicAttack;
    [SerializeField] public GameObject uniqueSkill;
    [SerializeField] public GameObject specialSkill;
    public float xinput;
    public float zinput;
    public float xspeed;
    public bool left;
    public bool isAttack;
    public bool isUnique;
    public bool isSpecial;
    public bool isDeath;
    public float basicSpeed;
    public bool isDonMove;

    void Awake()
    {
        left = true;
        basicSpeed = speed;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (selectPlayer == 1) gameObject.tag = $"{selectPlayer}Player";
        else gameObject.tag = $"{selectPlayer}Player";
    }
    void FixedUpdate()
    {
        if (GameManager.Instance.GameOver) return;
        if (isDeath == true) return;
        if (playerStun == state.Stun) return;
        if (isDonMove == true) return;
        if (selectPlayer == 1)//1p
        {
            if (Input.GetKey(KeyCode.LeftArrow)) xinput = -1;
            else if (Input.GetKey(KeyCode.RightArrow)) xinput = 1;
            else xinput = 0;
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                if (isAttack == false) BasicAttack();
            }
            if (Input.GetKeyDown(KeyCode.Period))
            {
                if (isUnique == false) UniqueSkill();
            }
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                if (isSpecial == false) SpecialSkill();
            }
            if (Input.GetKey(KeyCode.UpArrow)) zinput = 1;
            else zinput = 0;
        }
        else//2p
        {
            if (Input.GetKey(KeyCode.D)) xinput = -1;
            else if (Input.GetKey(KeyCode.G)) xinput = 1;
            else xinput = 0;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (isAttack == false) BasicAttack();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (isUnique == false) UniqueSkill();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (isSpecial == false) SpecialSkill();
            }

            if (Input.GetKey(KeyCode.R)) zinput = 1;
            else zinput = 0;
        }
        xspeed = speed * xinput * Time.deltaTime;
        if (zinput > 0 && animator.GetBool("isGround") == true)
        {
            rigidbody.AddForce(new Vector2(xspeed, speed / 3.5f), ForceMode2D.Impulse);
            animator.SetBool("isGround", false);
        }
        rigidbody.velocity = new Vector2(xspeed, rigidbody.velocity.y);
        if (xinput != 0)
        {
            animator.SetBool("isMove", true);
            if (xinput < 0)
            {
                left = true;
                spriteRenderer.flipX = true;
            }
            if (xinput > 0)
            {
                left = false;
                spriteRenderer.flipX = false;
            }
        }
        else if (animator.GetBool("isMove") == true) animator.SetBool("isMove", false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator.GetBool("isGround") == false)
        {
            if (collision.gameObject.tag == "Ground")
            {
                if (collision.contacts[0].normal.y >= 0)
                {
                    animator.SetBool("isGround", true);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "DeathZone")
        {
            GetDamage(1000);
        }

    }

    public void GetDamage(float damage)
    {
        if (isDeath == true) return;
        if (playerInvincibility == state.invincibility) return;
        curHp += -damage;
        if (curHp <= 0)
        {
            curHp = 0;
            animator.SetTrigger("isDeath");
            isDeath = true;
        }
        if (selectPlayer == 1) UIManager.Instance.P1Hit(damage);
        else if (selectPlayer == 2) UIManager.Instance.P2Hit(damage);

    }
    public void StateChage(Stateinfo chage)
    {
        if (chage.state == state.None) return;
        if (chage.state == state.Stun) StartCoroutine(StateChage_Stun(chage.timer));
        if (chage.state == state.Slow) StartCoroutine(StateChage_Slow(chage.timer, chage.slowdownRate));
    }
    IEnumerator StateChage_Stun(float timer)
    {
        animator.SetTrigger("isStun");
        playerStun = state.Stun;
        yield return new WaitForSeconds(timer);
        playerStun = state.None;
        animator.SetTrigger("isStunEnd");
        yield break;
    }
    IEnumerator StateChage_Slow(float timer, float slow)
    {
        speed = basicSpeed;
        speed = speed * ((100 - slow) / 100);
        playerSlow = state.Slow;
        yield return new WaitForSeconds(timer);
        playerSlow = state.None;
        speed = basicSpeed;
        yield break;
    }
    public virtual void BasicAttack()
    {
        GameObject hitbox;
        isAttack = true;
        animator.SetTrigger("attack1");
        if (left == true)
        {
            hitbox = Instantiate(basicAttack, (Vector2)this.transform.position + new Vector2(-0.6f, 0), Quaternion.identity);
            hitbox.SendMessage("Name", selectPlayer, SendMessageOptions.DontRequireReceiver);
            Destroy(hitbox, 1f);
        }
        else
        {
            hitbox = Instantiate(basicAttack, (Vector2)this.transform.position + new Vector2(0.6f, 0), Quaternion.identity);
            hitbox.SendMessage("Name", selectPlayer, SendMessageOptions.DontRequireReceiver);
            Destroy(hitbox, 1f);
        }
        StartCoroutine(BasicAttack_timer(basicAttackSpeed));
    }
    public virtual IEnumerator BasicAttack_timer(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = false;
        yield break;
    }
    public virtual void UniqueSkill()
    {
        StartCoroutine(UniqueSkill_timer(uniqueSkillCooltime));
    }
    public virtual IEnumerator UniqueSkill_timer(float time)
    {
        GameObject hitbox;
        isUnique = true;
        isDonMove = true;
        animator.SetTrigger("isSpell1");
        yield return new WaitForSeconds(0.01f);
        if (left == true)
        {
            hitbox = Instantiate(uniqueSkill, (Vector2)this.transform.position + new Vector2(-1f, -0.5f), Quaternion.identity);
            hitbox.GetComponentInChildren<ParticleSystemRenderer>().flip = new Vector2(1, 0);
            hitbox.SendMessage("Name", selectPlayer, SendMessageOptions.DontRequireReceiver);
            rigidbody.velocity = new Vector2(-20, 0);
            Destroy(hitbox, 1f);
        }
        else
        {
            hitbox = Instantiate(uniqueSkill, (Vector2)this.transform.position + new Vector2(1f, -0.5f), Quaternion.identity);
            hitbox.GetComponentInChildren<ParticleSystemRenderer>().flip = new Vector2(0, 0);
            hitbox.SendMessage("Name", selectPlayer, SendMessageOptions.DontRequireReceiver);
            rigidbody.velocity = new Vector2(20, 0);

            Destroy(hitbox, 1f);
        }
        yield return new WaitForSeconds(0.3f);
        isDonMove = false;

        yield return new WaitForSeconds(time - 0.2f);
        isUnique = false;
        yield break;
    }
    public virtual void SpecialSkill()
    {
        StartCoroutine(SpecialSkill_timer(specialSkillCooltime));
    }
    public virtual IEnumerator SpecialSkill_timer(float time)
    {
        GameObject hitbox;
        isSpecial = true;
        isDonMove = true;
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
        yield return new WaitForSeconds(time - 0.2f);
        isSpecial = false;
        isDonMove = false;
        yield break;
    }


}
