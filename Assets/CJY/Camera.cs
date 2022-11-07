using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameObject pc1 = null;
    GameObject pc2 = null;

    [SerializeField] Camera mainCamera;

    Transform caremraTr;

    Camera cam;

    Vector3 pos1;
    Vector3 pos2;

    float dis = 0f;

    float oldDis = 0f;

    private void Awake()
    {
        pc1 = GameObject.FindWithTag("1Player");
        pc2 = GameObject.FindWithTag("2Player");

        

        dis = Vector3.Distance(pc1.transform.position,pc2.transform.position);

        caremraTr = GetComponent<Transform>();
        caremraTr.position = new Vector3(0, 0, -20f);
        //caremraTr.position = (0.5f*(pc1.transform.position - pc2.transform.position))+ new Vector3(0, 0, -10f);
        oldDis = dis;
        Debug.Log($"Ω√¿€ {dis}");
    }

    void Update()
    {
        pos1 = pc1.transform.position;
        pos2 = pc2.transform.position;
        dis = Vector3.Distance(pc1.transform.position,pc2.transform.position);
        
        if (oldDis < dis)
        {
            oldDis = dis;
            caremraTr.position = pc1.transform.position+(0.5f *(pc1.transform.position - pc2.transform.position));

            caremraTr.position += Vector3.back*0.1f;
            //caremraTr.position = new Vector3((pos1.x - pos2.x)/2, (pos1.y - pos2.y)/2, -20)+Vector3.back*0.01f;




        }
        else if (oldDis > dis)
        {
            Debug.Log("hi");
            oldDis = dis;
            caremraTr.position = pc1.transform.position + (0.5f *(pc1.transform.position - pc2.transform.position));
            //caremraTr.position = new Vector3((pos1.x - pos2.x) / 2, (pos1.y - pos2.y) / 2, -20) + Vector3.forward * 0.01f;
            caremraTr.position += Vector3.forward * 0.1f;
        }
        else
        {
            oldDis = dis;
        }
             


    }










}
