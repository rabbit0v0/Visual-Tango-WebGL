using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// "isMoving" class is defined in around.cs
/// "streaming" class, "total" class are defined in save.cs
/// "isTurning" class is defined in Turn.cs, which actually does the turning.
/// </summary>

// an index to control which pose to play
public static class ind
{
    public static int i = -1;
}

// what happens when "play" button (or step) is clicked
public class play : MonoBehaviour
{
    int status = 0;
    GameObject stepback;
    // Start is called before the first frame update
    void Start()
    {
        stepback = GameObject.Find("stepback");
        stepback.SetActive(false);
    }

    // start playing
    void FixedUpdate()
    {
        Text text = gameObject.GetComponent<Button>().GetComponentInChildren<Text>();
        Toggle step = GameObject.Find("Toggle").GetComponent<Toggle>();
        // to see whether we should play step by step or not.
        if (step.isOn)
        {
            text.text = "step";
            stepback.SetActive(true);
        }
        else
        {
            text.text = "play";
            stepback.SetActive(false);
        }
        // play all in a row then stop.
        // when finished, ind.i must = -1.
        if (status == 1)
        {
            if (ind.i >= streaming.l.Count - 1 || ind.i < -1)
            {
                //go(ind.i);
                ind.i = -1;
                status = 0;
                return;
            }
            ind.i = 1 + ind.i;
            go(ind.i);
        }
        // step by step
        else if (status == 2)
        {
            // step forward
            // can stop when ind.i in [0, tot-1].
            if (!stepback.GetComponent<Toggle>().isOn)
            {
                ind.i = ind.i + 1;
                if (ind.i >= streaming.l.Count)
                {
                    ind.i = 0;
                    go(ind.i);
                    status = 0;
                    return;
                }
                go(ind.i);
                
                status = 0;
            }
            // step back
            // can stop when ind.i in [0, tot-1].
            else
            {
                ind.i = ind.i - 1;
                if (ind.i <= -1)
                {
                    ind.i = streaming.l.Count - 1;
                    go(ind.i);
                    status = 0;
                    return;
                }
                go(ind.i);
                status = 0;
            }
        }
    }
    // add the listener to the button
    void Awake()
    {
        Button button = gameObject.GetComponent<Button>() as Button;
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Toggle step = GameObject.Find("Toggle").GetComponent<Toggle>();
        if (step.isOn)
            status = 2;// play step by step
        else
            status = 1;// play in a row
    }
    // make the pose
    void go(int k)
    {
        if (k == -1)// for the last step, if there is any movement, do it.
        {
            Pose pp = streaming.l[total.tot-1];
            stepAround(pp);
            return;
        }
        Debug.Log(k);
        Debug.Log(streaming.l[k].r);
        Move(streaming.l[k]);
        stepAround(streaming.l[k]);
        turnAround(streaming.l[k]);
    }

    // rotate
    void turnAround(Pose p)
    {
        isTurning.r = p.r;
    }

    int pre = 0;
    int pre_w = 0;

    // calculate how far the women moves totally
    // "isMoving" class is defined in around.cs
    void stepAround(Pose p)
    {
        GameObject model = GameObject.Find("dance");
        GameObject leg_r = GameObject.Find("RightUpLeg");
        GameObject leg_rr = GameObject.Find("RightLeg");
        GameObject leg_l = GameObject.Find("LeftUpLeg");
        GameObject leg_ll = GameObject.Find("LeftLeg");
        if (pre_w == 0) // in the former pose, she weighted on the right leg
        {
            if (p.w == 1)// change the weight to the left leg, so move to where the left feet is
            {
                isMoving.m = 1;
                isMoving.tmp = model.transform.position;
                isMoving.xx = (float)(Mathf.Sin(leg_l.transform.rotation.x) * 0.95 + Mathf.Sin(leg_ll.transform.rotation.x) * 0.45);
                isMoving.zz = (float)(-Mathf.Sin(leg_l.transform.rotation.z) * 0.95 - Mathf.Sin(leg_ll.transform.rotation.z) * 0.45);
            }
            else
                return;
        }
        else // in the former pose, she weighted on the left leg
        {
            if (p.w == 0) // change the weight to the right leg
            {
                isMoving.m = 1;
                isMoving.tmp = model.transform.position;
                isMoving.xx = (float)(Mathf.Sin(leg_r.transform.rotation.x) * 0.95 + Mathf.Sin(leg_rr.transform.rotation.x) * 0.45);
                isMoving.zz = (float)(-Mathf.Sin(leg_r.transform.rotation.z) * 0.95 - Mathf.Sin(leg_rr.transform.rotation.z) * 0.45);
                //model.transform.position = tmp + new Vector3((float)(-Mathf.Sin(leg_r.transform.rotation.x) * 0.95 - Mathf.Sin(leg_rr.transform.rotation.x) * 0.5), 0, (float)(-Mathf.Sin(leg_r.transform.rotation.z) * 0.95 - Mathf.Sin(leg_rr.transform.rotation.z) * 0.5));
            }
            else
                return;
        }
        pre_w = p.w;
    }

