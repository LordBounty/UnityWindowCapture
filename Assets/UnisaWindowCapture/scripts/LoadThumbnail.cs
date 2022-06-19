using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubeWindow;
using Events;
using QFramework;
using QFramework.Example;

public class LoadThumbnail : MonoBehaviour
{
    public Material mat;

    private void Awake()
    {
        //Open the Thumbnails panel
        ResKit.Init();
        UIKit.Root.CanvasScaler.LogInfo();
       
    }

    // Start is called before the first frame update
    public void OpenThumbnail()
    {
        List<Vector3> v = FindObjectOfType<MouseRay>().vertices;
        if (v.Count == 4)
        {
            //Reduce the vector square by one thousandth
            GameObject cube = GameObject.Find("MainCube");
            cube.transform.localScale = new Vector3(0.999f, 0.999f, 0.999f);
            Debug.Log(v.Count);
            CreateMesh(v[0],v[1],v[2],v[3],mat);
        }
        else
        {
            Debug.Log(v.Count);

        }
    }

    public void CreateMesh(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4,Material mat)
    {
      
       // Vector2[] uvs = new Vector2[]{
       //     new Vector2(0, 1),
       //     new Vector2(0, 0),
       //     new Vector2(1, 0),
       //     new Vector2(1, 1),
       // };
       Vector2[] uvs = new Vector2[]{
           new Vector2(0, 0),
           new Vector2(1, 0),
           new Vector2(1, 1),
           new Vector2(0, 1),
          
       };
        GameObject newMesh = CreatenewMeshHelper.CreateMeshBy4Point(p1,p2,p3,p4,mat,uvs);
        newMesh.AddComponent<CreateNewMeshCtrl>();
        WindowListPop();
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
}


