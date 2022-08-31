using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ARMath.Exercise
{
	public class ExerciseFeedback : MonoBehaviour
	{
		[SerializeField] CanvasGroup group;
		[SerializeField] Color correctColor;
		[SerializeField] Color incorrectColor;

		[Header("Answer")]
		[SerializeField] GameObject answerFeedback;
		[SerializeField] Image feedbackImage;
		[SerializeField] TextMeshProUGUI feedbackText;

		[Header("End")]
		[SerializeField] GameObject endFeedback;

		public void Reset()
		{
			DisableCanvasGroup();
		}

		private void Awake()
		{
			DisableCanvasGroup();
		}

		void EnableCanvasGroup()
		{
			group.interactable = true;
			group.alpha = 1;
			group.blocksRaycasts = true;
		}

		void DisableCanvasGroup()
		{
			group.interactable = false;
			group.alpha = 0;
			group.blocksRaycasts = false;
		}

		void EnableAnswerFeedback()
		{
			answerFeedback.SetActive(true);
			endFeedback.SetActive(false);
		}

		void EnableEndFeedback()
		{
			answerFeedback.SetActive(false);
			endFeedback.SetActive(true);
		}

		void DisableFeedback()
		{
			answerFeedback.SetActive(false);
			endFeedback.SetActive(false);
		}

		public void ShowFeedback(ExerciseState state, bool isEnd = false)	
		{
			EnableCanvasGroup();
			/*if (state == ExerciseState.End)
			{
				answerFeedback.SetActive(false);
				endFeedback.SetActive(true);
				return;
			}*/

			/*answerFeedback.SetActive(true);
			endFeedback.SetActive(false);*/
			EnableAnswerFeedback();
			if (isEnd)
			{
				if (state == ExerciseState.Correct) StartCoroutine(IE_SetCorrect(isEnd));
				else StartCoroutine(IE_SetIncorrect(isEnd));
				return;
			}
			if (state == ExerciseState.Correct) StartCoroutine(IE_SetCorrect(isEnd));
			else StartCoroutine(IE_SetIncorrect(isEnd));
		}

		public void ShowEnd()
		{
			EnableEndFeedback();
			/*answerFeedback.SetActive(false);
			endFeedback.SetActive(true);*/
		}

		public IEnumerator IE_SetCorrect(bool isEnd)
		{
			feedbackImage.color = correctColor;
			feedbackText.text = "Yeay, kamu benar!";
			yield return new WaitForSeconds(1.5f);
			answerFeedback.SetActive(false);
			endFeedback.SetActive(false);
			if (isEnd)
			{
				ShowEnd();
				yield break;
			}
			DisableCanvasGroup();
		}

		public IEnumerator IE_SetIncorrect(bool isEnd = false)
		{
			feedbackImage.color = incorrectColor;
			feedbackText.text = "Maaf, belajar lagi ya!";

			yield return new WaitForSeconds(1.5f);
			answerFeedback.SetActive(false);
			endFeedback.SetActive(false);
			if (isEnd)
			{
				ShowEnd();
				yield break;
			}
			DisableCanvasGroup();
		}

	}
}

public enum ExerciseState
{
	Correct, Incorrect, End
}
