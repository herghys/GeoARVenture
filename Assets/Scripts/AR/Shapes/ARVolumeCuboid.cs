using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARVolumeCuboid : MonoBehaviour
{
    //[SerializeField] ARVolumeCuboid reference;
    [SerializeField] List<GameObject> fill;
    [SerializeField] List<bool> fillState;
    [SerializeField] Button prevButton; [SerializeField] Button nextButton;
    [SerializeField] bool allFillDisabled = true;
    [SerializeField] bool allFillEnabled = false;
    [SerializeField] int currentFill = 0;
    [SerializeField] int totalFill;

    public bool AllFillEnabled{get=> allFillEnabled;}
    public int CurrentFill {get => currentFill;}
    public bool AllFillDisabled{get=> allFillDisabled;}
    public int TotalFill {get => totalFill;}

    void Awake()
    {
       for (var i = 0; i < fill.Count; i++)
       {
           fillState[i] = fill[i].gameObject.activeSelf;
       }
    }

    void OnEnable()
    {
       CheckFillState();
    }

    private void CheckFillState(){
         if (fillState.All (x => x == false)){
            prevButton.interactable = false;
        }else if (fillState.All (x => x == true)){
            nextButton.interactable = false;
        }
        else{
            prevButton.interactable = true;
            nextButton.interactable = true;
        }
    }

    public void DisableFill()
    {
        if (!gameObject.activeSelf) return;
        if (currentFill > 0){
            fill[currentFill-1].SetActive(false);
            fillState[currentFill-1] = false;
            currentFill--;
        }
        CheckFillState();
    }
    public void EnableFill(){
        if (!gameObject.activeSelf) return;
        if (currentFill <= totalFill)
        {
            fill[currentFill].SetActive(true);
            fillState[currentFill] = true;
            currentFill++;
        }

        CheckFillState();

    }

    [ContextMenu("Get Fill")]
    public void GetFill(){
        totalFill = fill.Count;
    }
}
