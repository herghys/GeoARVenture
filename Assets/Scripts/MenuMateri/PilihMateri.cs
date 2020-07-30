using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PilihMateri : MonoBehaviour
{

    [SerializeField]
    protected List<GameObject> buttonMateri;

    private int curLevel;
    private string curMateri;
    private int maxLevel;

    // Start is called before the first frame update
    void Start()
    {
        //Ngecek Materi dan Level
        curMateri = PlayerPrefs.GetString("Bentuk", "Kubus");
        curLevel = PlayerPrefs.GetInt(curMateri, 1);
        var level = "Max" + curMateri;
        maxLevel = PlayerPrefs.GetInt(level, 1);

        SetLevelActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLevelActive()
    {
        if (maxLevel >= 1)
        {
            buttonMateri[0].SetActive(true);

            if(maxLevel >= 2)
            {
                buttonMateri[1].SetActive(true);

                if (maxLevel >= 3)
                {
                    buttonMateri[2].SetActive(true);

                    if (maxLevel >= 4)
                    {
                        buttonMateri[3].SetActive(true);

                        if (maxLevel >= 5)
                        {
                            buttonMateri[4].SetActive(true);

                        }
                    }
                }
            }
        }
    }

    public void LoadLevel(int level)
    {
        string materi = "";
        PlayerPrefs.SetInt(curMateri, level);
        PlayerPrefs.Save();
        switch (level)
        {
            case 1:
                materi = "Unsur-unsur";
                break;
            case 2:
                materi = "Jaring-jaring";
                break;
            case 3:
                materi = "Luas Permukaan";
                break;
            case 4:
                materi = "Volume";
                break;
            default:
                materi = "";
                break;
        }

        PlayerPrefs.SetString("Materi", materi + " " + curMateri);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Main");
    }

    public void BackToHome()
    {
        SceneManager.LoadScene("Materi");
    }
}
