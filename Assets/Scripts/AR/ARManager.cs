using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ARMath.AR.Manager
{
    public class ARManager : MonoBehaviour
    {
        public void GoToScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}