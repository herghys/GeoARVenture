using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

namespace ARMath.Exercise
{
    public class ARManagerExercise : MonoBehaviour
    {
		string sqrtSymbol = $"\u221A";
		//string baseQuestionInfo, mainQuestionInfo;

		[Header("Reference")]
		[SerializeField] ARManagerExerciseUI ui;
		[SerializeField] bool isEnd = false;

		[Header("Answers")]
		[SerializeField] string inputEssayAnswer;
		[SerializeField] bool inputYesNoAnswer;

		[Header("Questions")]
		[SerializeField] int totalBaseQuestion;
		[SerializeField] int baseQuestionIndex;
		[SerializeField] int totalMainQuestions;
		[SerializeField] int mainQuestionIndex;
		[SerializeField] List<BaseSoal> baseQuestions;
		[SerializeField] List<GameObject> question3DModel;
		[SerializeField] QuestionType questionType;

		[SerializeField] BaseSoal baseQuestion;
		[SerializeField] Questions mainQuestion;
		[SerializeField] Answers currentAnswer;

		#region Property GetSetter
		public int BaseQuestionIndex
		{
			get => baseQuestionIndex;
			set => baseQuestionIndex = value;
		}
		public QuestionType QuestType
		{
			get => questionType;
			set => questionType = value;
		}
		#endregion

		private void Awake()
		{
			if (ui is null) ui = GetComponent<ARManagerExerciseUI>();
		}

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			GetTotalBaseQuestion();
			baseQuestionIndex = 0;
			mainQuestionIndex = 0;
			isEnd = false;
			LoadQuestion();
		}

		public void Reset()
		{
			Init();
			ui.Reset();
		}

		[ContextMenu("Load Question")]
		public void LoadQuestion()
		{
				GetQuestionType();
				GetQuestion();
				GetMainQuestion();

				ui.SetQuestionType(QuestType);
				ui.SetQuestion(baseQuestion.exerciseNumber, baseQuestion.baseQuestion, mainQuestion.questionText);
		}

		[ContextMenu("Get Total Base Questions")]
		public void GetTotalBaseQuestion()
		{
			totalBaseQuestion = baseQuestions.Count;
			GetTotalMainQuestion();
		}

		public void GetTotalMainQuestion()
		{
			if (baseQuestionIndex <= totalBaseQuestion - 1)
				totalMainQuestions = baseQuestions[baseQuestionIndex].GetTotalSoal();
		}

		[ContextMenu("Get Question Type")]
		public void GetQuestionType() 
		{
			if (baseQuestionIndex < totalBaseQuestion)
				questionType = baseQuestions[baseQuestionIndex].type;
		}

		public void GetQuestion()
		{
			if (baseQuestionIndex < totalBaseQuestion)
			{
				baseQuestion = baseQuestions[baseQuestionIndex];
				//baseQuestionInfo = baseQuestions[baseQuestionIndex].baseQuestion;
				GetTotalMainQuestion();
			}
		}

		public void GetMainQuestion()
		{
			if (mainQuestionIndex < totalMainQuestions)
			{
				mainQuestion = baseQuestion.questions[mainQuestionIndex];
				//mainQuestionInfo = baseQuestions[baseQuestionIndex].questions[mainQuestionIndex].questionText;
				GetAnswers();
			}
		}

		public void GetAnswers()
		{
			currentAnswer = mainQuestion.answers;
		}

		/// <summary>
		/// Handles string input
		/// </summary>
		public void CheckAnswers()
		{
			SubmitAnswer(inputEssayAnswer == currentAnswer.GetEssayAnswer());
		}

		/// <summary>
		/// Handles boolean YesNo Type
		/// </summary>
		/// <param name="answer"></param>
		public void CheckAnswers(bool answer)
		{
			SubmitAnswer(answer == currentAnswer.yesNoAnswerInfo);
		}

		public void SubmitAnswer(bool correct)
		{
			var state = correct ? ExerciseState.Correct : ExerciseState.Incorrect;
			//var isEnd = baseQuestionIndex >= totalBaseQuestion - 1;

			if (isEnd)
			{
				ui.SubmitAnswer(state, true);
				return;
			}

			ui.SubmitAnswer(state, false);
			if (mainQuestionIndex != totalMainQuestions - 1)
			{
				mainQuestionIndex++;
				LoadQuestion();
				return;
			}

			baseQuestionIndex++;
			if (baseQuestionIndex < totalBaseQuestion)
			{
				mainQuestionIndex = 0;
				LoadQuestion();
			}
			if (baseQuestionIndex >= totalBaseQuestion - 1)
			{
				isEnd = true;
			}
		}

		#region Event Handler
		public void EssayAnswerHandler(string answer)
		{
			inputEssayAnswer = answer;
			if (currentAnswer.hasSymbol)
			{
				Regex rgx = new Regex("[\u221A]");
				if (rgx.IsMatch(answer))
				{
					inputEssayAnswer = Regex.Replace(answer, sqrtSymbol, "sqrt");
				}
			}
		}
		#endregion

		#region Other
		public void GoToScene(string scene)
		{
			SceneManager.LoadScene(scene);
		}
		#endregion
	}
}


/*
		#region Debug
		#if UNITY_EDITOR
		[ContextMenu("Test Essay Answer")]
		public void TestAnswer()
		{
			if (questionType == QuestionType.Essay) DebugCheckAnswers();
			else DebugCheckYesNoAnswers();
		}
		public void DebugCheckAnswers()
		{
			Debug.Log("Essayt");
			if (currentAnswer.hasSymbol)
			{
				Regex rgx = new Regex("[\u221A]");
				if (rgx.IsMatch(inputEssayAnswer))
				{
					inputEssayAnswer = Regex.Replace(inputEssayAnswer, sqrtSymbol, "sqrt");
				}
			}
			SubmitAnswer(inputEssayAnswer == currentAnswer.GetEssayAnswer(), QuestionType.Essay);
		}

		public void DebugCheckYesNoAnswers()
		{
			Debug.Log("Yes No");
			SubmitAnswer(inputYesNoAnswer == currentAnswer.yesNoAnswerInfo, QuestionType.YesNo);
		}
#endif
		#endregion*/