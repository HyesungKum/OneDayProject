using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleTon<UIManager>
{
    [SerializeField] bool DamageDebug = false;
    //[Header("Player1_Info")]
    [field: SerializeField] public Slider leftChildSlider { get; set; }
    [SerializeField] Slider leftSlider = null;
    [SerializeField] bool p1Win = false;

    //[Header("Player2_Info")]
    [field: SerializeField] public Slider rightChildSlider { get; set; }
    [SerializeField] Slider rightSlider = null;
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

        if (leftChildSlider != null && leftSlider != null)
        {
            RightRedSlider();
            LeftRedSlider();
        }
    }

    public void P1Hit(float damage)
    {
        if (GameManager.Instance.LeftPlayer == "Player1")
        {
            leftSlider.value -= damage;
            if (leftSlider.value <= 0)
            {
                P2Win();
            }
        }
        else if (GameManager.Instance.RightPlayer == "Player1")
        {
            rightSlider.value -= damage;
            if (rightSlider.value <= 0)
            {
                P2Win();
            }
        }
    }
    public void P2Hit(float damage)
    {
        if (GameManager.Instance.LeftPlayer == "Player2")
        {
            leftSlider.value -= damage;
            if (leftSlider.value <= 0)
            {
                P1Win();
            }
        }
        else if (GameManager.Instance.RightPlayer == "Player2")
        {
            rightSlider.value -= damage;
            if (rightSlider.value <= 0)
            {
                P1Win();
            }
        }
    }

    void LeftRedSlider()
    {
        if (leftChildSlider.value > leftSlider.value)
        {
            leftChildSlider.value -= lerpScale;
        }
        else leftChildSlider.value = leftSlider.value;
    }

    void RightRedSlider()
    {
        if (rightChildSlider.value > rightSlider.value)
        {
            rightChildSlider.value -= lerpScale;
        }
        else rightChildSlider.value = rightSlider.value;
    }

    public void P1Win()
    {
        p1WinText.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.GameOver = true;
    }
    public void P2Win()
    {
        p2WinText.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.GameOver = true;
    }
    void Draw()
    {
        drawText.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.GameOver = true;
    }

    void TimeOver()
    {
        if (leftSlider.value > rightSlider.value)
        {
            P1Win();
            GameManager.Instance.GameOver = true;
        }
        else if (leftSlider.value < rightSlider.value)
        {
            P2Win();
            GameManager.Instance.GameOver = true;
        }
        else
        {
            Draw();
            GameManager.Instance.GameOver = true;
        }
        Time.timeScale = 0f;
        GameManager.Instance.GameOver = true;
    }
}
