using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    //Camera cam;
    private void Start()
    {
        
        //cam = GameObject.Find("Main Camara").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameManager.Instance.TimerStart();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GameManager.Instance.TimerStop();
        }

        
    }
}
