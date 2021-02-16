using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerInputController : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
{
    [SerializeField]
    public int player;
    GameManager gManager;

    // Start is called before the first frame update
    void Start()
    {
        gManager = FindObjectOfType<GameManager>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("ME TOCASTE!!!");
        gManager.PlayerClickedHisSide(player);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

}
