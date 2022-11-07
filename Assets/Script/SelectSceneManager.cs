using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoSingleTon<SelectSceneManager>
{
    private void Update()
    {
        if (GameManager.Instance.Player1Done && GameManager.Instance.Player2Done)
        {
            GameManager.Instance.SceneNum = 2;
            SceneManager.LoadScene("2.BattleScene");
            GameManager.Instance.TimerStart();
        }
    }
}
