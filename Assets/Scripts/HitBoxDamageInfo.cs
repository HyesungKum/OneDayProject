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
        //�ڽ��� ������ �÷��̾ Ȯ��
        target = this.tag[0];//Ÿ���� 1~2
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
