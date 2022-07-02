using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARManagerUnsur : MonoBehaviour
{
    public bool IsAnimating {get; set;}

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
