using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cubex.UI;

namespace Cubex.AR
{
    public class ShapeTouch : MonoBehaviour
    {
        private int sceneIndex;
        private string currentBentuk;
        private string currentLevel;

        [SerializeField]
        protected Text teksPetunjuk;
        [SerializeField]
        protected Text teksSelesai;
        [SerializeField]
        protected UIManager uiManager;
        [SerializeField]
        protected UIWindow windowsSelesai;
        [SerializeField]
        protected Animator animatorSelesai;

        [Header("Marker")]
        [Space]
        [SerializeField]
        protected GameObject markerUnsur;
        [SerializeField]
        protected GameObject markerJaring;
        [SerializeField]
        protected GameObject markerLuas;
        [SerializeField]
        protected GameObject markerVolume;

        [Header("Unsur-Unsur")]
        [Space]
        [SerializeField]
        protected Text teksPenjelasan;
        [SerializeField]
        protected List<string> unsurTag;
        [SerializeField]
        protected List<MeshRenderer> unsurObject;
        [SerializeField]
        protected List<string> unsurPenjelasan;
        [SerializeField]
        protected Material materialDefault;
        [SerializeField]
        protected Material materialRed;
        [SerializeField]
        protected Text teksHitung;

        [Header("Jaring-Jaring")]
        [Space]
        [SerializeField]
        protected MeshRenderer jaringObject;
        [SerializeField]
        protected string jaringTag;
        [SerializeField]
        protected Animator jaringAnimator;

        [Header("Luas")]
        [Space]
        [SerializeField]
        protected List<GameObject> luasObject;
        [SerializeField]
        protected List<GameObject> luasButton;
        [SerializeField]
        protected GameObject notAnimatedObject;
        [SerializeField]
        protected GameObject animatedObject;
        [SerializeField]
        protected GameObject luasPlayButton;
        [SerializeField]
        protected Animator luasAnimator;
        [SerializeField]
        protected GameObject rumusLuas;

        [Header("Volume")]
        [Space]
        [SerializeField]
        protected string volumeTag;
        [SerializeField]
        protected Animator volumeAnimator;
        [SerializeField]
        protected string animationTag;
        [SerializeField]
        protected GameObject buttonPlus;
        [SerializeField]
        protected int jumlahIsi = 0;
        [SerializeField]
        protected List<GameObject> volumeIsi;
        [SerializeField]
        protected GameObject rumusVolume;

        public float delay = 3;
        public float ActiveTime = 0f;

        private List<int> qHit;
        private int qTotal;
        private int volumeIndex;
        
        private int luasHit;
        private int levelnya;
        private string materinya;
        private int maxLevel;

        private bool finished = false;

        // Start is called before the first frame update
        void Start()
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            currentBentuk = PlayerPrefs.GetString("Bentuk", "Kubus");

            levelnya = PlayerPrefs.GetInt(currentBentuk, 1);

            switch (PlayerPrefs.GetInt(currentBentuk, 1))
            {
                case 1:
                    currentLevel = "Unsur-Unsur";
                    //markerUnsur.SetActive(true);
                    break;
                case 2:
                    currentLevel = "Jaring-jaring";
                    //markerJaring.SetActive(true);
                    break;
                case 3:
                    currentLevel = "Luas Permukaan";
                    //markerLuas.SetActive(true);
                    break;
                case 4:
                    currentLevel = "Volume";
                    //markerVolume.SetActive(true);
                    break;
                default:
                    currentLevel = "Marker";
                    break;
            }
            materinya = currentLevel + " " + currentBentuk;
            teksPetunjuk.text = "Carilah Marker " + currentLevel + " " + currentBentuk + " kemudian scan.";

            volumeIndex = 0;
            finished = false;
            ActiveTime = 0f;

            qTotal = 0;
            qHit = new List<int>(unsurTag.Count);
            for (int i = 0; i < unsurTag.Count; i++)
            qHit.Add(0);
            teksHitung.text = qTotal + " / " + unsurTag.Count;
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                switch (PlayerPrefs.GetInt(currentBentuk, 1))
                {
                    case 1:
                        UnsurUnsur();
                        break;
                    case 2:
                        JaringJaring();
                        break;
                    case 3:

                        break;
                    case 4:
                        VolumeAnimation();
                        break;
                    default:
                        break;
                }
                
            }

