using System.Collections;
using System;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;
using SFB;
using System.Runtime.InteropServices;

public class save_file : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveTextAsFile(string filename, string text);

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
        button.onClick.AddListener(Save);
    }

    public void Save()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < streaming.l.Count; i++)
        {
            sb.Append(streaming.l[i].d);
            sb.Append("\n");
            sb.Append(streaming.l[i].h);
            sb.Append("\n");
            sb.Append(streaming.l[i].p);
            sb.Append("\n");
            sb.Append(streaming.l[i].r);
            sb.Append("\n");
            sb.Append(streaming.l[i].t);
            sb.Append("\n");
            sb.Append(streaming.l[i].w);
            if (i != streaming.l.Count - 1)
            {
                sb.Append("\n");
            }
        }

        SaveTextAsFile("save.txt", sb.ToString());

    }
    //+ DateTime.Now.GetDateTimeFormats('s')[0]
}
