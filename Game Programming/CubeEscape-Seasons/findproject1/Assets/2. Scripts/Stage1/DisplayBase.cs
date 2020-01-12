using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBase : MonoBehaviour
{
    public DisplayCategory category = DisplayCategory.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitDisplay()
    {
        if (category == DisplayCategory.None)
            return;

        //arrows.arrow_left.SetActive(true);
        //arrows.arrow_right.SetActive(true);
        //arrows.arrow_up.SetActive(true);

        //if (category == DisplayCategory.Ceil)
        //{
        //    arrows.arrow_down.SetActive(true);
        //}
        //else
        //{
        //    arrows.arrow_down.SetActive(false);
        //}

    }
}
