using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ARMath.Exercise
{
    [System.Serializable]
    public class Answers
    {
        [Tooltip("Essay Answers")]
        [TextArea(0,1)]
		public string answerInfo;
        public bool yesNoAnswerInfo;

		public bool hasSymbol;
		public string symbol;

        public string GetEssayAnswer()
        {
            if (!hasSymbol)
            {
                symbol = string.Empty;
                return answerInfo;
            }
            else
            {
				Regex rgx = new Regex(this.symbol);
                string newAns = string.Empty;
				if (rgx.IsMatch(answerInfo))
				{
					newAns = Regex.Replace(answerInfo, this.symbol, "sqrt");
				}
                Debug.Log(newAns);
                return newAns;
            }
        }

        public bool GetYesNoAnswer()
        {
            return yesNoAnswerInfo;
        }
    }
}