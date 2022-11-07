using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //[Header("Player1_Info")]
    [SerializeField] Slider childSlider1 = null;
    [SerializeField] Slider playerSlider1 = null;
    [SerializeField] bool p1Win = false;

    //[Header("Player2_Info")]
    [SerializeField] Slider childSlider2 = null;
    [SerializeField] Slider playerSlider2 = null;
    [SerializeField] bool p2Win = false;


    [SerializeField] float lerpScale = 0f;

    [SerializeField] Text time = null;
    [SerializeField] Text drawText = null;
    [SerializeField] Text p1WinText = null;
    [SerializeField] Text p2WinText = null;

    private void Awake()
    {
        time = GameObject.Find("Time").GetComponent<Text>();

        lerpScale = 0.07f;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //time.text = GameManager.Instace.TimerCount;
        //if (GameManager.Instance.TimeOut) // ��������
        //{
        //    TimeOver();
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            P1Hit(10);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            P2Hit(10);
        }

        if (childSlider1 != null && playerSlider1 != null)
        {
            P1RedSlider();
            P2RedSlider();
        }
    }

    public void P1Win()
    {
        p1WinText.gameObject.SetActive(true);
        //GameManager.Instance.Player2Win = true;
    }

    public void P1Hit(int damage)
    {
        playerSlider1.value -= damage;
        if (playerSlider1.value <= 0)
        {
            P2Win();
        }
    }

    void P1RedSlider()
    {
        if (childSlider1.value > playerSlider1.value)
        {
            childSlider1.value -= lerpScale;
        }
        else childSlider1.value = playerSlider1.value;
    }

    public void P2Win()
    {
        p2WinText.gameObject.SetActive(true);
        //GameManager.Instance.Player2Win = true;
    }

    public void P2Hit(int damage)
    {
        playerSlider2.value -= damage;
        if (playerSlider2.value <= 0)
        {
            P1Win();
        }
    }

    void P2RedSlider()
    {
        if (childSlider2.value > playerSlider2.value)
        {
            childSlider2.value -= lerpScale;
        }
        else childSlider2.value = playerSlider2.value;
    }

    void Draw()
    {
        drawText.gameObject.SetActive(true);
        //GameManager.Instance.Player1Win = false;
        //GameManager.Instance.Player2Win = false;
    }

    void TimeOver()
    {
        if (playerSlider1.value > playerSlider2.value)
        {
            P1Win();
        }
        if (playerSlider1.value < playerSlider2.value)
        {
            P2Win();
        }
        else
        {
            Draw();
        }
    }
}
