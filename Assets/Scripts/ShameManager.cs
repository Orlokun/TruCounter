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
    WebCamTexture wTexture;
    private ShameDataManager shameData;
    [SerializeField]
    Transform shameScrollView;
    [SerializeField]
    WebCamManager wManager;

    public int shameToErase;
    // Start is called before the first frame update
    void Awake()
    {
        wTexture = wManager.WebCamTexture();
        shameData = SaveSystem.LoadShameData();
        if (shameData == null)
        {
<<<<<<< HEAD
            shameData = new ShameData(0, new List<byte[]>(), new List<string>(), new List<string>(), new List<string>());
            Debug.Log("ShameData created from none");
=======
            shameData = new ShameDataManager(0, new List<byte[]>(), new List<string>(), new List<string> (), new List<string>());
>>>>>>> f367fcff32ee6afe4e06792db0134f3ee7237185
        }

        if(shameData.numberOfShames > 0)
        {
<<<<<<< HEAD
            PictureData pData = new PictureData(wTexture.width, wTexture.height, bytes);
            CreateImageInWall(pData);
            Debug.Log("Created image: " + bytes);
=======
            InstantiateAllShames();
>>>>>>> f367fcff32ee6afe4e06792db0134f3ee7237185
        }
    }

<<<<<<< HEAD
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
=======
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
>>>>>>> f367fcff32ee6afe4e06792db0134f3ee7237185
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
