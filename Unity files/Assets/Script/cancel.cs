using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cancel : MonoBehaviour
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
        selected.select = total.tot;
        foreach (GameObject ts in GameObject.FindGameObjectsWithTag("text0"))
        {
            ts.GetComponent<Text>().fontStyle = FontStyle.Normal;
        }
        Debug.Log(selected.select);
    }
}
