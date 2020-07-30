 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabSelected;

    public List<GameObject> objectsToSwap;
    public List<GameObject> materiToSwap;

    private int indexSelected = 10;
     
    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        //ResetTabs();
    }

    public void OnTabExit(TabButton button)
    {
        //ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        int index = button.transform.GetSiblingIndex();
        
        if (indexSelected == index)
        {
            switch (index)
            {
                case 0:
                    PlayerPrefs.SetString("Bentuk", "Kubus");
                    break;
                case 1:
                    PlayerPrefs.SetString("Bentuk", "Balok");
                    break;
                case 2:
                    PlayerPrefs.SetString("Bentuk", "Prisma");
                    break;
                case 3:
                    PlayerPrefs.SetString("Bentuk", "Limas");
                    break;
                default:
                    
                    break;
            }
            PlayerPrefs.Save();
            SceneManager.LoadScene("PilihMateri");
        }
        else
        {
            //Debug.Log("MASUK");
            indexSelected = index;
            ResetTabs();
            button.background.sprite = button.tabSelected;
            //int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                if (i == index)
                {
                    objectsToSwap[i].SetActive(true);
                    materiToSwap[i].SetActive(true);
                }
                else
                {
                    objectsToSwap[i].SetActive(false);
                    materiToSwap[i].SetActive(false);
                }
            }    
        }
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            button.background.sprite = button.tabExit;
        }
    }
}
