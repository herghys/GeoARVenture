using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Cubex.UI
{
    public class StartScreen : UIScreen
    {
        [SerializeField]
        protected Button MulaiButton = null;
        [SerializeField]
        protected Button KIKDButton = null;
        [SerializeField]
        protected Button EvaluasiButton = null;
        [SerializeField]
        protected Button KeluarButton = null;

        [SerializeField]
        protected Button PetunjukButton = null;
        [SerializeField]
        protected Button PengaturanButton = null;

        private void Start()
        {
            MulaiButton.onClick.AddListener(MulaiGame);
            KeluarButton.onClick.AddListener(KeluarGame);
            EvaluasiButton.onClick.AddListener(EvaluasiGame);
        }

        private void MulaiGame()
        {
            //Debug.Log("Mulai");
            SceneManager.LoadScene("Introduction");
        }

        private void KeluarGame()
        {
            Application.Quit();
        }

        private void EvaluasiGame()
        {
            SceneManager.LoadScene("Evaluasi");
        }
    }
}
