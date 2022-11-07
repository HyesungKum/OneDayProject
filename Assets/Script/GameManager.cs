using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleTon<GameManager>
{
    #region 1P status
    //player 1 status
    public bool Player1Done { get; set; }
    public int Player1SetPos { get; set; }
    #endregion

    #region 2P status
    //player 2 status
    public bool Player2Done { get; set; }
    public int Player2SetPos { get; set; }
    #endregion

    public string LeftPlayer { get; set; }
    public string RightPlayer { get; set; }

    //game system controll
    public int SceneNum { get; set; }
    public float TimerCount { get; set; }
    public bool TimeOver { get; set; }
    public bool Player1Win { get; set; }
    public bool Player2Win { get; set; }

    //call gamemanager
    bool Call { get; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Initializing();
    }

    void Update()
    {
        if (GameManager.Instance.SceneNum == 2)
        {
        }
    }

    public void Initializing()
    {
        GameManager.Instance.TimerCount = 0f;
        GameManager.Instance.TimeOver = false;

        GameManager.Instance.TimerInit();

        GameManager.Instance.Player1Win = false;
        GameManager.Instance.Player2Win = false;

        GameManager.Instance.SceneNum = 0;

        //players init

        GameManager.Instance.Player1SetPos = 0;
        GameManager.Instance.Player2SetPos = 0;
    }

    #region timer controll
    IEnumerator enumeratorT = null;
    public void TimerStart()
    {
        if(enumeratorT != null)
            StartCoroutine(enumeratorT);
    }
    public void TimerStop()
    {
        if(enumeratorT != null)
            StopCoroutine(enumeratorT);
    }
    IEnumerator timerEnumerator()
    {
        while (GameManager.Instance.TimerCount <= 60f)
        {
            GameManager.Instance.TimerCount += Time.deltaTime;
            Debug.Log($"current Time : {(int)GameManager.Instance.TimerCount}");
            yield return null;
        }

        GameManager.Instance.TimeOver = true;
        TimerDestroy();
    }
    public void TimerInit()
    {
        enumeratorT = timerEnumerator();
    }
    public void TimerDestroy()
    {
        if (enumeratorT != null)
            enumeratorT = null;
    }
    #endregion
}