using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceenManager : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text teamName;
    [SerializeField] Button startButton;

    public void OnStartButton()
    {
        GameManager.Instance.SceneNum = 1;
        SceneManager.LoadScene("1.SelectScene");
    }
}
