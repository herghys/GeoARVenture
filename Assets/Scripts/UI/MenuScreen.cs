using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Cubex.UI
{
    public class MenuScreen : MonoBehaviour
    {

        [SerializeField]
        protected Button MulaiButton = null;
        [SerializeField]
        protected Button KIKDButton = null;
        [SerializeField]
        protected Button EvaluasiButton = null;
        [SerializeField]
        protected Button KeluarButton = null;

        private void Start()
        {
            MulaiButton.onClick.AddListener(MulaiGame);
        }

        private void MulaiGame()
        {
            //Debug.Log("Mulai");
            int curMateriCube;
            curMateriCube = PlayerPrefs.GetInt("MaxKubus", 1);
            if (curMateriCube == 1)
            {
                SceneManager.LoadScene("Introduction");
            }
            else
            {
                SceneManager.LoadScene("Materi");
            }
        }
    }
}
