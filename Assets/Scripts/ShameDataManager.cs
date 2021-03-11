using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShameDataManager
{
    public int numberOfShames;
    public List<byte[]> shameImgBytes;
    public List<string> shameNames;
    public List<string> shameScores;
    public List<string> shameDates;

    public ShameDataManager(int _shames, List<byte[]> _bytes, List<string>_names, List<string> _scores, List<string> _dates)
    {
        numberOfShames = _shames;
        shameImgBytes = _bytes;
        shameNames = _names;
        shameScores = _scores;
        shameDates = _dates;
    }

    public void AddNewShameToData(byte[] _shameImg, string names, string score, string date)
    {
        shameImgBytes.Add(_shameImg);
        shameNames.Add(names);
        shameScores.Add(score);
        shameDates.Add(date);
        if (shameImgBytes.Count!= shameNames.Count|| shameImgBytes.Count != shameScores.Count || shameImgBytes.Count != shameDates.Count)
        {
            Debug.LogError("The length of each shame column in not the same");
        }
        numberOfShames = shameImgBytes.Count;
    }

    public void UpdateShameData()
    {
        numberOfShames = shameImgBytes.Count;
    }

    public int NumberOfShames{get{return numberOfShames;}set{numberOfShames = value;}}
}
