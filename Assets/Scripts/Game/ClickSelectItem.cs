using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class ClickSelectItem : MonoBehaviour
{
    public int id;
    public int Nextid;

    public Color scelectColor=Color.red;
    public Color unScelectColor=Color.cyan;

    private MeshRenderer meshRenderer;
    //public bool allowClick; 
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ChangeSelectStatus(false);
    }

    public void ChangeSelectStatus(bool select)
    {
        if (select)
        {
            meshRenderer.material.color = scelectColor;
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.material.color = unScelectColor;
            meshRenderer.enabled = false;
        }
    }
    private void OnMouseDown()
    {
        TypeEventSystem.Global.Send<EventClickSelectItem>( new EventClickSelectItem(){clickItem = this,id = id,nextid = this.Nextid});
    }
}
