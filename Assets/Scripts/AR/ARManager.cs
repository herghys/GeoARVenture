using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace Cubex.AR
{
    public class ARManager : DefaultTrackableEventHandler
    {
        [SerializeField]
        protected GameObject teksPetunjuk;

        // Start is called before the first frame update
        void Start()
        {

        }

        
        protected virtual void OnTrackableStatusChanged(TrackableBehaviour.StatusChangeResult statusChangeResult)
        {
            m_PreviousStatus = statusChangeResult.PreviousStatus;
            m_NewStatus = statusChangeResult.NewStatus;

            Debug.Log("HAHAHAHAH");
            if (m_NewStatus == TrackableBehaviour.Status.DETECTED ||
                m_NewStatus == TrackableBehaviour.Status.TRACKED ||
                m_NewStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                teksPetunjuk.SetActive(false);
            }
            else
            {
                teksPetunjuk.SetActive(true);
            }
        }
      
        // Update is called once per frame
        void Update()
        {

        }
    }
}