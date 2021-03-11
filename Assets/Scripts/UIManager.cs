using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text p1Name;
    public TMP_Text p2Name;

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
    [SerializeField]
    private GameObject wallOfShamePanel;
    [SerializeField]
    private GameObject shameEraseConfirmPanel;


    private UIGeneralStates actualState;


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

        PauseGamePanelToggle(false);
        ClearShameConfirmPanelToggle(false);
        FinishGamePanelToggle(false);
        TakePicturePanelToggle(false);
        WallOfShamePanelToggle(false);
    }

    public void FinishGamePanelToggle(bool isActive)
    {
        restartPanel.SetActive(isActive);
    }

    public void ClearShameConfirmPanelToggle(bool isActive)
    {
        shameEraseConfirmPanel.SetActive(isActive);
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

    public void WallOfShamePanelToggle(bool isActive)
    {
        wallOfShamePanel.SetActive(isActive);
    }

    public string GetPlayerNames()
    {
        try
        {
            p1Name = GameObject.Find("P1InputNameText").GetComponent<TMP_Text>();
            p2Name = GameObject.Find("P2InputNameText").GetComponent<TMP_Text>();
        }
        catch
        {
            Debug.LogError("You have no inputTextObjects assigned");
        }

        return p1Name.text + " - " + p2Name.text;
    }

}
