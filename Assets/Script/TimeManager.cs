using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Text time = null;
    [SerializeField] float setTime = 0f;
    [SerializeField] bool win = false;

    private void Start()
    {
        setTime = 99f;
         StartCoroutine(Time());
    }

    IEnumerator Time()
    {
        while (true)
        {
            if (setTime > 0)
            {
                setTime--;
                time.text = setTime.ToString();
                yield return new WaitForSeconds(1f);
            }
            else yield break;
        }

    }
}
