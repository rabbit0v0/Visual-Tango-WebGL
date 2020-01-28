using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;
using SFB;
using System.Runtime.InteropServices;


public class load_file : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void LoadFileToLocalStorage();

    [DllImport("__Internal")]
    private static extern int CheckLoadDone();

    [DllImport("__Internal")]
    private static extern string GetLoadedString();

    [DllImport("__Internal")]
    private static extern void RefreshStorage();
    
    string[] Name = { "Collected", "Corssed forward", "Forward", "Backward", "In air forward", "In air backward", "Slide outside", "Wrapped around", "Collected high", "Crossed backward" };
    string[] Height = { "straight", "bent", "tiptoe" };
    string[] Leg = { "right", "left" };
    string[] Direction = { "north", "northwest", "northeast" };
    int[] angle = { 0, 30, 60, 90, 120, 150, 180, 270, 360 };

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
        button.onClick.AddListener(Load);
    }

    void Load()
    {
        clear();

        LoadFileToLocalStorage();

        StartCoroutine(ConditionRun());
        
    }

    void clear()
    {
        RefreshStorage();
        streaming.l = new List<Pose>();
        total.t = 0;
        selected.s = 0;
    }

    IEnumerator ConditionRun()
    {
        Debug.Log("Start waiting condition...");
        yield return new WaitUntil(() => CheckLoadDone() != -1);
        Debug.Log("Ready to go.");

        var s = GetLoadedString();

        Debug.Log(s);

        string[] s_split = s.Split('\n');
        
        for (int i = 0; i < s_split.Length; i += 6)
        {
            Pose p = new Pose();
            p.d = Int32.Parse(s_split[i]);
            p.h = Int32.Parse(s_split[i + 1]);
            p.p = Int32.Parse(s_split[i + 2]);
            p.r = Int32.Parse(s_split[i + 3]);
            p.t = Int32.Parse(s_split[i + 4]);
            p.w = Int32.Parse(s_split[i + 5]);
            Debug.Log(p.p);
            streaming.l.Add(p);
            total.tot += 1;
        }
        GameObject canvas = GameObject.Find("Canvas");
        foreach (GameObject bs in GameObject.FindGameObjectsWithTag("log"))
        {
            Destroy(bs);
        }
        for (int i = 0; i < total.t; ++i)
        {
            int index = i;
            GameObject button = new GameObject("Button", typeof(Button), typeof(RectTransform), typeof(Text));
            button.transform.SetParent(canvas.transform);
            Button B = button.GetComponent<Button>();
            B.tag = "log";
            B.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 20);
            B.GetComponent<RectTransform>().anchoredPosition = new Vector2(500, 300 - 50 * i);
            //B.GetComponent<RectTransform>().pivot = new Vector2(0, 0);


            GameObject temp = new GameObject("Text", typeof(RectTransform), typeof(Text));
            temp.transform.SetParent(button.transform);
            Text text = temp.GetComponent<Text>();
            text.tag = "text0";
            text.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 20);
            //text.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
            //text.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            //text.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);

            text.text = "";
            text.text += Name[streaming.l[i].p];
            text.text += ", ";
            text.text += Height[streaming.l[i].h];
            text.text += ", ";
            text.text += Leg[streaming.l[i].w];
            text.text += ", ";
            text.text += Direction[streaming.l[i].d];
            text.text += ", ";
            text.text += (streaming.l[i].t).ToString();
            text.text += ", ";
            text.text += (streaming.l[i].r).ToString();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 14;
            text.fontStyle = FontStyle.Normal;
            text.color = Color.black;
            //text.text += '\n';
            B.onClick.AddListener(delegate () {// if the log is clicked, the text would be bold and select this log
                selected.select = index;
                foreach (GameObject ts in GameObject.FindGameObjectsWithTag("text0"))
                {
                    ts.GetComponent<Text>().fontStyle = FontStyle.Normal;
                }
                text.fontStyle = FontStyle.BoldAndItalic;
                //Debug.Log(selected.select);
            });
        }
    }
}