    // change the pose
    void Move(Pose p)
    {
        GameObject model = GameObject.Find("dance");
        GameObject leg_r = GameObject.Find("RightUpLeg");
        GameObject leg_l = GameObject.Find("LeftUpLeg");
        Animator ani = leg_l.GetComponent<Animator>();
        Animator ani0 = leg_r.GetComponent<Animator>();

        // change timescale according to the "speed" sliders
        // if timescale is 2, the timer is 2 times faster, while when timescale is 0.5, the timer is 2 times slower.
        Time.timeScale = p.t + 0.50f;
        int overall = (int)GameObject.Find("overAllTime").GetComponent<Slider>().value;
        Time.timeScale = Time.timeScale * overall * 0.10f;
        //Debug.Log(Time.timeScale);
        if (p.w == 0) // weighted: right leg
        {
            ani0.SetInteger("state", 0);
            if (p.h == 1) // height: bend
            {
                ani0.SetInteger("hi", 1);
                ani.SetInteger("hi", 1);
            }
            else if (p.h == 2) // height: tiptoe
            {
                ani0.SetInteger("hi", 2);
                ani.SetInteger("hi", 0);
            }
            else // height: straight
            {
                ani0.SetInteger("hi", 0);
                ani.SetInteger("hi", 0);
            }
            switch (p.p) // free leg pose
            {
                case 0:
                    ani.SetInteger("state", 2);
                    break;
                case 1:
                    ani.SetInteger("state", 3);
                    break;
                case 2:
                    ani.SetInteger("state", 1);
                    break;
                case 3:
                    ani.SetInteger("state", 4);
                    break;
                case 4:
                    ani.SetInteger("state", 5);
                    break;
                case 5:
                    ani.SetInteger("state", 6);
                    break;
                case 6:
                    ani.SetInteger("state", 7);
                    break;
                case 7:
                    ani.SetInteger("state", 8);
                    break;
                case 8:
                    ani.SetInteger("state", 9);
                    break;
                case 9:
                    ani.SetInteger("state", 10);
                    break;
            }

        }
        else
        {
            ani.SetInteger("state", 0);
            if (p.h == 1)
            {
                ani0.SetInteger("hi", 1);
                ani.SetInteger("hi", 1);
            }
            else if (p.h == 2)
            {
                ani0.SetInteger("hi", 0);
                ani.SetInteger("hi", 2);
            }
            else
            {
                ani0.SetInteger("hi", 0);
                ani.SetInteger("hi", 0);
            }
            switch (p.p)
            {
                case 0:
                    ani0.SetInteger("state", 2);
                    break;
                case 1:
                    ani0.SetInteger("state", 3);
                    break;
                case 2:
                    ani0.SetInteger("state", 1);
                    break;
                case 3:
                    ani0.SetInteger("state", 4);
                    break;
                case 4:
                    ani0.SetInteger("state", 5);
                    break;
                case 5:
                    ani0.SetInteger("state", 6);
                    break;
                case 6:
                    ani0.SetInteger("state", 7);
                    break;
                case 7:
                    ani0.SetInteger("state", 8);
                    break;
                case 8:
                    ani0.SetInteger("state", 9);
                    break;
                case 9:
                    ani0.SetInteger("state", 10);
                    break;
            }

        }
        GameObject woman = GameObject.Find("Spine");
        Animator ani1 = woman.GetComponent<Animator>();
        switch (p.d) // the face direction
        {
            case 0:
                ani1.SetInteger("face", 0);

                break;
            case 1:
                ani1.SetInteger("face", 1);

                break;
            case 2:
                ani1.SetInteger("face", 2);

                break;
        }
        Animator ani2 = model.GetComponent<Animator>();
        switch (p.h) // the height
        {
            case 0:
                ani2.SetInteger("state", 2);
                //ani0.SetInteger("hi", 0);
                //ani.SetInteger("hi", 0);
                break;
            case 1:
                ani2.SetInteger("state", 1);
                //ani0.SetInteger("hi", 1);
                //ani.SetInteger("hi", 1);
                break;
            case 2:
                ani2.SetInteger("state", 3);
                //ani0.SetInteger("hi", 2);
                //ani.SetInteger("hi", 2);
                break;
        }
    }
}
