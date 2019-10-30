using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// what happens when you change your selections of the dropdowns
// do not walk around and rotate

public class face : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Facedir").GetComponent<Dropdown>().onValueChanged.AddListener(facedir);
        GameObject.Find("Weighted").GetComponent<Dropdown>().onValueChanged.AddListener(weight);
        GameObject.Find("Pose").GetComponent<Dropdown>().onValueChanged.AddListener(pose);
        GameObject.Find("Position").GetComponent<Dropdown>().onValueChanged.AddListener(position);
        //GameObject.Find("Rotate").GetComponent<Dropdown>().onValueChanged.AddListener(rotate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int dir = 0;
    int wei = 0;
    int hei = 0;
    int pos = 0;
    //int rot = 0;

    // weighted leg pose
    public void show()
    {
        GameObject woman = GameObject.Find("Spine");
        Animator anis = woman.GetComponent<Animator>();
        anis.SetInteger("face", dir);

        GameObject leg_l = GameObject.Find("LeftUpLeg");
        Animator ani = leg_l.GetComponent<Animator>();
        GameObject leg_r = GameObject.Find("RightUpLeg");
        Animator ani0 = leg_r.GetComponent<Animator>();
        if (wei == 0) // right leg weighted
        {
            ani0.SetInteger("state", 0);
            if (hei == 1) // bent
            {
                ani0.SetInteger("hi", 1);
                ani.SetInteger("hi", 1);
            }
            else if (hei == 3) // tiptoe
            {
                ani0.SetInteger("hi", 2);
                ani.SetInteger("hi", 0);
            }
            else // straight
            {
                ani0.SetInteger("hi", 0);
                ani.SetInteger("hi", 0);
            }
            
            ani.SetInteger("state", pos);


        }
        else
        {
            ani.SetInteger("state", 0);
            if (hei == 1)
            {
                ani0.SetInteger("hi", 1);
                ani.SetInteger("hi", 1);
            }
            else if (hei == 3)
            {
                ani0.SetInteger("hi", 0);
                ani.SetInteger("hi", 2);
            }
            else
            {
                ani0.SetInteger("hi", 0);
                ani.SetInteger("hi", 0);
            }
            
            ani0.SetInteger("state", pos);
            
        }
    }

    // changing free leg pose
    public void pose(int value)
    {
        switch (value)
        {
            case 0:
                pos = 2;
                break;
            case 1:
                pos = 3;
                break;
            case 2:
                pos = 1;
                break;
            case 3:
                pos = 4;
                break;
            case 4:
                pos = 5;
                break;
            case 5:
                pos = 6;
                break;
            case 6:
                pos = 7;
                break;
            case 7:
                pos = 8;
                break;
            case 8:
                pos = 9;
                break;
            case 9:
                pos = 10;
                break;
        }
        show();
    }

    // changing the height
    public void position(int value)
    {
        GameObject model = GameObject.Find("dance");
        Animator ani = model.GetComponent<Animator>();
        GameObject leg_l = GameObject.Find("LeftUpLeg");
        Animator ani1 = leg_l.GetComponent<Animator>();
        GameObject leg_r = GameObject.Find("RightUpLeg");
        Animator ani0 = leg_r.GetComponent<Animator>();
        switch (value)
        {
            case 0:
                //if (hei == 1)
                ani.SetInteger("state", 2);
                hei = 0;
                ani0.SetInteger("hi", 0);
                ani1.SetInteger("hi", 0);
                break;
            case 1:
                ani.SetInteger("state", 1);
                hei = 1;
                ani0.SetInteger("hi", 1);
                ani1.SetInteger("hi", 1);
                break;
            case 2:
                ani.SetInteger("state", 3);
                hei = 3;
                ani0.SetInteger("hi", 2);
                ani1.SetInteger("hi", 2);
                break;
        }
        show();
    }

    // changing the weighted leg
    public void weight(int value)
    {
        wei = value;
        show();
    }

    //public void rotate(int value)
    //{
    //    rot = value;
    //    //switch (value)
    //    //{
    //    //    case 0:
    //    //        rot = 0;
    //    //        break;
    //    //    case 1:
    //    //        rot = 1;
    //    //        break;

    //    //}
    //    show(); 
    //}

    // changing the face direction
    public void facedir(int value)
    {
        GameObject woman = GameObject.Find("Spine");
        Animator ani = woman.GetComponent<Animator>();
       
        switch (value)
        {
            case 0:
                //if (dir == 1)
                //    ani.SetInteger("face", 4);
                //else if (dir == 2)
                //    ani.SetInteger("face", 2);
                dir = 0;
                break;
            case 1:
                //if (dir == 0)
                //    ani.SetInteger("face", 3);
                dir = 1;
                break;
            case 2:
                //if (dir == 0)
                //    ani.SetInteger("face", 1);
                dir = 2;
                break;
        }
        show();
    }
}
