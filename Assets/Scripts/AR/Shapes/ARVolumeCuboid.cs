using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARVolumeCuboid : MonoBehaviour
{
    [SerializeField] ARVolumeCuboid reference;
    [SerializeField] List<GameObject> fill;
    [SerializeField] Button prevButton; [SerializeField] Button nextButton;
    [SerializeField] int currentFill = 0;
    [SerializeField] int totalFill;
    public int TotalFill {get => totalFill;}

    void Awake()
    {
        if (totalFill != fill.Count) totalFill = fill.Count;
    }

    void OnEnable()
    {
        
    }

    void CheckFillReference(){

    }

    public void DisableFill(){
        if (currentFill > 0 && gameObject.activeSelf){
            fill[currentFill].SetActive(false);
            currentFill--;
        }
    }

    public void EnableFill(){
        if (currentFill < totalFill && gameObject.activeSelf){
        fill[currentFill].SetActive(true);
        currentFill++;
        }
    }

    [ContextMenu("Get Fill")]
    public void GetFill(){
        totalFill = fill.Count;
    }
}
