using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct MatchData
{
    public string playerNames;
    public string matchDate;
    public string matchScore;

    public MatchData(string _pNames, string _mDate, string _mScore)
    {
        playerNames = _pNames;
        matchDate = _mDate;
        matchScore = _mScore;
    }
}

public class ShameManager : MonoBehaviour
{
    private ShameData shameData;

    [SerializeField]Transform shameScrollView;
    [SerializeField]WebCamManager wManager;
    public int shameToErase;


    void Awake()
    {
        shameData = SaveSystem.LoadShameData();
        if (shameData == null)
        {
            shameData = new ShameData(0, new List<byte[]>(), new List<string>(), new List<string>(), new List<string>());
            Debug.Log("ShameData created from none");
        }
        else if (shameData != null)
        {
            Debug.Log("Number of Shames to Load is: " + shameData.numberOfShames);
            LoadShameImages();
        }
    }

    private void LoadShameImages()
    {
        WebCamTexture wTexture = wManager.WcTexture();
        for (int i = 0; i < shameData.numberOfShames; i++)
        {
            PictureData pData = new PictureData(wTexture.width, wTexture.height, shameData.imgBytes[i]);
            MatchData mData = new MatchData(shameData.playerNames[i], shameData.dates[i], shameData.scores[i]);

            GameObject shameImageObject = CreateImageInWall(pData);
            ShameImage shameHolder = shameImageObject.GetComponent<ShameImage>();
            shameHolder.SetDataInText(mData);
        }
    }

    public void AddNewShame(PictureData _pData, MatchData mData)
    {
        shameData.AddNewShame(_pData.rawData, mData);
        GameObject shameImageObject = CreateImageInWall(_pData);
        ShameImage shameHolder = shameImageObject.GetComponent<ShameImage>();
        shameHolder.SetDataInText(mData);

        SaveSystem.FormatAndSaveShameData(this);
    }

    public void RemoveShame()
    {
        shameData.imgBytes.RemoveAt(shameToErase);
        shameData.playerNames.RemoveAt(shameToErase);
        shameData.dates.RemoveAt(shameToErase);
        shameData.scores.RemoveAt(shameToErase);

        shameData.UpdateShameData();
        GameObject shObject = shameScrollView.GetChild(shameToErase).gameObject;
        Destroy(shObject);
        SaveSystem.FormatAndSaveShameData(this);

        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.ClearShameConfirmPanelToggle(false);
    }

    private GameObject CreateImageInWall(PictureData _pData)
    {
        GameObject newImg = (GameObject)Instantiate(Resources.Load("shameImage"), shameScrollView);
        Texture2D text = new Texture2D(_pData.width, _pData.height);
        text.LoadImage(_pData.rawData);
        RawImage rImg = newImg.GetComponent<RawImage>();
        rImg.texture = text;

        return newImg;
    }

    public ShameData GetShameData()
    {
        return shameData;
    }
}
