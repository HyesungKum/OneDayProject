using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDamageInfo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float damage;
    [SerializeField] public float timer;

    private char target = ' ';

    private void Awake()
    {
        //자신을 생성한 플레이어를 확인
        target = this.tag[0];//타겟은 1~2
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "1Player" || collision.gameObject.tag == "2Player")
        {
            if (target != collision.gameObject.tag[0])
            {
                collision.gameObject.SendMessage("GetDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
