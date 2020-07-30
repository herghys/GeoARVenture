using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Cubex.Characters;

namespace Cubex
{
    public sealed class GameManager : MonoBehaviour
    {
        public delegate void ResetHandler();

        public static event ResetHandler OnReset;

        private static GameManager m_Singleton;
        
        [SerializeField]
        protected SpriteRenderer imageBackground;
        [SerializeField]
        protected List<Sprite> mBackgrounds;
        [SerializeField]
        protected List<GameObject> mKubusLevel;
        [SerializeField]
        protected List<GameObject> mBalokLevel;
        [SerializeField]
        protected List<GameObject> mPrismaLevel;
        [SerializeField]
        protected List<GameObject> mLimasLevel;

        [SerializeField]
        protected GameObject kakekObject;

        private int curLevel;
        private string curMateri;

        [SerializeField]
        protected Text textLevel;

        public static GameManager Singleton
        {
            get
            {
                return m_Singleton;
            }
        }

        [SerializeField]
        private Character m_MainCharacter;

        private bool m_GameStarted = false;
        private bool m_GameRunning = false;

        void Awake()
        {
            if (m_Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            m_Singleton = this;
            LoadLevel();
        }

        void UpdateDeathEvent(bool isDead)
        {
            if (isDead)
            {
                StartCoroutine(DeathCrt());
            }
            else
            {
                StopCoroutine("DeathCrt");
            }
        }

        IEnumerator DeathCrt()
        {
            /*m_LastScore = m_Score;
            if (m_Score > m_HighScore)
            {
                m_HighScore = m_Score;
            }
            if (OnScoreChanged != null)
            {
                OnScoreChanged(m_Score, m_HighScore, m_LastScore);
            }
            */
            yield return new WaitForSecondsRealtime(1.5f);

            //EndGame();
            //var endScreen = UIManager.Singleton.UISCREENS.Find(el => el.ScreenInfo == UIScreenInfo.END_SCREEN);
            //UIManager.Singleton.OpenScreen(endScreen);
        }

        private void Start()
        {
            
            m_MainCharacter.IsDead.AddEventAndFire(UpdateDeathEvent, this);
            //m_StartScoreX = m_MainCharacter.transform.position.x;
            //Init();
        }

        public void Init()
        {
            //EndGame();
            //UIManager.Singleton.Init();
            StartCoroutine(Load());
            
        }

        public void LoadLevel()
        {
            curMateri = PlayerPrefs.GetString("Bentuk", "Kubus");
            curLevel = PlayerPrefs.GetInt(curMateri, 1);

            Debug.Log("MATERI : " + curMateri + " " + curLevel);

            textLevel.text = CekMateri();

            //Background
            switch (curMateri)
            {
                case "Kubus":
                    imageBackground.sprite = mBackgrounds[0];
                    //Level
                    switch (curLevel)
                    {
                        case 1:
                            mKubusLevel[0].SetActive(true);
                            break;
                        case 2:
                            mKubusLevel[1].SetActive(true);
                            break;
                        case 3:
                            mKubusLevel[2].SetActive(true);
                            break;
                        case 4:
                            mKubusLevel[3].SetActive(true);
                            break;
                        case 5:
                            mKubusLevel[4].SetActive(true);
                            break;
                        default:
                            mKubusLevel[0].SetActive(true);
                            break;
                    }
                    break;
                case "Balok":
                    imageBackground.sprite = mBackgrounds[1];
                    //Level
                    switch (curLevel)
                    {
                        case 1:
                            mBalokLevel[0].SetActive(true);
                            break;
                        case 2:
                            mBalokLevel[1].SetActive(true);
                            break;
                        case 3:
                            mBalokLevel[2].SetActive(true);
                            break;
                        case 4:
                            mBalokLevel[3].SetActive(true);
                            break;
                        case 5:
                            mBalokLevel[4].SetActive(true);
                            break;
                        default:
                            mBalokLevel[0].SetActive(true);
                            break;
                    }
                    break;
                case "Prisma":
                    imageBackground.sprite = mBackgrounds[2];
                    //Level
                    switch (curLevel)
                    {
                        case 1:
                            mPrismaLevel[0].SetActive(true);
                            break;
                        case 2:
                            mPrismaLevel[1].SetActive(true);
                            break;
                        case 3:
                            mPrismaLevel[2].SetActive(true);
                            break;
                        case 4:
                            mPrismaLevel[3].SetActive(true);
                            break;
                        case 5:
                            mPrismaLevel[4].SetActive(true);
                            break;
                        default:
                            mPrismaLevel[0].SetActive(true);
                            break;
                    }
                    break;
                case "Limas":
                    imageBackground.sprite = mBackgrounds[3];
                    //Level
                    switch (curLevel)
                    {
                        case 1:
                            mLimasLevel[0].SetActive(true);
                            break;
                        case 2:
                            mLimasLevel[1].SetActive(true);
                            break;
                        case 3:
                            mLimasLevel[2].SetActive(true);
                            break;
                        case 4:
                            mLimasLevel[3].SetActive(true);
                            break;
                        case 5:
                            mLimasLevel[4].SetActive(true);
                            break;
                        default:
                            mLimasLevel[0].SetActive(true);
                            break;
                    }
                    break;
                default:
                    imageBackground.sprite = mBackgrounds[0];
                    //Level
                    switch (curLevel)
                    {
                        case 1:
                            mKubusLevel[0].SetActive(true);
                            break;
                        case 2:
                            mKubusLevel[1].SetActive(true);
                            break;
                        case 3:
                            mKubusLevel[2].SetActive(true);
                            break;
                        case 4:
                            mKubusLevel[3].SetActive(true);
                            break;
                        case 5:
                            mKubusLevel[4].SetActive(true);
                            break;
                        default:
                            mKubusLevel[0].SetActive(true);
                            break;
                    }
                    break;
            }

            CekKakek();
            
        }

        void CekKakek()
        {
            if(curMateri == "Kubus" && curLevel == 1)
            {
                //kakekObject.SetActive(true);
            }
            else
            {
                //kakekObject.SetActive(false);
            }
        }

        private string CekMateri()
        {
            string materi = "";

            switch (curLevel)
            {
                case 1:
                    materi = "Unsur-unsur";
                    break;
                case 2:
                    materi = "Jaring-jaring";
                    break;
                case 3:
                    materi = "Luas Permukaan";
                    break;
                case 4:
                    materi = "Volume";
                    break;
                case 5:
                    materi = "Kesimpulan";
                    break;
                default:
                    materi = "";
                    break;
            }

            materi = materi + " " + curMateri;

            return materi;
        }

        IEnumerator Load()
        {
            //var startScreen = UIManager.Singleton.UISCREENS.Find(el => el.ScreenInfo == UIScreenInfo.START_SCREEN);
            yield return new WaitForSecondsRealtime(3f);
            //UIManager.Singleton.OpenScreen(startScreen);
        }

        public void StartGame()
        {
            m_GameStarted = true;
            ResumeGame();
        }

        public void StopGame()
        {
            m_GameRunning = false;
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            m_GameRunning = true;
            Time.timeScale = 1f;
        }

        public void EndGame()
        {
            m_GameStarted = false;
            StopGame();
        }

        public void Reset()
        {
            //m_Score = 0f;
            if (OnReset != null)
            {
                OnReset();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void BackToHome()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.UnloadSceneAsync(sceneIndex);

            SceneManager.LoadScene("MainMenu");
            
        }

        public void LoadMateri()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene("Materi");
            SceneManager.UnloadSceneAsync(sceneIndex);
        }
    }
}