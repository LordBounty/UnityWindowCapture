using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

// 1. Please set the namespace in the menu Editor Extensions/Namespace Settings
// 2. After the namespace is changed, after the code is generated, the namespace of the logic code file (non-Designer) needs to be manually changed
namespace QFramework.Example
{
    public partial class SelectComponent : MonoSingleton<SelectComponent>
    {
        public ClickDisplayPanel DisplayPanel;

        public List<ClickSelectItem> clickSelectItems;
        
        private int clickCount = 0;
        private int startId = 0;
        private int nextId = 0;


        void Start()
        {
            ResKit.Init();
            // Code Here
            TypeEventSystem.Global.Register<EventClickSelectItem>(OnClickSelectItem);
        }

        private void OnClickSelectItem(EventClickSelectItem obj)
        {
            if (clickCount == 0)
            {
                startId = obj.id;
                nextId = obj.nextid;
                clickSelectItems.Find(va => va.id == obj.id).ChangeSelectStatus(true);
                clickCount++;
            }

            if (nextId == obj.clickItem.id)
            {
                clickSelectItems.Find(va => va.id == obj.id).ChangeSelectStatus(true);
                nextId = obj.nextid;
                clickCount++;
                if (nextId == startId)
                {
                    

                    Debug.Log("All Selected");
                    foreach (ClickSelectItem item in clickSelectItems)
                    {
                        item.ChangeSelectStatus(false);
                        DisplayPanel.SelectDisPlay();
                        RestartStatus();
                    }
                    clickCount = 0;
                }
            }
        }

        private void RestartStatus()
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

        // public void SetDisplay(Texture2D texture2D)
        // {

        // }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            TypeEventSystem.Global.UnRegister<EventClickSelectItem>(OnClickSelectItem);
        }
    }
}