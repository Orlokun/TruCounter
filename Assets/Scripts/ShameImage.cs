using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShameImage : MonoBehaviour, IPointerClickHandler
{
    ShameManager sManager;

    [SerializeField]
    TMP_Text score;
    [SerializeField]
    TMP_Text names;
    [SerializeField]
    TMP_Text dates;

    void Awake()
    {
        sManager = FindObjectOfType<ShameManager>();   
    }
    
    public void SetDataInText(MatchData mData)
    {
<<<<<<< HEAD
        score.text = mData.matchScore;
        names.text = mData.playerNames;
        dates.text = mData.matchDate;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int myIndex = transform.GetSiblingIndex();
        ShameManager shManager = FindObjectOfType<ShameManager>();
        shManager.shameToErase = myIndex;
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.ClearShameConfirmPanelToggle(true);
=======
        int myIndex = transform.GetSiblingIndex();
        sManager.RemoveShame(myIndex);
        Destroy(gameObject);
>>>>>>> f367fcff32ee6afe4e06792db0134f3ee7237185
    }
}
