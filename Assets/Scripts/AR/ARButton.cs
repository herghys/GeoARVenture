// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using ARMath.GeoShape;
// using ARMath.Events;

// namespace ARMath.AR
// {
//     [RequireComponent(typeof(Button))]
//     public class ARButton : MonoBehaviour
//     {
//         [SerializeField] protected string text = "Luas Permukaan";
//         [SerializeField] protected int index;
//         [SerializeField] protected Button button;

//         [SerializeField] TextMeshProUGUI buttonText;
//         [SerializeField] ARButtonController controller;
//         [SerializeField] ARShapeEvents shapeEvents;
//         public int Index { set { index = value; } }

//         private void Awake()
//         {
//             button = GetComponent<Button>();
//             controller = gameObject.GetComponentInParent<ARButtonController>();
//             controller.Subscribe(this);
//         }

//         private void OnEnable()
//         {
            
//         }

//         private void Start()
//         {
//             ChangeText();
//         }

//         protected void ChangeText()
//         {
//             GetComponentInChildren<TextMeshProUGUI>().text = $"{text} {index + 1}";
//         }

//         private void OnDisable()
//         {
//             button.onClick.RemoveAllListeners();
//         }
//         protected void InitNetAnimListeners()
//             => button.onClick.AddListener(()
//                 => ShapeManager.Instance.OnNetClicked(index));
            
//         protected void InitPlaceSideListeners() 
//             => button.onClick.AddListener(() 
//                 => ShapeManager.Instance.OnPlaceClicked(index));

//         protected void InitPerSideAnimListeners() 
//             => button.onClick.AddListener(() 
//                 => ShapeManager.Instance.OnAnimationPerSideClicked(index));

//         protected void InitPlayAnimationListeners() 
//             => button.onClick.AddListener(() 
//                 => ShapeManager.Instance.OnPlayAnimationClicked());
//     }
// }
