using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Cubex.UI
{
    public class MateriScreen : MonoBehaviour
    {
        [SerializeField]
        protected TabGroup tabGroup;
        [SerializeField]
        protected TabButton tabButton;
        [SerializeField]
        protected Sprite tabSelected;
        [SerializeField]
        protected Sprite tabExit;

        private int sceneIndex;

        // Start is called before the first frame update
        void Start()
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                BackToPreviousScene();

            
        }

        private void LateUpdate()
        {
            /*tabButton.background = GetComponent<Image>();
            tabButton.tabSelected = tabSelected;
            tabButton.tabExit = tabExit;
            tabGroup.OnTabSelected(tabButton);*/
        }

        public void BackToPreviousScene()
        {
            SceneManager.LoadScene(sceneIndex - 1);
        }
    }
}