using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleTon<UIManager>
{
    [SerializeField] bool DamageDebug = false;
    //[Header("Player1_Info")]
    [field: SerializeField] public Slider childSlider1 { get; set; }
    [SerializeField] Slider playerSlider1 = null;
    [SerializeField] bool p1Win = false;

    //[Header("Player2_Info")]
    [field: SerializeField] public Slider childSlider2 { get; set; }
    [SerializeField] Slider playerSlider2 = null;
    [SerializeField] bool p2Win = false;


    [SerializeField] float lerpScale = 0f;

    [SerializeField] Text time = null;
    [SerializeField] Text drawText = null;
    [SerializeField] Text p1WinText = null;
    [SerializeField] Text p2WinText = null;


    [SerializeField] Text leftText = null;
    [SerializeField] Text rightText = null;

    private void Awake()
    {
        time = GameObject.Find("Time").GetComponent<Text>();

        lerpScale = 0.07f;

        leftText.text = GameManager.Instance.LeftPlayer;
        rightText.text = GameManager.Instance.RightPlayer;
    }

    void Start()
    {
        GameManager.Instance.TimerStart();

    }

    // Update is called once per frame
    void Update()
    {
        time.text = ((int)GameManager.Instance.TimerCount).ToString();
        if (GameManager.Instance.TimerCount<=0) // 게임종료
        {
            TimeOver();
        }

        #region Debug function
        #if UNITY_EDITOR
        if (DamageDebug)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                P1Hit(10);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                P2Hit(10);
            }
        }
        #endif
        #endregion

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

    public void P1Hit(float damage)
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

    public void P2Hit(float damage)
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
            GameManager.Instance.GameOver = true;
        }
        else if (playerSlider1.value < playerSlider2.value)
        {
            P2Win();
            GameManager.Instance.GameOver = true;
        }
        else
        {
            Draw();
            GameManager.Instance.GameOver = true;
        }
    }
}
