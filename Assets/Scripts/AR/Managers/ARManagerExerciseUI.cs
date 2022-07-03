using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ARMath.Exercise
{
    public class ARManagerExerciseUI : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] ARManagerExercise manager;
        [SerializeField] ExerciseFeedback feedback;

        [Header("Questions")]
        [SerializeField] TextMeshProUGUI baseQuestion;
        [SerializeField] TextMeshProUGUI mainQuestion;

        [Header("UI Ref Essay")]
        [SerializeField] GameObject EssayAnswerArea;

        [Header("UI Ref YesNo")]
        [SerializeField] GameObject YesNoAnswerArea;

        [SerializeField] bool isEssay;

        private void Awake()
        {
            if (manager is null) manager = GetComponent<ARManagerExercise>();
        }

        public void Reset()
        {
            feedback.Reset();
        }

        public void ShowEnd()
        {
            Debug.Log("ShowEnd UI");
            feedback.ShowFeedback(ExerciseState.End, isEnd:true);
        }

        public void SetQuestionType(QuestionType type)
        {
            isEssay = type == QuestionType.Essay;

            EssayAnswerArea.SetActive(isEssay);
            YesNoAnswerArea.SetActive(!isEssay);
        }

        public void SetQuestion(int noQ, string baseQ, string mainQ)
        {
            baseQuestion.text = $"{noQ}. {baseQ}";
            mainQuestion.text = mainQ;
        }

        public void SubmitAnswer(ExerciseState state, bool isEnd = false)
        {
            if (state == ExerciseState.Correct)
                feedback.ShowFeedback(state, isEnd);
            else if (state == ExerciseState.Incorrect)
                feedback.ShowFeedback(state, isEnd);
        }
    }
}