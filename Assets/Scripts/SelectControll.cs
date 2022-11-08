using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectControll : MonoBehaviour
{
    //area transform
    Transform[] transforms = new Transform[3];

    //player select index;
    int index = 2;
    int charIndex = 0;

    //sprite list
    List<Sprite> charSprites = new List<Sprite>();

    //targetwindow
    GameObject charWindow = null;

    //player select state
    int selectState = 0;


    [Header("[move]")]
    [SerializeField] KeyCode up = KeyCode.W;
    [SerializeField] KeyCode down = KeyCode.S;
    [SerializeField] KeyCode right = KeyCode.D;
    [SerializeField] KeyCode left = KeyCode.A;

    [Header("select")]
    [SerializeField] KeyCode choice = KeyCode.Alpha1;

    [SerializeField] GameObject lCharWindow = null;
    [SerializeField] GameObject rCharWindow = null;

    private void Awake()
    {
        charSprites = SelectSceneManager.Instance.charList;

        transforms[0] = GameObject.Find("LeftArea").transform;
        transforms[1] = GameObject.Find("RightArea").transform;
        transforms[2] = GameObject.Find("MiddleArea").transform;
    }

    private void Start()
    {
        if(lCharWindow.activeSelf)
            lCharWindow.SetActive(false);

        if (rCharWindow.activeSelf)
            rCharWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SelectPos();
        SelectChar();
    }

    void SelectPos()
    {
        if (selectState != 0)
            return;

        if (Input.GetKeyDown(up))
        {
            for (int i = 0; i < 2; i++)
            {
                if (transforms[i].childCount < 2)
                {
                    index = i;
                    this.transform.parent = transforms[i];
                }
            }
        }

        if (Input.GetKeyDown(down))
        {
            index = 2;
            this.transform.parent = transforms[2];//go middle
        }

        if (Input.GetKeyDown(left))
        {
            if (index == 0) return;

            if (transforms[0].childCount < 2)
            {
                index = 0;
                this.transform.parent = transforms[0];//go left
            }
            else
            {
                StartCoroutine(Shake());
            }
        }

        if (Input.GetKeyDown(right))
        {
            if (index == 1) return;

            if (transforms[1].childCount < 2)
            {
                index = 1;
                this.transform.parent = transforms[1];//go right 
            }
            else
            {
                StartCoroutine(Shake());
            }
        }

        if (Input.GetKeyUp(choice))
        {
            if (this.transform.parent != transforms[2])
            {
                GameManager.Instance.Player2SetPos = index;
                this.GetComponent<Image>().color += new Color(0.5f, 0.5f, 0.5f);

                //test
                if (this.name == "Player1Controller")
                {
                    if (index == 1)//오른쪽
                    {
                        GameManager.Instance.RightPlayer = "Player1";
                        rCharWindow.SetActive(true);
                    }
                    else//왼쪽
                    {
                        GameManager.Instance.LeftPlayer = "Player1";
                        lCharWindow.SetActive(true);
                    }

                    GameManager.Instance.Player1Ready = true;
                }
                else if(this.name == "Player2Controller")
                {
                    if (index == 1)//오른쪽
                    {
                        GameManager.Instance.RightPlayer = "Player2";
                        rCharWindow.SetActive(true);
                    }
                    else//왼쪽
                    {
                        GameManager.Instance.LeftPlayer = "Player2";
                        lCharWindow.SetActive(true);
                    }

                    GameManager.Instance.Player2Ready = true;
                }

                //캐릭터 선택화면으로 들어가기
                selectState = 1;
            }
        }
    }

    void SelectChar()
    {
        if (selectState != 1)
            return;

        if (index == 0) //left window controll
        {
            charWindow = lCharWindow;
        }
        else //right window controll
        {
            charWindow = rCharWindow;
        }

        if (Input.GetKeyDown(left))
        {
            --charIndex;
            if (charIndex < 0)
            {
                charIndex = SelectSceneManager.Instance.charList.Count-1;
            }

            charWindow.GetComponent<Image>().sprite = charSprites[charIndex];
        }

        if (Input.GetKeyDown(right))
        {
            ++charIndex;

            if (charIndex > SelectSceneManager.Instance.charList.Count-1)
            {
                charIndex = 0;
            }

            charWindow.GetComponent<Image>().sprite = charSprites[charIndex];
        }


        if (Input.GetKeyDown(choice))
        {
            if (this.name == "Player1Controller")
            {
                GameManager.Instance.Player1Done = true;
            }

            if (this.name == "Player2Controller")
            {
                GameManager.Instance.Player2Done = true;
            }

            if (index == 0) //왼쪽
            {
                GameManager.Instance.leftCharIndex = charIndex;
            }
            
            if (index == 1)//오른쪽
            {
                GameManager.Instance.rightCharIndex = charIndex;
            }
            selectState = 2;
        }
    }

    IEnumerator Shake()
    {
        float timer = 0f;
        while(timer <= 0.2f)
        {
            timer += Time.deltaTime;
            float rand = Random.Range(-5f,5f);
            this.transform.Rotate(Vector3.forward * rand);
            yield return null;
        }
        this.transform.rotation = Quaternion.identity;
    }
}
