using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShameManager : MonoBehaviour
{
    private ShameData shameData;
    // Start is called before the first frame update
    void Awake()
    {
        shameData = SaveSystem.LoadShameData();
        if(shameData==null)
        {
            shameData = new ShameData(0, new List<byte[]>());
        }
    }

    public void AddNewShame(byte[] shameImg)
    {
        shameData.AddNewShame(shameImg);
        CreateImageInWall(shameImg);
    }

    private void CreateImageInWall(byte[] _shameImg)
    {
        //TODO Instantiate image
        //TODO Set image as child of Content
        //TODO Assign ImageBytes to RawImageComponent
    }



    public ShameData GetShameData()
    {
        return shameData;
    }
}
