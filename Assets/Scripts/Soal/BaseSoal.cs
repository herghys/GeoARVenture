using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARMath.Exercise
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Questions", menuName ="ARMath/Exercise")]
    public class BaseSoal : ScriptableObject
    {
		public QuestionType type;
		public int exerciseNumber;
		[TextArea(0, 7)]
		public string baseQuestion;
        public List<Questions> questions;

        public int GetTotalSoal()
        {
            return questions.Count;
        }
    }
}


public enum QuestionType
{
	Essay, YesNo
}