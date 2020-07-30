using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Cubex.AR
{
    public class BalokTouch : MonoBehaviour
    {
        private int sceneIndex;

        [Header("Balok Unsur")]
        [Space]
        [SerializeField]
        protected Text teksPenjelasan;

        [Header("Balok Jaring-Jaring")]
        [Space]
        [SerializeField]
        protected MeshRenderer balokJaring;
        [SerializeField]
        protected Animator balokJaringAnimator;

        [Header("Balok Luas")]
        [Space]
        [SerializeField]
        protected List<GameObject> balokLuas;
        [SerializeField]
        protected List<GameObject> balokButton;
        [SerializeField]
        protected GameObject balokPlayButton;

        // Start is called before the first frame update
        void Start()
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                BalokJaring();
            }
        }

        void BalokJaring()
        {
            // The pos of the touch on the screen
            Vector2 vTouchPos = Input.GetTouch(0).position;

            // The ray to the touched object in the world
            Ray ray = Camera.main.ScreenPointToRay(vTouchPos);

            // Your raycast handling
            RaycastHit vHit;

            if (Physics.Raycast(ray.origin, ray.direction, out vHit))
            {
                if (vHit.transform.tag == "JaringBalok")
                {
                    if (balokJaringAnimator.GetBool("Clicked"))
                    {
                        balokJaringAnimator.SetBool("Clicked", false);
                    }
                    else
                    {
                        balokJaringAnimator.SetBool("Clicked", true);
                    }
                }
            }
        }

        public void PlayAnimation()
        {
            if (balokJaringAnimator.GetBool("Clicked"))
            {
                balokJaringAnimator.SetBool("Clicked", false);
            }
            else
            {
                balokJaringAnimator.SetBool("Clicked", true);
            }
        }
    }
}