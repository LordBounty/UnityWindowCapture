using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVNC : MonoBehaviour
{
    // Start is called before the first frame update
    public void openVNC()
    {
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        Vector3 pos = new Vector3(0, 0, 0);
        GameObject instance = (GameObject)Instantiate(Resources.Load("Prefabs/VNCcomponent"), pos, rot);
        instance.GetComponent<CreatMesh>().creatMesh();
    }


}
