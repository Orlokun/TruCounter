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
    [SerializeField]
    Transform shameScrollView;
    [SerializeField]
    WebCamManager wManager;

    public int shameToErase;
    // Start is called before the first frame update
    void Awake()
    {
        WebCamTexture wTexture = wManager.WcTexture();

        shameData = SaveSystem.LoadShameData();
        if (shameData == null)
        {
            shameData = new ShameData(0, new List<byte[]>(), new List<string>(), new List<string>(), new List<string>());
            Debug.Log("ShameData created from none");
        }
        Debug.Log("Number of Shames: " + shameData.numberOfShames);

        foreach (byte[] bytes in shameData.imgBytes)
        {
            PictureData pData = new PictureData(wTexture.width, wTexture.height, bytes);
            CreateImageInWall(pData);
            Debug.Log("Created image: " + bytes);
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

    private void AdjustPadding(RawImage _rImg)
    {
        /*GridLayoutGroup grid = shameScrollView.gameObject.GetComponent<GridLayoutGroup>();
        grid.cellSize = _rImg.GetComponent<RectTransform>().sizeDelta;*/
    }

    public ShameData GetShameData()
    {
        return shameData;
    }
}
