using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GetInputText
{
    public class GetInputTextClass : MonoBehaviour
    {
        public string inputPort;
        public string inputHost;
        public string inputPassword;
        public int inputPort_int;

        string inputPassword_text;
        // Start is called before the first frame update
  
        public void confirmEvent()
        {
            //Get input information
            inputPort = GameObject.Find("InputPort").GetComponent<InputField>().text.ToString();
            inputHost = GameObject.Find("InputHost").GetComponent<InputField>().text.ToString();
            inputPassword = GameObject.Find("InputPassword").GetComponent<InputField>().text.ToString();

            //Convert the Port parameter of type text to int to adapt to the VNCScreen class.
            //If user enter a non-number, an error will pop up.    
            int.TryParse(inputPort, out inputPort_int);

            Debug.Log(inputPassword);
            Debug.Log(inputPort_int);
            Debug.Log(inputHost);
        }
    }
}