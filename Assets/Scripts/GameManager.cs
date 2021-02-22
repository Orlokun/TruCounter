using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    #region GlobalVariables
    bool isAdding;

    private int player1count;
    private int player2count;
    private int maxScore;


    private UIManager uiManager;
    private WebCamManager wcManager;
    private SoundManager soundManager;
    private ShameManager shManager;

    [SerializeField]
    private MainMatchUIObject match30GameCounter;
    [SerializeField]
    private MainMatchUIObject match15GameCounter;

    [SerializeField]
    private TMP_InputField[] nameInputs;
    private MainMatchUIObject activeGameCounter;
    #endregion

    private void Awake()
    {
        InitGlobalVariables();
        uiManager.StartDictionary();
        uiManager.SetState(UIManager.UIGeneralStates.MainMenu);
    }

    private void InitGlobalVariables()
    {
        uiManager = GetComponent<UIManager>();
        wcManager = FindObjectOfType<WebCamManager>();
        soundManager = FindObjectOfType<SoundManager>();
        shManager = FindObjectOfType<ShameManager>();
        nameInputs = new TMP_InputField[2];
    }

    public void Select30Match()
    {
        maxScore = 30;
        activeGameCounter = match30GameCounter;
        uiManager.SetState(UIManager.UIGeneralStates.Match30);
        GetNameInputs();
        player1count = 0;
        player2count = 0;
        ChangePlayerAddSubstract(true);
    }

    public void Select15Match()
    {
        maxScore = 15;
        activeGameCounter = match15GameCounter;
        uiManager.SetState(UIManager.UIGeneralStates.Match15);
        GetNameInputs();
        player1count = 0;
        player2count = 0;
        ChangePlayerAddSubstract(true);
    }

    public void BackToMenu()
    {
        maxScore = 0;
        player1count = 0;
        player2count = 0;
        activeGameCounter.ResetCounter();
        uiManager.SetState(UIManager.UIGeneralStates.MainMenu);
    }

    public void PlayerClickedHisSide(int _player)
    {
        switch (_player)
        {
            #region Player1Interact
            case 1:
                if (isAdding)
                {
                    activeGameCounter.AddCount(_player, player1count);
                    player1count++;
                    soundManager.PlaySound("addCounter");
                    if (player1count >= maxScore)
                    {
                        Debug.Log("Player 1 Won");
                        GameFinishedWon(_player);
                        player1count = 0;
                    }
                }
                else
                {
                    player1count--;
                    soundManager.PlaySound("minusCounter");
                    if (player1count < 0)
                    {
                        player1count = 0;
                    }
                    else
                    {
                        activeGameCounter.SubstractCount(_player, player1count);
                    }
                }
                break;
            #endregion
            #region Player2Interact
            case 2:
                if (isAdding)
                {
                    activeGameCounter.AddCount(_player, player2count);
                    player2count++;
                    soundManager.PlaySound("addCounter");

                    if (player2count >= maxScore)
                    {
                        Debug.Log("Player 2 Won");
                        GameFinishedWon(_player);
                        player2count = 0;
                    }
                }
                else
                {
                    player2count--;
                    soundManager.PlaySound("minusCounter");
                    if (player2count < 0)
                    {
                        player2count = 0;
                    }
                    else
                    {
                        activeGameCounter.SubstractCount(_player, player2count);
                    }
                }
                break;
                #endregion
        }
    }
    private void GetNameInputs()
    {
        //Another display of the unsafest code ever jaja
        nameInputs[0] = GameObject.Find("Name1Field").GetComponent<TMP_InputField>();
        nameInputs[1] = GameObject.Find("Name1Field").GetComponent<TMP_InputField>();
    }

    private void EraseNameInputs()
    {
        nameInputs[0] = null;
        nameInputs[1] = null;
    }


    public void ChangePlayerAddSubstract(bool _isIt)
    {
        isAdding = _isIt;
    }

    private void GameFinishedWon(int _player)
    {
        //Display Score and Result
        uiManager.FinishGamePanelToggle(true);
    }

    public void RestartGame()
    {
        player1count = 0;
        player2count = 0;
        activeGameCounter.ResetCounter();
        uiManager.PauseGamePanelToggle(false);
        uiManager.FinishGamePanelToggle(false);
    }

    public void StartSavingShame(int width, int height, byte[] img)
    {
        string[] names = new string[2] { nameInputs[0].text, nameInputs[1].text};
        int[] score = new int[2] { player1count, player2count };
        string date = DateTime.Today.ToString();

        string pNames = names[0] + " - " + names[1];
        string pScore = score[0].ToString() + " - " + score[1].ToString();

        shManager.AddNewShame(width, height, img, pNames, pScore, date);
    }
}
