using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    [SerializeField] Slider childSlider = null;
    [SerializeField] Slider playerSlider = null;
    [SerializeField] float lerpScale = 0f;

    private void Awake()
    {
        lerpScale = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestDamage();
        }

        if (childSlider != null && playerSlider != null)
        {
            if (childSlider.value != playerSlider.value)
            {
                childSlider.value -= lerpScale;
            } 
        }
    }

    void TestDamage()
    {
        playerSlider.value -= 10;
    }
}
