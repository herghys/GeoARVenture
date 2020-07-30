using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifikasiWindow : MonoBehaviour
{
    [SerializeField]
    protected Button buttonBenar;
    [SerializeField]
    protected Button buttonSalah;
    [SerializeField]
    protected Color colorBenar;
    [SerializeField]
    protected Color colorSalah;
    [SerializeField]
    protected Color colorDefault;
    [SerializeField]
    protected GameObject buttonNext;

    [Header("Container")]
    [Space]
    [SerializeField]
    protected Text teksSoal;
    [SerializeField]
    protected Text titleSoal;

    [Header("Kubus")]
    [Space]
    [SerializeField]
    protected List<string> verifikasiKubus;
    [SerializeField]
    protected List<string> jawabanKubus;

    [Header("Balok")]
    [Space]
    [SerializeField]
    protected List<string> verifikasiBalok;
    [SerializeField]
    protected List<string> jawabanBalok;

    [Header("Prisma")]
    [Space]
    [SerializeField]
    protected List<string> verifikasiPrisma;
    [SerializeField]
    protected List<string> jawabanPrisma;

    [Header("Limas")]
    [Space]
    [SerializeField]
    protected List<string> verifikasiLimas;
    [SerializeField]
    protected List<string> jawabanLimas;

    private string curMateri;
    private int curLevel;
    private List<string> soalVerifikasi;
    private List<string> jawabanVerifikasi;
    private int i;

    // Start is called before the first frame update
    void Start()
    {

        i = 0;
        curMateri = PlayerPrefs.GetString("Bentuk", "Kubus");
        curLevel = PlayerPrefs.GetInt(curMateri, 1);
        if (curLevel == 5)
        {
            LoadSoal();
            SetSoal(i);
            titleSoal.text = "Verifikasi Materi " + curMateri;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadSoal()
    {
        switch (curMateri)
        {
            case "Kubus":
                soalVerifikasi = verifikasiKubus;
                jawabanVerifikasi = jawabanKubus;
                break;
            case "Balok":
                soalVerifikasi = verifikasiBalok;
                jawabanVerifikasi = jawabanBalok;
                break;
            case "Prisma":
                soalVerifikasi = verifikasiPrisma;
                jawabanVerifikasi = jawabanPrisma;
                break;
            case "Limas":
                soalVerifikasi = verifikasiLimas;
                jawabanVerifikasi = jawabanLimas;
                break;
            default:
                soalVerifikasi = verifikasiKubus;
                jawabanVerifikasi = jawabanKubus;
                break;
        }
        //PlayerPrefs.SetInt(curMateri, 5);
    }

    void SetSoal(int index)
    {
        ResetButton();
        teksSoal.text = soalVerifikasi[index];
    }

    public void PilihJawaban(string jawaban)
    {
        StartCoroutine(CekJawaban(jawaban));
    }

    IEnumerator CekJawaban(string jawaban)
    {
        if(jawaban == jawabanVerifikasi[i])
        {
            if(jawaban == "Benar")
            {
                //buttonBenar.colors = colorBenar;
                buttonBenar.image.color = colorBenar;
            }
            else
            {
                //buttonSalah.colors = colorBenar;
                buttonSalah.image.color = colorBenar;
            }
            showToast("Selamat kamu benar", 3);
        }
        else
        {
            if (jawaban == "Benar")
            {
                //buttonBenar.colors = colorSalah;
                buttonBenar.image.color = colorSalah;
            }
            else
            {
                //buttonSalah.colors = colorSalah;
                buttonSalah.image.color = colorSalah;
            }
            showToast("Maaf kamu kurang tepat", 3);
        }

        i++;

        yield return new WaitForSeconds(3.5f);

        if (i < 2)
        {
            SetSoal(i);
        }
        else
        {
            buttonNext.SetActive(true);
        }
    }

    void ResetButton()
    {
        buttonBenar.image.color = colorDefault;
        buttonSalah.image.color = colorDefault;
    }

    public Text txt;

    void showToast(string text,
        int duration)
    {
        StartCoroutine(showToastCOR(text, duration));
    }

    private IEnumerator showToastCOR(string text,
    int duration)
    {
        Color orginalColor = new Color(0f, 0f, 0f, 0f);

        txt.text = text;
        txt.enabled = true;

        //Fade in
        yield return fadeInAndOut(txt, true, 0.1f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(txt, false, 0.5f);

        txt.enabled = false;
        txt.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = Color.white;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }

    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}