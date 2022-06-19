using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uWindowCapture;

public class PanelCapture : MonoBehaviour
{
    public UwcWindowList windowList;

    public InputField inputFindWin;
    public Button btnFind;
    // Start is called before the first frame update
    void Start()
    {
        btnFind.onClick.AddListener(() =>
        {
            windowList.FindWindow(inputFindWin.text);
        });
    }

   
}
