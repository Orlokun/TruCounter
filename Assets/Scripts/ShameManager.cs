using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShameManager : MonoBehaviour
{
    private ShameData shameData;
    [SerializeField]
    Transform shameScrollView;
    [SerializeField]
    WebCamManager wManager;
    // Start is called before the first frame update
    void Awake()
    {
        WebCamTexture wTexture = wManager.WcTexture();

        shameData = SaveSystem.LoadShameData();
        if(shameData==null)
        {
            shameData = new ShameData(0, new List<byte[]>());
        }
        Debug.Log("Number of Shames: " + shameData.numberOfShames);
        foreach(byte[] bytes in shameData.imgBytes)
        {
            CreateImageInWall(wTexture.width, wTexture.height, bytes);
            Debug.Log("Created image: " + bytes);
        }
        
    }

    public void AddNewShame(int width, int height, byte[] shameImg)
    {
        shameData.AddNewShame(shameImg);
        CreateImageInWall(width, height, shameImg);
        SaveSystem.SaveImages(this);
    }

    private void CreateImageInWall(int width, int height, byte[] _shameImg)
    {
        GameObject newImg = (GameObject)Instantiate(Resources.Load("shameImage"), shameScrollView);
        Texture2D text = new Texture2D(width, height);
        text.LoadImage(_shameImg);
        RawImage rImg = newImg.GetComponent<RawImage>();
        rImg.texture = text;

        AdjustPadding(rImg);
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