            if (finished)
            {
                ActiveTime += Time.deltaTime;
                var percent = ActiveTime / delay;

                if (Mathf.Clamp01(percent) == 1f)
                {
                    uiManager.OpenWindow(windowsSelesai);
                    animatorSelesai.SetBool("Opened", true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
                BackToMenu();
        }

        void UnsurUnsur()
        {
            ResetMaterial();

            string hitObject = "";

            hitObject = TouchManager();

            //Mengganti warna material menjadi merah saat di klik
            for(int i = 0; i < unsurTag.Count; i++)
            {
                if(hitObject == unsurTag[i])
                {
                    unsurObject[i].material = materialRed;
                    qHit[i] = 1;
                    teksPenjelasan.text = unsurPenjelasan[i];
                }
            }

            //Menghitung total materi yang sudah dipilih
            if (qTotal < unsurTag.Count)
            {
                qTotal = 0;
                for (int i = 0; i < unsurTag.Count; i++)
                {
                    qTotal += qHit[i];
                }
                Debug.Log("HIT TOTAL = " + qTotal);
            }
            else
            {
                teksSelesai.text = "Selamat. Kamu berhasil menentukan " + materinya + ". Lanjut yuk ke materi berikutnya dengan klik tombol di bawah";
                //PlayerPrefs.SetInt(currentBentuk, 2);
                //PlayerPrefs.Save();
                //uiManager.OpenWindow(windowsSelesai);
                finished = true;
                //animatorSelesai.SetBool("Opened", true);

            }

            teksHitung.text = qTotal + " / " + unsurTag.Count;
        }

        void JaringJaring()
        {
            string hitObject = "";

            hitObject = TouchManager();

            if (hitObject == jaringTag)
            {
                if (jaringAnimator.GetBool("Clicked"))
                {
                    jaringAnimator.SetBool("Clicked", false);
                }
                else
                {
                    teksSelesai.text = "Selamat. Kamu berhasil menentukan " + materinya + ". Lanjut yuk ke materi berikutnya dengan klik tombol di bawah";
                    //PlayerPrefs.SetInt(currentBentuk, 3);
                    //PlayerPrefs.Save();
                    finished = true;

                    jaringAnimator.SetBool("Clicked", true);
                    Jeda();
                    //uiManager.OpenWindow(windowsSelesai);
                    //animatorSelesai.SetBool("Opened", true);
                }
            }
        }

        void Luas()
        {

        }

        public void IsiBentuk(int i)
        {
            luasHit++;

            luasObject[i].SetActive(true);

            luasButton[i].SetActive(false);

            if(luasHit == luasObject.Count)
            {
                //PlayerPrefs.SetInt(currentBentuk, 4);
                //PlayerPrefs.Save();
                teksSelesai.text = "Selamat. Kamu berhasil menentukan " + materinya + ". Lanjut yuk ke materi berikutnya dengan klik tombol di bawah";
                //uiManager.OpenWindow(windowsSelesai);
                //animatorSelesai.SetBool("Opened", true);
                luasPlayButton.SetActive(true);
                animatedObject.SetActive(true);
                rumusLuas.SetActive(true);
                notAnimatedObject.SetActive(false);
                finished = true;
            }
        }

        public void BukaSelesai()
        {
            Jeda();
            uiManager.OpenWindow(windowsSelesai);
        }

        IEnumerator Jeda()
        {
            Debug.Log("Jeda 3 Detik");
            yield return new WaitForSeconds(3);
        }

        public void SetLevel(int i)
        {
            PlayerPrefs.SetInt(currentBentuk, i);
            PlayerPrefs.Save();
        }

        public void IsiVolume()
        {
            if (volumeIndex < jumlahIsi)
            {
                volumeIsi[volumeIndex].SetActive(true);
                volumeIndex++;

                if (volumeIndex == jumlahIsi)
                {
                    //PlayerPrefs.SetInt(currentBentuk, 5);
                    //PlayerPrefs.Save();
                    teksSelesai.text = "Selamat. Kamu berhasil menentukan " + materinya + ". Lanjut yuk ke materi berikutnya dengan klik tombol di bawah";
                    //uiManager.OpenWindow(windowsSelesai);
                    //animatorSelesai.SetBool("Opened", true);
                    buttonPlus.SetActive(false);
                    rumusVolume.SetActive(true);
                    finished = true;
                }
            }
        }

        void VolumeAnimation()
        {
            string hitObject = "";

            hitObject = TouchManager();
            if (volumeTag == null || volumeTag.Equals(null) || volumeTag == "") { }
            else
            {
                if (hitObject == volumeTag)
                {
                    //PlayerPrefs.SetInt(currentBentuk, 5);
                    //PlayerPrefs.Save();
                    teksSelesai.text = "Selamat. Kamu berhasil menentukan " + materinya + ". Lanjut yuk ke materi berikutnya dengan klik tombol di bawah";
                    //uiManager.OpenWindow(windowsSelesai);
                    //animatorSelesai.SetBool("Opened", true);
                    volumeAnimator.Play(animationTag);
                    rumusVolume.SetActive(true);
                    finished = true;
                }
            }
        }

        //Reset warna menjadi default
        void ResetMaterial()
        {
            for (int i = 0; i < unsurObject.Count; i++)
            {
                unsurObject[i].material = materialDefault;
            }
            teksPenjelasan.text = "Tekan bagian-bagian " + currentBentuk + " untuk mengetahui unsur-unsurnya!";
        }

        public void SetMaxLevel(int level, string bentuk)
        {
            maxLevel = PlayerPrefs.GetInt("Max" + bentuk);
            if (level > maxLevel)
            {
                PlayerPrefs.SetInt("Max" + bentuk, level);
            }
        }

        public void FinishedLevel()
        {
            finished = true;
        }

        public void RestartScene()
        {
            //var level = PlayerPrefs.GetInt(currentBentuk, 1) - 1;
            //PlayerPrefs.SetInt(currentBentuk, level);
            //PlayerPrefs.Save();

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Sensor untuk mengetahui objek apa yang sedang dipilih
        private string TouchManager()
        {
            // The pos of the touch on the screen
            Vector2 vTouchPos = Input.GetTouch(0).position;

            // The ray to the touched object in the world
            Ray ray = Camera.main.ScreenPointToRay(vTouchPos);

            // Your raycast handling
            RaycastHit vHit;

            string hitObject = "";
            if (Physics.Raycast(ray.origin, ray.direction, out vHit))
            {
                hitObject = vHit.transform.tag;
            }

            return hitObject;
        }

        public void BackToPreviousScene()
        {
            SceneManager.LoadScene("Main");
        }

        public void FinishLevel()
        {
            //var level = PlayerPrefs.GetInt(currentBentuk, 1) + 1;
            var level = levelnya + 1;
            PlayerPrefs.SetInt(currentBentuk, level);
            PlayerPrefs.Save();

            SetMaxLevel(level, currentBentuk);

            SceneManager.LoadScene("Main");
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
 