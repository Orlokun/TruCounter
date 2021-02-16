using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMatchUIObject : MonoBehaviour
{
    public GameObject[] P1Matches;
    public GameObject[] P2Matches;

    public Text p1Name;
    public Text p2Name;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gObject in P1Matches)
        {
            gObject.SetActive(false);
        }

        foreach (GameObject gObject in P2Matches)
        {
            gObject.SetActive(false);
        }
    }

    public void AddCount(int player, int index)
    {
        switch(player)
        {
            case 1:
                P1Matches[index].SetActive(true);
                break;
            case 2:
                P2Matches[index].SetActive(true);
                break;
        }
    }

    public void SubstractCount(int player, int index)
    {
        switch (player)
        {
            case 1:
                P1Matches[index].SetActive(false);
                break;
            case 2:
                P2Matches[index].SetActive(false);
                break;
        }
    }

    public void ResetCounter()
    {
        foreach (GameObject gObj in P1Matches)
        {
            gObj.SetActive(false);
        }
        foreach (GameObject gObj in P2Matches)
        {
            gObj.SetActive(false);
        }
    }
}
