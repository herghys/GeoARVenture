using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cubex.UI
{
    public class KesimpulanWindow : MonoBehaviour
    {

        [SerializeField]
        protected GameObject buttonFinish;
        [SerializeField]
        protected List<GameObject> objectKesimpulan;


        // Start is called before the first frame update
        void Start()
        {
            string curMateri = PlayerPrefs.GetString("Bentuk", "Kubus");

            switch (curMateri)
            {
                case "Kubus":
                    objectKesimpulan[0].SetActive(true);
                    break;
                case "Balok":
                    objectKesimpulan[1].SetActive(true);
                    break;
                case "Prisma":
                    objectKesimpulan[2].SetActive(true);
                    break;
                case "Limas":
                    objectKesimpulan[3].SetActive(true);
                    break;
                default:
                    objectKesimpulan[0].SetActive(true);
                    break;
            }

            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}