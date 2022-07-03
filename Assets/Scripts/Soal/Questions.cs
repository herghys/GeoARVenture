using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARMath.Exercise
{
    [System.Serializable]
    public class Questions
    {
		[TextArea(0, 7)]
		public string questionText;
        public Answers answers;

        public string GetEssayCorrectAnswer()
        {
			return answers.GetEssayAnswer();
		}

        public bool GetYesNoAnswer()
        {
            return answers.GetYesNoAnswer();
        }
    }
}