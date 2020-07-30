using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cubex.Utilities
{
    public class BGSound : MonoBehaviour
    {


        // Use this for initialization
        void Start()
        {
        }

        //Play Global
        private static BGSound instance = null;
        public static BGSound Instance
        {
            get { return instance; }
        }

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }

            DontDestroyOnLoad(this.gameObject);
        }
        //Play Gobal End

        // Update is called once per frame
        void Update()
        {

        }

        public void ToggleSound()
        {
            if (PlayerPrefs.GetInt("Muted", 0) == 0)
            {
                PlayerPrefs.SetInt("Muted", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Muted", 0);
            }
        }
    }
}