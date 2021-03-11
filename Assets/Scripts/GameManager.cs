using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    }

    public void Select30Match()
    {
        maxScore = 30;
        activeGameCounter = match30GameCounter;
        uiManager.SetState(UIManager.UIGeneralStates.Match30);
        player1count = 0;
        player2count = 0;
        ChangePlayerAddSubstract(true);
    }

    public void Select15Match()
    {
        maxScore = 15;
        activeGameCounter = match15GameCounter;
        uiManager.SetState(UIManager.UIGeneralStates.Match15);
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

    public void StartShameSavingProcess(PictureData _pData)
    {
        string matchCount = player1count.ToString() + " - " + player2count.ToString();
        MatchData mData = new MatchData(uiManager.GetPlayerNames(), GetDate(), matchCount);
        shManager.AddNewShame(_pData, mData);
    }

    private string GetDate()
    {
        DateTime date = DateTime.Now;
        return date.ToString();
    }
}
