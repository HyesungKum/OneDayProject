using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewControll : MonoBehaviour
{
    GameObject pc1 = null;
    GameObject pc2 = null;

    Camera cam;

    float dis1 = 0f;
    float dis2 = 0f;
    float oldDis1 = 0f;
    float oldDis2 = 0f;
    float zPos = 0f;

    Vector3 convertPos;

    bool Found = false;

    private void Start()
    {
        StartCoroutine(FindPlayer());

        cam = Camera.main;

        oldDis1 = dis1;
    }
    IEnumerator FindPlayer()
    {
        while (pc1 == null && pc2 == null)
        {
            pc1 = GameObject.FindWithTag("1Player");
            pc2 = GameObject.FindWithTag("2Player");

            yield return null;
        }
        Found = true;
    }

    void FixedUpdate()
    {
        if (!Found) return;

        dis1 = Vector3.Distance(Camera.main.transform.position, pc1.transform.position);
        dis2 = Vector3.Distance(Camera.main.transform.position, pc2.transform.position);
        Vector3 camPos = Vector3.zero;

        convertPos = new Vector3(Mathf.Abs(pc1.transform.position.x - pc2.transform.position.x) / 2
            , Mathf.Abs(pc1.transform.position.y - pc2.transform.position.y) / 2, -10f);


        if (pc1.transform.position.x < pc2.transform.position.x)
            camPos.x += (pc1.transform.position.x + convertPos.x);
        else
            camPos.x += (pc2.transform.position.x + convertPos.x);

        if (pc1.transform.position.y < pc2.transform.position.y)
            camPos.y += (pc1.transform.position.y + convertPos.y);
        else
            camPos.y += (pc2.transform.position.y + convertPos.y);


        cam.transform.position = camPos + new Vector3(0, 0, -10f);
        if (oldDis1 < dis1 && 13f < dis1)
        {
            if (cam.orthographicSize > 11f) return;
            oldDis1 = dis1;
            cam.orthographicSize += 0.1f;
        }
        else if (oldDis1 > dis1 && 13f > dis1)
        {
            if (cam.orthographicSize < 6f) return;
            oldDis1 = dis1;
            cam.orthographicSize -= 0.1f;
        }
        else
        {
            oldDis1 = dis1;
        }

        if (oldDis2 < dis2 && 13f < dis2)
        {
            if (cam.orthographicSize > 11f) return;
            oldDis2 = dis2;
            cam.orthographicSize += 0.1f;
        }
        else if (oldDis2 > dis2 && 13f > dis2)
        {
            if (cam.orthographicSize < 6f) return;
            oldDis2 = dis2;
            cam.orthographicSize -= 0.1f;
        }
        else
        {
            oldDis2 = dis2;
        }
    }
}


