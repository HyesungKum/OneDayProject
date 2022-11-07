using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Stateinfo
{
    public state state;
    public float timer;
    public float slowdownRate;
}
public class HitBoxDamageInfo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float damage;
    [SerializeField] private float timer;
    [SerializeField] private float slowdownRate;
    [SerializeField] private state state;
    private Stateinfo stateChage;
    private char target = ' ';


    void Name(int num)
    {
        gameObject.tag = $"{num}HitBox";
        target = gameObject.tag[0];//타겟은 1~2
    }
    private void Awake()
    {
        //자신을 생성한 플레이어를 확인
        stateChage.state = state;
        stateChage.timer = timer;
        stateChage.slowdownRate = slowdownRate;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "1Player" || collision.gameObject.tag == "2Player")
        {
            if (target != collision.gameObject.tag[0])
            {
                collision.gameObject.SendMessage("GetDamage", damage, SendMessageOptions.DontRequireReceiver);
                collision.gameObject.SendMessage("StateChage",stateChage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
