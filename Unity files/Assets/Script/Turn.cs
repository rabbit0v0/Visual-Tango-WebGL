using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// do the turnings in each frame
public class Turn : MonoBehaviour
{
    private Transform thisTransform;
    public float timerNow = 0;
    //float[] angle = { 0, 30, 60, 90, 120, 150, 180, 270, 360 };
    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurning.r != 0)
            playAnimation(isTurning.r);
    }
    // calculate how much model turns in each frame, and do it
    public void playAnimation(float a)

    {
        
        float allTimer = 0.20f / Time.timeScale;
        float del_a = a * Time.deltaTime / allTimer;
        if (timerNow < allTimer)
        {
            thisTransform.Rotate(0, del_a, 0, Space.Self);
            timerNow += Time.deltaTime;

        }

        else

        {
            timerNow = 0;
            isTurning.r = 0;
        }

    }
}
public static class isTurning
{
    public static int r = 0;
}