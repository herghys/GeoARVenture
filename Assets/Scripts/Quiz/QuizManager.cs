using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cubex.UI;

namespace Cubex.Quiz
{
    public class QuizManager : MonoBehaviour
    {
        [SerializeField]
        private QuizUI quizUI;
        [SerializeField]
        private UIManager uiManager;
        [SerializeField]
        private UIWindow windowReset;
        [SerializeField]
        private List<Question> questions;
        [SerializeField]
        private Text labelSkor;
        
        private Question selectedQuestion;

        private List<Question> myQuestion;
        private List<int> listScore;
        
        //private Text nomorSoal;

        private int nomorSoal;
        private int skor;

        // Start is called before the first frame update
        void Start()
        {
            nomorSoal = 1;
            myQuestion = new List<Question>(questions);
            listScore = new List<int>(5);
            SelectQuestion();
            
        }

        void SelectQuestion()
        {
            labelSkor.text = "" + skor;
            if (nomorSoal <= 10)
            {
                int val = Random.Range(0, myQuestion.Count);
                selectedQuestion = myQuestion[val];

                quizUI.SetQuestion(selectedQuestion, nomorSoal);
                myQuestion.RemoveAt(val);
                nomorSoal++;
            }
            else
            {
                Debug.Log(myQuestion.Count);
                uiManager.OpenWindow(windowReset);
                SaveHighScore();
            }
        }

        public bool Answer(string answered)
        {
            bool correctAns = false;

            if (answered == selectedQuestion.correctAns)
            {
                //YES
                correctAns = true;
                skor += 10;
            }
            else
            {
                //NO
            }

            Invoke("SelectQuestion", 0.4f);

            return correctAns;
        }

        public void RestartQuiz()
        {
            nomorSoal = 1;
            skor = 0;
            myQuestion = new List<Question>(questions);
            SelectQuestion();
        }

        void SaveHighScore()
        {
            int storedScore = skor;
            int scoreA = 0;
            string score = "";
            for (int i = 0; i<5; i++)
            {
                listScore.Add(0);

                scoreA = storedScore;

                score = "Highscore" + i;
                listScore[i] = PlayerPrefs.GetInt(score, 0);

                if (storedScore > listScore[i])
                {
                    storedScore = listScore[i];
                    listScore[i] = scoreA;
                }

                PlayerPrefs.SetInt(score, listScore[i]);
            }
        }

        public void BackToHome()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    [System.Serializable]
    public class Question
    {

        public string questionInfo;
        public QuestionType questionType;
        public Sprite questionImg;
        public AudioClip questionClip;
        public UnityEngine.Video.VideoClip questionVideo;
        public List<string> options;
        public string correctAns;
    }

    [System.Serializable]
    public enum QuestionType
    {
        TEXT,
        IMAGE,
        VIDEO,
        AUDIO
    }
}