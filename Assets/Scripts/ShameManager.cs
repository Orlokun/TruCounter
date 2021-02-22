using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShameManager : MonoBehaviour
{
    WebCamTexture wTexture;
    private ShameDataManager shameData;
    [SerializeField]
    Transform shameScrollView;
    [SerializeField]
    WebCamManager wManager;
    // Start is called before the first frame update
    void Awake()
    {
        wTexture = wManager.WebCamTexture();
        shameData = SaveSystem.LoadShameData();
        if (shameData == null)
        {
            shameData = new ShameDataManager(0, new List<byte[]>(), new List<string>(), new List<string> (), new List<string>());
        }

        if(shameData.numberOfShames > 0)
        {
            InstantiateAllShames();
        }
    }

    private void InstantiateAllShames()
    {
        for (int i=0;i<shameData.numberOfShames;i++)
        {
            GameObject _shObj = (GameObject)Instantiate(Resources.Load("shameImage"), shameScrollView);
            SetImageInTexture(_shObj, wTexture.width, wTexture.height, shameData.shameImgBytes[i]);
        }
    }
    
    private void InstantiateNewShame(int width, int height, byte[] shameImg)
    {
        GameObject _shObj = (GameObject)Instantiate(Resources.Load("shameImage"), shameScrollView);
        SetImageInTexture(_shObj, wTexture.width, wTexture.height, shameImg);
        //SetNamesInTextBox
        //SetDateInTextBox
        //SetScoreInTextBox
    }

    public void AddNewShame(int width, int height, byte[] shameImg, string names, string score, string date)
    {
        shameData.AddNewShameToData(shameImg, names, score, date);
        InstantiateNewShame(width, height, shameImg);
        SaveSystem.SaveImages(shameData);
    }

    public void RemoveShame(int myIndex)
    {
        shameData.shameImgBytes.RemoveAt(myIndex);
        shameData.shameNames.RemoveAt(myIndex);
        shameData.shameScores.RemoveAt(myIndex);
        shameData.shameDates.RemoveAt(myIndex);

        shameData.UpdateShameData();
        SaveSystem.SaveImages(shameData);
    }

    private void SetImageInTexture(GameObject _obj, int width, int height, byte[] _shameImg)
    {
        Texture2D _shameImgTexture = new Texture2D(width, height);
        _shameImgTexture.LoadImage(_shameImg);
        RawImage rImg = _obj.GetComponent<RawImage>();
        rImg.texture = _shameImgTexture;
    }

    public ShameDataManager GetShameData()
    {
        return shameData;
    }
}
