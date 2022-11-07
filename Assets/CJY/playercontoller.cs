using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontoller : MonoBehaviour
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
    [Header("HitBoxPrefab")]
    [SerializeField] private GameObject basicAttack;
    private float xinput;
    private float zinput;
    private float xspeed;
    private bool left;
    private bool isAttack;

    void Start()
    {
        speed = 700;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (selectPlayer == 1) gameObject.tag = $"{selectPlayer}Player";
        else gameObject.tag = $"{selectPlayer}Player";
    }
    void Update()
    {
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
            if (Input.GetKeyDown(KeyCode.KeypadDivide))
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
            rigidbody.AddForce(new Vector2(xspeed, 70), ForceMode2D.Impulse);
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

        Debug.Log(animator.GetBool("isGround"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator.GetBool("isGround") == false)
        {
            if (collision.gameObject.tag == "Ground")
            {
                if (collision.contacts[0].normal.y > 0f)
                    animator.SetBool("isGround", true);

            }
        }
    }

    void BasicAttack()
    {
        GameObject hitbox;
        Debug.Log("АјАн");
        isAttack = true;
        animator.SetTrigger("attack1");
        if (left == true)
        {
            hitbox = Instantiate(basicAttack, (Vector2)this.transform.position + new Vector2(-0.6f, 0), Quaternion.identity);
            Destroy(hitbox, 1f);
        }
        else
        {
            hitbox = Instantiate(basicAttack, (Vector2)this.transform.position + new Vector2(0.6f, 0), Quaternion.identity);
            Destroy(hitbox, 1f);
        }
        hitbox.tag = $"{selectPlayer}HitBox";
        StartCoroutine(BasicAttack_timer(basicAttackSpeed));
    }

    void GetDamage(float damage)
    {
        curHp += -damage;
        if (curHp < 0) curHp = 0;
    }
    IEnumerator BasicAttack_timer(float time)
    {
        yield return new WaitForSeconds(time);
        isAttack = false;
        yield break;
    }

}
