using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] string text1;
    [SerializeField] string text2;

    [ContextMenu("Coba")]
    public void Coba()
    {
        string sqrtSymbol = $"\u221A";
        var newStr = string.Empty;

        Regex rgx = new Regex("[\u221A]");

        bool match = rgx.IsMatch(text1);

        newStr = Regex.Replace(text1, sqrtSymbol, "sqrt");

        var splitted = text1.Split(sqrtSymbol);

        Debug.Log($"{splitted[0]}{sqrtSymbol}{splitted[1]}");

        /*if (text1.Contains (sqrtSymbol))
        {
            for (int i = 0; i < text1.Length; i++)
            {

            }
        }*/
    }
}
