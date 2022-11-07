using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum state
{
    None,
    Stun,
    Slow,
    invincibility,
}
public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [Header("PlayerPick")]
    [SerializeField] private int selectPlayer;
    [Header("PlayerInfo")]
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;
    [SerializeField] private float basicAttackDamage;
    [SerializeField] private float basicAttackSpeed;
    [SerializeField] private float speed;
    [Header("PlayerState")]
    [SerializeField] private state playerStun;
    [SerializeField] private state playerSlow;
    [SerializeField] private state playerInvincibility;
    [Header("HitBoxPrefab")]
    [SerializeField] private GameObject basicAttack;
    private float xinput;
    private float zinput;
    private float xspeed;
    private bool left;
    private bool isAttack;
    private bool isDeath;
    private float basicSpeed;

    void Start()
    {
        basicSpeed = speed;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (selectPlayer == 1) gameObject.tag = $"{selectPlayer}Player";
        else gameObject.tag = $"{selectPlayer}Player";
    }
    void Update()
    {
        if (isDeath == true) return;
        if (playerStun == state.Stun) return;
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
            }
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                
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
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
            }

            if (Input.GetKey(KeyCode.R)) zinput = 1;
            else zinput = 0;
        }
        xspeed = speed * xinput * Time.deltaTime;
        if (zinput > 0 && animator.GetBool("isGround") == true)
        {
            rigidbody.AddForce(new Vector2(xspeed, speed / 10), ForceMode2D.Impulse);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator.GetBool("isGround") == false)
        {
            if (collision.gameObject.tag == "Ground")
            {
                if (collision.contacts[0].normal == Vector2.up)
                    animator.SetBool("isGround", true);
            }
        }
    }

    void GetDamage(float damage)
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

    }
    void StateChage(Stateinfo chage)
    {
        if (chage.state == state.None) return;
        if (chage.state == state.Stun) StartCoroutine(StateChage_Stun(chage.timer));
        if (chage.state == state.Slow) StartCoroutine(StateChage_Slow(chage.timer,chage.slowdownRate));
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
    IEnumerator StateChage_Slow(float timer,float slow)
    {
        speed = basicSpeed;
        speed = speed*((100-slow)/100);
        playerSlow = state.Slow;
        yield return new WaitForSeconds(timer);
        playerSlow = state.None;
        speed = basicSpeed;
        yield break;
    }

    void BasicAttack()
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
    IEnumerator BasicAttack_timer(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = false;
        yield break;
    }

}
