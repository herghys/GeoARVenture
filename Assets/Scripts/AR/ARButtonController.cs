// using System.Collections.Generic;
// using UnityEngine;

// namespace ARMath.AR
// {
//     public class ARButtonController : MonoBehaviour
//     {
//         [SerializeField] int totalSides;
//         [SerializeField] ARButton buttonPrefab;
//         [SerializeField] List<ARButton> arButtonList;

//         private void Awake()
//         {
//             //Spawn Button
//             SpawnButton(totalSides);
//         }

//         void SpawnButton(int _sides)
//         {
//             for (int i = 0; i < _sides; i++)
//             {
//                 var arButton = Instantiate(buttonPrefab, transform);
//                 arButton.Index = i;
//                 arButtonList.Add(arButton);
//             }
//         }

//         public void Subscribe(ARButton button)
//         {
//             if(arButtonList == null) arButtonList = new List<ARButton> ();
//             arButtonList.Add(button);
//         }

//         public void Unsubscribe(ARButton button)
//         {
//             arButtonList.Remove(button);
//         }

//         public void OnTargetFound(int _sides)
//         {
//             SpawnButton(_sides);
//         }

//         public void OnTargetLost(int _sides)
//         {

//         }

//         public void ResetController()
//         {
//             arButtonList.Clear();
//         }
//     }
// }
