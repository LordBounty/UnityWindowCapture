using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using QFramework;
using QFramework.Example;
using UnityEngine;

public class RayComponent : MonoSingleton<RayComponent>
{
    public GameObject sphere;
    public Transform sphereParent;

    public int ClickCount = 0;
    public Material mat;
    public GameObject newMesh;

    public List<GameObject> clickSpherelist = new List<GameObject>();

    private void Awake()
    {
        ResKit.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "DisplayPanel"&& newMesh==null&& ClickCount<4)
                {
                    GameObject obj = GameObject.Instantiate(sphere, sphereParent);
                    Vector3 office =sphereParent.TransformPoint(sphereParent.localPosition - new Vector3(0, 0, 0.03f)) ;
                    obj.transform.position = hit.point+office;
                    clickSpherelist.Add(obj);
                    ClickCount++;
                    if (ClickCount==4)
                    {
                        Debug.Log("four tags");
                        newMesh = CreatenewMeshHelper.CreateMeshBy4Point(clickSpherelist[0].transform.position,
                            clickSpherelist[1].transform.position,
                            clickSpherelist[2].transform.position,
                            clickSpherelist[3].transform.position,mat);
                        newMesh.AddComponent<CreateNewMeshCtrl>();
                        WindowListPop();
                       
                    }
                    
                }
               
            }
        }
    }
    private void WindowListPop()
    {
        if (UIKit.GetPanel<UIPopWindowsListPanel>())
        {
            UIKit.GetPanel<UIPopWindowsListPanel>().Scale(Vector3.one);
        }
        else
        {
            UIKit.OpenPanel<UIPopWindowsListPanel>();
        }
            
    }

    public void ReStartClick()
    {
        ClickCount = 0;
        if (newMesh)
        {
            Destroy(newMesh.gameObject);
            foreach (GameObject o in clickSpherelist)
            {
                Destroy(o);
            }
            clickSpherelist.Clear();
            newMesh = null;
        }
    }
   

    private void OnDestroy()
    {

    }
}