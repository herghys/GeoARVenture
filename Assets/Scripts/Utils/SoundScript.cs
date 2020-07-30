using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cubex.Utilities
{
    public class SoundScript : MonoBehaviour
    {
        [SerializeField]
        protected Button buttonSound;
        [SerializeField]
        protected Sprite musicOnButton;
        [SerializeField]
        protected Sprite musicOffButton;
        [SerializeField]
        protected AudioSource myBGM;

        private BGSound myMusic;

        // Start is called before the first frame update
        void Start()
        {
            myMusic = GameObject.FindObjectOfType<BGSound>();
            myBGM = GameObject.FindObjectOfType<AudioSource>();
            UpdateIcon();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PauseMusic()
        {
            myMusic.ToggleSound();
            UpdateIcon();
        }

        void UpdateIcon()
        {
            if (myMusic != null)
            {
                if (PlayerPrefs.GetInt("Muted", 0) == 0)
                {
                    myBGM.volume = 1;
                    buttonSound.GetComponent<Image>().sprite = musicOnButton;
                }
                else
                {
                    myBGM.volume = 0;
                    buttonSound.GetComponent<Image>().sprite = musicOffButton;
                }
            }
        }
    }
}