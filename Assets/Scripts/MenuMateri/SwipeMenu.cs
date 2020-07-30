using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cubex.UI
{
    public class SwipeMenu : MonoBehaviour
    {
        [SerializeField]
        protected GameObject scrollBar;
        float scroll_pos = 0;
        float[] pos;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            pos = new float[transform.childCount];
            float distance = 1f / (pos.Length - 1f);

            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = distance * i;
            }
            if (Input.GetMouseButton(0))
            {
                scroll_pos = scrollBar.GetComponent<Scrollbar>().value;
            }
            else
            {
                for(int i=0;i < pos.Length; i++)
                {
                    if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                    {
                       scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                        //Debug.Log("POS 1 " + scrollBar.GetComponent<Scrollbar>().value);
                        //scrollBar.GetComponent<Scrollbar>().value = 1f - Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                        //Debug.Log("POS 2 " + scrollBar.GetComponent<Scrollbar>().value);
                        //Debug.Log("i = " + (1f - pos[i]));
                    }
                }
            }
        }
    }
}