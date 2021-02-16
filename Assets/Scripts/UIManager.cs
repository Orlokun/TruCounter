using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public enum UIGeneralStates
    {
        MainMenu,
        Match30,
        Match15,
    }

    private Dictionary<UIGeneralStates, GameObject> uiObjects;


    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject restartPanel;
    [SerializeField]
    private GameObject takePicturePanel;


    private UIGeneralStates actualState;


    private void Awake()
    {

    }

    public void StartDictionary()
    {
        uiObjects = new Dictionary<UIGeneralStates, GameObject>();
        uiObjects.Add(UIGeneralStates.MainMenu, GameObject.Find("MainMenu"));
        uiObjects.Add(UIGeneralStates.Match30, GameObject.Find("30GamePanel"));
        uiObjects.Add(UIGeneralStates.Match15, GameObject.Find("15GamePanel"));
    }

    public void SetState(UIGeneralStates _state)
    {
        actualState = _state;
        ResetUIObjects(_state);
    }

    private void ResetUIObjects(UIGeneralStates actualState)
    {
        foreach (KeyValuePair<UIGeneralStates, GameObject> key in uiObjects)
        {
            if (key.Key != actualState)
            {
                key.Value.SetActive(false);
            }
            else
            {
                key.Value.SetActive(true);
            }
        }

        pausePanel.SetActive(false);
        restartPanel.SetActive(false);
        takePicturePanel.SetActive(false);
    }

    public void FinishGamePanelToggle(bool isActive)
    {
        restartPanel.SetActive(isActive);
    }

    public void PauseGamePanelToggle(bool isActive)
    {
        pausePanel.SetActive(isActive);
    }

    public void TakePicturePanelToggle(bool isActive)
    {
        if (isActive)
        {
            takePicturePanel.GetComponent<WebCamManager>().RestartImage();
        }
        else
        {
            takePicturePanel.GetComponent<WebCamManager>().RestartImage();
        }
        takePicturePanel.SetActive(isActive);
    }

}
