using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShameData
{
    public int numberOfShames;

    public List<byte[]> imgBytes;
    public List<string> scores;
    public List<string> playerNames;
    public List<string> dates;

    public ShameData(int _shames, List<byte[]> _bytes, List<string> _scores, List<string> _playerNames, List<string> _dates)
    {
        numberOfShames = _shames;
        imgBytes = _bytes;
        scores = _scores;
        playerNames = _playerNames;
        dates = _dates;
    }

    public void AddNewShame(byte[] _shameImg, MatchData mData)
    {
        imgBytes.Add(_shameImg);
        scores.Add(mData.matchScore);
        playerNames.Add(mData.playerNames);
        dates.Add(mData.matchDate);
        numberOfShames = imgBytes.Count;
    }

    public void UpdateShameData()
    {
        numberOfShames = imgBytes.Count;
    }

    public int NumberOfShames
    {   
        get
        {
            return numberOfShames;
        }
        set
        {
            numberOfShames = value;
        }
    }
}
