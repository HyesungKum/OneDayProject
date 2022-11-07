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

    //player select state
    int selectState = 0;

    [Header("[move]")]
    [SerializeField] KeyCode up = KeyCode.W;
    [SerializeField] KeyCode down = KeyCode.S;
    [SerializeField] KeyCode right = KeyCode.D;
    [SerializeField] KeyCode left = KeyCode.A;

    [Header("select")]
    [SerializeField] KeyCode choice = KeyCode.Alpha1;

    

    private void Awake()
    {
        transforms[0] = GameObject.Find("LeftArea").transform;
        transforms[1] = GameObject.Find("RightArea").transform;
        transforms[2] = GameObject.Find("MiddleArea").transform;
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
                Debug.Log(i);
                if (transforms[i].childCount == 0)
                {
                    
                    index = i;
                    this.transform.parent = transforms[i];
                    Debug.Log(transforms[i].name);
                }
            }
        }

        if (Input.GetKeyDown(down))
        {
            index = 2;
            this.transform.parent = transforms[2];//go middle
        }

        Debug.Log($"{this.name} : {index}");
        if (Input.GetKeyDown(left))
        {
            if (index == 0) return;

            if (transforms[0].childCount == 0)
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

            if (transforms[1].childCount == 0)
            {
                index = 1;
                this.transform.parent = transforms[1];//go right 
            }
            else
            {
                StartCoroutine(Shake());
            }
        }

        if (Input.GetKeyDown(choice))
        {
            if (transforms[2].childCount == 0)
            {
                GameManager.Instance.Player2SetPos = index;
                this.GetComponent<Image>().color += new Color(0.5f, 0.5f, 0.5f);

                //캐릭터 선택화면으로 들어가기
                selectState = 1;

                //test
                if (this.name == "Player1Controller")
                {
                    if (index == 2)//오른쪽
                    {
                        GameManager.Instance.RightPlayer = "Player1";
                    }
                    else//왼쪽
                    {
                        GameManager.Instance.LeftPlayer = "Player1";
                    }

                    GameManager.Instance.Player1Done = true;
                }
                else if(this.name == "Player2Controller")
                {
                    GameManager.Instance.Player2Done = true;
                    if (index == 2)//오른쪽
                    {
                        GameManager.Instance.RightPlayer = "Player2";
                    }
                    else//왼쪽
                    {
                        GameManager.Instance.LeftPlayer = "Player2";
                    }

                }
            }
        }
    }

    void SelectChar()
    {
        if (selectState != 1)
            return;

        Debug.Log($"{this.name} state : {GameManager.Instance.Player1SetPos}");
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
