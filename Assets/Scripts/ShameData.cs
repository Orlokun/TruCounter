using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShameData
{
    public int numberOfShames;
    public List<byte[]> imgBytes;

    public ShameData(int _shames, List<byte[]> _bytes)
    {
        numberOfShames = _shames;
        imgBytes = _bytes;
    }

    public void AddNewShame(byte[] _shameImg)
    {
        imgBytes.Add(_shameImg);
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
