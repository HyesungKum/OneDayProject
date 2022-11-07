using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject pc1 = null;
    GameObject pc2 = null;

    Camera cam;

    float dis = 0f;
    float oldDis = 0f;
    float zPos = 0f;
    private void Awake()
    {
        pc1 = GameObject.FindWithTag("1Player");
        pc2 = GameObject.FindWithTag("2Player");

        dis = Vector3.Distance(pc1.transform.position, pc2.transform.position);

        cam = Camera.main;

        oldDis = dis;
    }

    void Update()
    {
        Debug.Log(dis);
        dis = Vector3.Distance(pc1.transform.position, pc2.transform.position);
        cam.transform.position = pc1.transform.position + (-0.5f *(pc1.transform.position - pc2.transform.position));
        cam.transform.position = new Vector3(0, 0,-10f);
        if (oldDis < dis)
        {
            if (cam.orthographicSize >= 13f) return;
            oldDis = dis;
            cam.orthographicSize += 0.1f;
        }
        else if (oldDis > dis)
        {
            if (cam.orthographicSize <= 6f) return;
            oldDis = dis;
            cam.orthographicSize -= 0.1f;
        }
        else
        {
            oldDis = dis;
        }



    }










}
