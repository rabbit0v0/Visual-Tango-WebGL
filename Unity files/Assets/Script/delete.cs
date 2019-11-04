using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// happens when delete is clicked
public class delete : MonoBehaviour
{
    string[] Name = { "Collected", "Corssed forward", "Forward", "Backward", "In air forward", "In air backward", "Slide outside", "Wrapped around", "Collected high", "Crossed backward" };
    string[] Height = { "straight", "bent", "tiptoe" };
    string[] Leg = { "right", "left" };
    string[] Direction = { "north", "northwest", "northeast" };
    GameObject dialog;
    // Start is called before the first frame update
    // when start, do not show the dialog ("are you sure to delete xxx")
    void Start()
    {
        dialog = GameObject.Find("Dialog");
        dialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    // add listener
    void Awake()
    {
        Button button = gameObject.GetComponent<Button>() as Button;
        button.onClick.AddListener(makesure);
        Button yes = GameObject.Find("de").GetComponent<Button>();
        Button no = GameObject.Find("no").GetComponent<Button>();
        yes.onClick.AddListener(OnClick);
        no.onClick.AddListener(offClick);
    }
    // when "delete" is clicked, show the dialog
    void makesure()
    {
        dialog.SetActive(true);
        Text notice = GameObject.Find("sure").GetComponent<Text>();
        int index = selected.select;
        if (index == total.t)
        {
            index = index - 1;
        }
        notice.text = "Are you going to delete \"";
        notice.text += Name[streaming.l[index].p];
        notice.text += ", ";
        notice.text += Height[streaming.l[index].h];
        notice.text += ", ";
        notice.text += Leg[streaming.l[index].w];
        notice.text += ", ";
        notice.text += Direction[streaming.l[index].d];
        notice.text += ", ";
        notice.text += (streaming.l[index].t).ToString();
        notice.text += ", ";
        notice.text += (streaming.l[index].r).ToString();
        notice.text += "\"?";
    }

    // when click cancel, hide the dialog
    // there is also a "cancle.cs" which makes the text to be normal style
    void offClick()
    {
        dialog.SetActive(false);
    }

    // when click yes, delete the selected one
    void OnClick()
    {
        dialog.SetActive(false);
        //streaming.l.RemoveAt(streaming.l.Count - 1);
        int ind = selected.select;
        if (ind == total.t)
        {
            ind = ind - 1;
        }
        // reform the list
        List<Pose> tmplist = new List<Pose>();
        for (int i = 0; i < ind && i < total.tot; ++i)
        {
            tmplist.Add(streaming.l[i]);
        }
        for (int i = ind + 1; i < total.tot; ++i)
        {
            tmplist.Add(streaming.l[i]);
        }
        streaming.list = tmplist;
        total.t-=1;
        if (total.t < 0)
            total.t = 0;
        // select the empty space after the last pose
        selected.select = total.tot;
        GameObject canvas = GameObject.Find("Canvas");
        List<Pose> l = streaming.l;
        // rewrite the logs
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

            text.text = "";
            text.text += Name[l[i].p];
            text.text += ", ";
            text.text += Height[l[i].h];
            text.text += ", ";
            text.text += Leg[l[i].w];
            text.text += ", ";
            text.text += Direction[l[i].d];
            text.text += ", ";
            text.text += (l[i].t).ToString();
            text.text += ", ";
            text.text += (l[i].r).ToString();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 14;
            text.fontStyle = FontStyle.Normal;
            text.color = Color.black;
            //text.text += '\n';
            B.onClick.AddListener(delegate () {
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
