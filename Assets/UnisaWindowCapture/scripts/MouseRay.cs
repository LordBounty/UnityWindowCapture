using System;
using System.Collections.Generic;
using Events;
using QFramework;
using UnityEngine;

namespace CubeWindow
{
    public class MouseRay : MonoBehaviour
    {
        private Ray ray;
        public GameObject canvas;
        private RaycastHit hit;
        public GameObject clickTag;
        Vector3 target;
        GameObject _curGameObject;
        public List<Vector3> vertices = new List<Vector3>();
        public int count = 0;

        private void Start()
        {
            TypeEventSystem.Global.Register<EventRestartMeshCreate>(OnRestartMeshCreateHandler);
        }

        private void OnRestartMeshCreateHandler(EventRestartMeshCreate obj)
        {
            vertices.Clear();
            count = 0;
            canvas.gameObject.SetActive(true);
            GameObject.Find("MainCube").transform.localScale=Vector3.one;
            Destroy(FindObjectOfType<CreateNewMeshCtrl>().gameObject);
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("ClickTag"))
            {
                Destroy(o.gameObject);
            }
        }

        void Update()
        {
            //When the left mouse button is pressed       
            if (Input.GetMouseButtonDown(0))
            {

                //The position of the mouse on the screen
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit) && count <= 3)
                {
                    //Draw a red ray from the camera
                    //(The results of this section will not be displayed during the run and are only used for checking during early development work)
                    Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
                    target = hit.point;//Get the coordinates of the mouse

                    //Get the information of the object clicked by the mouse
                    //(This part only used for checking during early development work)
                    _curGameObject = hit.transform.gameObject;

                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    GameObject tag = GameObject.Instantiate(clickTag, target, rot) as GameObject;

                    vertices.Add(tag.transform.position);
                    count++;

                    Debug.Log("Get the world coordinate position of the mouse:" + target);
                    Debug.Log("vertex list:" + vertices.Count);
                }
                else
                {
                    Debug.Log("Object not touching / Vertex threshold exceeded");
                }

            }

        }

        private void OnDestroy()
        {
            TypeEventSystem.Global.UnRegister<EventRestartMeshCreate>(OnRestartMeshCreateHandler);

        }
    }
}