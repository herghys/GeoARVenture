using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Cubex.UI
{
    public class MisiScreen : MonoBehaviour
    {
        [SerializeField]
        protected GameObject buttonEnabled;
        [SerializeField]
        protected GameObject buttonDisabled;

        [SerializeField]
        protected GameObject teksPetunjuk;

        [SerializeField]
        protected UIManager uiManager;
        [SerializeField]
        protected UIWindow windowsSelesai;
        [SerializeField]
        protected Animator animatorSelesai;

        [SerializeField]
        protected GameObject boxPendekar;
        [SerializeField]
        protected GameObject boxKakek;
        [SerializeField]
        protected List<string> dialogPendekar;
        [SerializeField]
        protected List<string> dialogKakek;
        [SerializeField]
        protected Text teksPendekar;
        [SerializeField]
        protected Text teksKakek;

        public float delay = 3;
        public float ActiveTime = 0f;

        private int i = 0;
        private string bicara = "";

        private bool finished = false;

        // Start is called before the first frame update
        void Start()
        {
            bicara = "Pendekar";
            teksPetunjuk.SetActive(true);
            
        }

        // Update is called once per frame
        void Update()
        {
            ActiveTime += Time.deltaTime;
            var percent = ActiveTime / delay;

            if (Mathf.Clamp01(percent) == 1f)
            {
                delay = 3;
                if (i < 3)
                {
                    if (bicara == "Pendekar")
                    {
                        DisableAll();
                        boxPendekar.SetActive(true);
                        teksPendekar.text = dialogPendekar[i];

                        bicara = "Kakek";
                    }
                    else if (bicara == "Kakek")
                    {
                        DisableAll();
                        boxKakek.SetActive(true);
                        teksKakek.text = dialogKakek[i];

                        i++;
                        bicara = "Pendekar";
                    }
                    ActiveTime = 0f;
                }
                else
                {
                    finished = true;
                    if (finished && i == 3)
                    {
                        uiManager.OpenWindow(windowsSelesai);
                        animatorSelesai.SetBool("Open", true);
                        i++;
                    }
                    //buttonDisabled.SetActive(false);
                    //buttonEnabled.SetActive(true);
                    //Open Pop Up
                    //uiManager.OpenWindow(windowsSelesai);
                    //animatorSelesai.SetBool("Open", true);
                }

                
            }

            /*if (finished)
            {
                if (Mathf.Clamp01(percent) == 1f)
                {
                    uiManager.OpenWindow(windowsSelesai);
                    animatorSelesai.SetBool("Open", true);
                }
            }*/
        }

        void DisableAll()
        {
            teksPetunjuk.SetActive(false);
            boxPendekar.SetActive(false);
            boxKakek.SetActive(false);
        }

        public void GoToNextMateri()
        {
            SceneManager.LoadScene("Materi");
        }
    }
}