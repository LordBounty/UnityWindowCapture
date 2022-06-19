using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubeWindow;

public class Cancel : MonoBehaviour
{
    private GameObject[] tags;
    public void Delete ()
    {
        GameObject.Find("Main Camera").GetComponent<MouseRay>().vertices =  new List<Vector3>();
        GameObject.Find("Main Camera").GetComponent<MouseRay>().count = 0;
        tags = GameObject.FindGameObjectsWithTag("ClickTag");
        foreach (GameObject tag in tags)
        {
            Destroy(tag);
        }
    }
}
