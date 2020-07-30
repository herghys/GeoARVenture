using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Cubex.AR
{
    public class CubeTouch : MonoBehaviour
    {
        private int sceneIndex;

        [SerializeField]
        protected GameObject teksSelesai;

        [Header("Kubus Unsur")]
        [Space]
        [SerializeField]
        protected Text teksPenjelasan;
        [SerializeField]
        protected MeshRenderer sisiSamping;
        [SerializeField]
        protected MeshRenderer rusukKubus;
        [SerializeField]
        protected MeshRenderer titikSudut;
        [SerializeField]
        protected MeshRenderer diagonalSisi;
        [SerializeField]
        protected MeshRenderer diagonalRuang;
        [SerializeField]
        protected MeshRenderer bidangDiagonal;
        [SerializeField]
        protected Material materialDefault;
        [SerializeField]
        protected Material materialRed;
        [SerializeField]
        protected GameObject buttonFinish;
        [SerializeField]
        protected Text teksHitung;

        private int qSisi, qRusuk, qTitik, qDiagonalSisi, qDiagonalRuang, qBidang;
        private int qTotal;
        private int qHit;

        [Header("Kubus Jaring-Jaring")]
        [Space]
        [SerializeField]
        protected MeshRenderer kubusJaring;
        [SerializeField]
        protected Animator kubusJaringAnimator;
        [SerializeField]
        protected GameObject buttonFinish2;
        [SerializeField]
        protected Text teksHit;

        [Header("Kubus Luas")]
        [Space]
        [SerializeField]
        protected List<GameObject> kubusLuas;
        [SerializeField]
        protected List<GameObject> kubusButton;
        [SerializeField]
        protected GameObject notAnimatedObject;
        [SerializeField]
        protected GameObject animatedObject;
        [SerializeField]
        protected GameObject kubusPlayButton;
        [SerializeField]
        protected Animator kubusLuasAnimator;
        [SerializeField]
        protected GameObject rumusLuas;

        [Header("Kubus Volume")]
        [Space]
        [SerializeField]
        protected List<GameObject> kubusVolume;
        [SerializeField]
        protected GameObject kubusAddButton;
        [SerializeField]
        protected GameObject buttonVolumeFinish;
        [SerializeField]
        protected GameObject rumusVolume;

        private string currentBentuk;

        // Start is called before the first frame update
        void Start()
        {
            //sisiSamping.material = materialRed;
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            qTotal = 0;
            qHit = 0;

            currentBentuk = PlayerPrefs.GetString("Bentuk", "Kubus");
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(Input.touchCount);
            

            for (int i = 0; i < Input.touchCount; ++i)
            {
                KubusUnsur();
                KubusJaring();
                KubusLuas();
            }
        }

        void KubusUnsur()
        {
            //Menghitung materi yg sudah di klik
            qTotal = qSisi + qRusuk + qTitik + qDiagonalSisi + qDiagonalRuang + qBidang;
            teksHitung.text = "" + qTotal;

            //teksPenjelasan.text = "TOUCCH";
            Debug.Log("HELO");
            //Untuk reset Material
            ResetMaterial();

            // The pos of the touch on the screen
            Vector2 vTouchPos = Input.GetTouch(0).position;

            // The ray to the touched object in the world
            Ray ray = Camera.main.ScreenPointToRay(vTouchPos);

            // Your raycast handling
            RaycastHit vHit;
            
            if (Physics.Raycast(ray.origin, ray.direction, out vHit))
            {
                Debug.Log(vHit.transform.tag);
                teksPenjelasan.text = "" + vHit.transform.tag;
                if (vHit.transform.tag == "SisiKubus")
                {
                    sisiSamping.material = materialRed;

                    teksPenjelasan.text = "Ini adalah Sisi Kubus";
                    qSisi = 1;
                }
                else if (vHit.transform.tag == "RusukKubus")
                {
                    rusukKubus.material = materialRed;

                    teksPenjelasan.text = "Ini adalah Rusuk Kubus";
                    qRusuk = 1;
                }
                else if (vHit.transform.tag == "TitikSudut")
                {
                    titikSudut.material = materialRed;

                    teksPenjelasan.text = "Ini adalah Titik Sudut Kubus";
                    qTitik = 1;
                }
                else if (vHit.transform.tag == "DiagonalSisi")
                {
                    diagonalSisi.material = materialRed;

                    teksPenjelasan.text = "Ini adalah Diagonal Sisi Kubus";
                    qDiagonalSisi = 1;
                }
                else if (vHit.transform.tag == "DiagonalRuang")
                {
                    diagonalRuang.material = materialRed;

                    teksPenjelasan.text = "Ini adalah Diagonal Ruang Kubus";
                    qDiagonalRuang = 1;
                }
                else if (vHit.transform.tag == "BidangDiagonal")
                {
                    bidangDiagonal.material = materialRed;

                    teksPenjelasan.text = "Ini adalah Bidang Diagonal Kubus";
                    qBidang = 1;
                }
            }

            //TOMBOL BACK
            if (Input.GetKeyDown(KeyCode.Escape))
                BackToPreviousScene();


            //Memunculkan tombol Finish
            if (qTotal == 6)
            {
                buttonFinish.SetActive(true);
                teksSelesai.SetActive(true);
                PlayerPrefs.SetInt(currentBentuk, 2);
            }
        }

        void KubusJaring()
        {

            // The pos of the touch on the screen
            Vector2 vTouchPos = Input.GetTouch(0).position;

            // The ray to the touched object in the world
            Ray ray = Camera.main.ScreenPointToRay(vTouchPos);

            // Your raycast handling
            RaycastHit vHit;
            if (Physics.Raycast(ray.origin, ray.direction, out vHit))
            {
                if (vHit.transform.tag == "JaringKubus")
                {
                    if (kubusJaringAnimator.GetBool("Clicked"))
                    {
                        kubusJaringAnimator.SetBool("Clicked", false);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(currentBentuk, 3);
                        kubusJaringAnimator.SetBool("Clicked", true);
                        buttonFinish2.SetActive(true);
                        teksSelesai.SetActive(true);
                    }
                }
            }

        }

        void KubusLuas()
        {
            // The pos of the touch on the screen
            Vector2 vTouchPos = Input.GetTouch(0).position;

            // The ray to the touched object in the world
            Ray ray = Camera.main.ScreenPointToRay(vTouchPos);

            // Your raycast handling
            RaycastHit vHit;

            /*
            if (Physics.Raycast(ray.origin, ray.direction, out vHit))
            {
                if (vHit.transform.tag == "LuasKubus")
                {
                    kubusLuas[qHit].SetActive(true);

                    qHit++;

                    if (qHit > 6)
                    {
                        kubusKerangka.SetActive(false);

                        kubusLuasAnimator.SetBool("Completed", true);

                        qHit = 0;
                    }
                }
            }*/
            
        }

        public void KubusVolume()
        {

            kubusVolume[qHit].SetActive(true);

            qHit++;

            if (qHit == 8)
            {
                qHit = 0;

                buttonVolumeFinish.SetActive(true);
                kubusAddButton.SetActive(false);
                rumusVolume.SetActive(true);
                PlayerPrefs.SetInt(currentBentuk, 5);
                teksSelesai.SetActive(true);
            }
        }

        public void IsiKubus(int i)
        {
            qHit++;

            kubusLuas[i].SetActive(true);

            kubusButton[i].SetActive(false);

            if (qHit == 6)
            {
                kubusPlayButton.SetActive(true);
                notAnimatedObject.SetActive(false);
                animatedObject.SetActive(true);
                rumusLuas.SetActive(true);
                teksSelesai.SetActive(true);
                PlayerPrefs.SetInt(currentBentuk, 4);
            }
        }

        void ResetMaterial()
        {
            sisiSamping.material = materialDefault;
            rusukKubus.material = materialDefault;
            titikSudut.material = materialDefault;
            diagonalSisi.material = materialDefault;
            diagonalRuang.material = materialDefault;
            bidangDiagonal.material = materialDefault;
            teksPenjelasan.text = "Tekan bagian-bagian " + currentBentuk + "s untuk mengetahui unsur-unsurnya!";
        }

        public void BackToPreviousScene()
        {
            SceneManager.LoadScene("Main");
        }
    }
}