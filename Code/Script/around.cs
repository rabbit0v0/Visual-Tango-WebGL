using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class around : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationCurve xCurve;

    public AnimationCurve yCurve;

    public AnimationCurve zCurve;



    private Transform thisTransform;

    //public float allTimer = Time.timeScale;

    public float timerNow = 0;


    void Start()

    {

        thisTransform = this.GetComponent<Transform>();

    }

    // calculate how far the model moves in each frame
    // also affected by the curve in unity "dance" object
    public void playAnimation()

    {
        float allTimer = 0.20f/Time.timeScale;
        if (timerNow < allTimer)

        {

            float percent = timerNow / allTimer;
            //Debug.Log(timerNow / allTimer);
            //Debug.Log(thisTransform.position);

            Vector3 newPosition = thisTransform.right.normalized * xCurve.Evaluate(percent) * isMoving.xx + isMoving.tmp;

            newPosition -= thisTransform.forward.normalized * zCurve.Evaluate(percent) * isMoving.zz;

            thisTransform.position = newPosition;

            timerNow += Time.deltaTime;

        }

        else

        {
            timerNow = 0;

            isMoving.m = 0;
        }
        
    }

    

    void Update()

    {
        if (isMoving.m == 1)
            playAnimation();

    }

}

public static class isMoving
{
    public static int m = 0;
    public static float xx = 0;
    public static float zz = 0;
    public static Vector3 tmp = new Vector3(); 
}