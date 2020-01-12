using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeFunction : MonoBehaviour
{
    public UnityEngine.UI.Image fade;
    public float fades = 1.0f;
    public bool bFadeIn;
    public bool bFadeOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bFadeIn)
            FadeIn();
        if (bFadeOut)
            FadeOut();
    }

    public void FadeIn()
    {
        if (fades > 0.0f)
        {
            fades -= 0.02f;
            fade.color = new Color(0, 0, 0, fades);
        }
        else
        {
            fades = 0.0f;
            fade.enabled = false;
            bFadeIn = false;
        }
    }

    public void FadeOut()
    {
        fades += 0.01f;
        fade.color = new Color(0, 0, 0, fades);
        if (fades > 1.0f)
        {
            fades = 1.0f;
            bFadeOut = false;
        }
    }
}
