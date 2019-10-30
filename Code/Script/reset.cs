using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// happens when reset is clicked
public class reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        Button button = gameObject.GetComponent<Button>() as Button;
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        GameObject model = GameObject.Find("dance");
        GameObject leg_r = GameObject.Find("RightUpLeg");
        GameObject leg_l = GameObject.Find("LeftUpLeg");
        GameObject woman = GameObject.Find("Spine");
        model.transform.eulerAngles = new Vector3(0, 180, 0);
        model.transform.position = new Vector3(0, 0, (float)-6.99);
        Animator ani = leg_l.GetComponent<Animator>();
        Animator ani0 = leg_r.GetComponent<Animator>();
        Animator ani1 = woman.GetComponent<Animator>();
        Animator ani2 = model.GetComponent<Animator>();
        ani.SetInteger("state", 0);
        ani.SetInteger("hi", 0);
        ani0.SetInteger("state", 0);
        ani0.SetInteger("hi", 0);
        ani1.SetInteger("face", 0);
        ani2.SetInteger("state", 0);
        ind.i = 0;
    }
}
