using System;
using System.Collections.Generic;
using UnityEngine;

namespace VRStandardAssets.Utils
{
    // This class should be added to any gameobject in the scene
    // that should react to input based on the user's gaze.
    // It contains events that can be subscribed to by classes that
    // need to know about input specifics to this gameobject.
    public class VRInteractiveItem : MonoBehaviour
    {
        public event Action OnOver;             // Called when the gaze moves over this object
        public event Action OnOut;              // Called when the gaze leaves this object
        public event Action OnClick;            // Called when click input is detected whilst the gaze is over this object.
        public event Action OnDoubleClick;      // Called when double click input is detected whilst the gaze is over this object.
        public event Action OnUp;               // Called when Fire1 is released whilst the gaze is over this object.
        public event Action OnDown;             // Called when Fire1 is pressed whilst the gaze is over this object.


        protected bool m_IsOver;

        private List<PlayMakerFSM> m_FSMList;

        void Start()
        {
            m_FSMList = new List<PlayMakerFSM>();

            PlayMakerFSM[] fsms = gameObject.GetComponents<PlayMakerFSM>();
            Debug.Log(fsms.Length);

            if (fsms.Length > 0)
            {
                for (int i = 0; i < fsms.Length; i++)
                {
                    m_FSMList.Add(fsms[i]);
                }
            }

        }

        public void SendEventToFSM(string fsmEvent)
        {
            if (null != m_FSMList)
            {
                for (int i = 0; i < m_FSMList.Count; i++)
                {
                    PlayMakerFSM fsm = m_FSMList[i];
                    Debug.Log(m_FSMList.Count);
                    fsm.SendEvent(fsmEvent);
                }
            }
        }

        public bool IsOver
        {
            get { return m_IsOver; }              // Is the gaze currently over this object?
        }


        // The below functions are called by the VREyeRaycaster when the appropriate input is detected.
        // They in turn call the appropriate events should they have subscribers.
        public void Over()
        {
            m_IsOver = true;
            SendEventToFSM("ON_OVER");


            if (OnOver != null)
            {
                OnOver();
            }
        }


        public void Out()
        {
            m_IsOver = false;
            SendEventToFSM("OUT");


            if (OnOut != null)
                OnOut();
        }


        public void Click()
        {
            if (OnClick != null)
                OnClick();
        }


        public void DoubleClick()
        {
            if (OnDoubleClick != null)
                OnDoubleClick();
        }


        public void Up()
        {
            if (OnUp != null)
                OnUp();
        }


        public void Down()
        {
            if (OnDown != null)
                OnDown();
        }
    }
}