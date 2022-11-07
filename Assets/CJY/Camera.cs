using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameObject pc1 = null;

    Vector3 camerPos=Vector3.zero;

    private void Awake()
    {
        pc1 = GameObject.FindWithTag("1Player");

        camerPos = new Vector3(0, 0, -10);
    }

    void Update()
    {
        transform.position = pc1.transform.position+ pc1.transform.position + camerPos;
    }
}
