using System.Collections.Generic;
using UnityEngine;

public class ButtonHelper : MonoBehaviour
{
    [SerializeField] List<GameObject> gameObjects;

    [SerializeField] UnityEngine.UI.Button button;

    private void OnEnable()
    {
        button.onClick.AddListener(SetGOActive);
        button.onClick.AddListener(SetGOInactive);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(SetGOActive);
        button.onClick.RemoveListener(SetGOInactive);
    }

    public void SetGOActive()
    {
        gameObjects.ForEach(go => go.SetActive(true));
    }

    public void SetGOInactive()
    {
        gameObjects.ForEach(go => go.SetActive(false));
    }
  

#if UNITY_EDITOR
    private void OnValidate()
    {
        button = GetComponent<UnityEngine.UI.Button>();
    }
#endif
}
