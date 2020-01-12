using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayEast : DisplayBase
{
    public GameObject windowButton;
    public GameObject windowCurtainL;
    public GameObject windowCurtainR;
    private int wButtonCount;
    // Start is called before the first frame update
    void Start()
    {
        category = DisplayCategory.East;
        InitDisplay();
        wButtonCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        windowCurtainL.transform.localScale = new Vector3(0.2f, 1.0f, 1.0f);
        windowCurtainL.transform.position = new Vector3(-2.0f, 0.92f, 0.0f);
        windowCurtainR.transform.localScale = new Vector3(0.5f, 1.0f, 1.0f);

        if (wButtonCount == 0)
        {
            windowCurtainL.transform.localScale = new Vector3(0.2f, 1.0f, 1.0f);
           // windowCurtainL.transform.position = new Vector3(-2.0f, 0.92f, 0.0f);
            windowCurtainL.transform.position = new Vector3(-2.3f, 0.92f, 0.0f);
            windowCurtainR.transform.position = new Vector3(0.5f, 0.92f, 0.0f);
            windowCurtainR.transform.localScale = new Vector3(0.2f, 1.0f, 1.0f);
        }
    }
}
