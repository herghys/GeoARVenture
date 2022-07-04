using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ARMath.AR.Manager
{
    public class ARManagerLP : MonoBehaviour
    {
		
        public void GoToScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

		
	}
}