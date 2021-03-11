using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public struct PictureData
{
    public int width;
    public int height;
    public byte[] rawData;

    public PictureData(int _width, int _height, byte[] _bytes)
    {
        width = _width;
        height = _height;
        rawData = _bytes;
    }
}

public class WebCamManager : MonoBehaviour
{

    WebCamTexture webCamTexture;
    Texture2D photo;
    bool photoTaken;
    private Quaternion baseRotation;

    [SerializeField]
    GameObject imageObject;
    RawImage realImage;

    private void Awake()
    {
        realImage = imageObject.GetComponent<RawImage>();
    }
    void Start()
    {
        photoTaken = false;
        SelectCamera();
        realImage.material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    private void SelectCamera()
    {
        string selectedDeviceName = "";
        WebCamDevice[] allDevices = WebCamTexture.devices;
        for (int i = 0; i < allDevices.Length; i++)
        {
            if (allDevices[i].isFrontFacing == false)
            {
                selectedDeviceName = allDevices[i].name;
                break;
            }
        }
        webCamTexture = new WebCamTexture(selectedDeviceName);
    }

    public WebCamTexture WcTexture()
    {
        SelectCamera();
        return webCamTexture;
    }

    private void Update()
    {
        if (photoTaken == false)
        {
            realImage.texture = webCamTexture;
        }
    }

    public void TakePhotoStart()
    {
        StartCoroutine(TakePhoto());
    }

    IEnumerator TakePhoto()  // Start this Coroutine on some button click
    {
        yield return new WaitForEndOfFrame();

        int width = webCamTexture.width;
        int height = webCamTexture.height;

        photo = new Texture2D(width, height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        photoTaken = true;
        realImage.texture = photo;

        byte[] bytes = photo.EncodeToPNG();
        PictureData pData = new PictureData(width, height, bytes);
        SaveImage(pData);

        StartCoroutine(WaitForClosingPopUp());
    }

    private IEnumerator WaitForClosingPopUp()
    {
        yield return new WaitForSeconds(1.3f);
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.TakePicturePanelToggle(false);
    }

    public void RestartImage()
    {
        photoTaken = false;
        Destroy(photo);
    }

    public void SaveImage(PictureData _pData)
    {
        GameManager gManager = FindObjectOfType<GameManager>();
        gManager.StartShameSavingProcess(_pData);
        
        
        /*          NATIVE SAVING
        string imgName = Application.productName + pictureNumber.ToString();
        NativeGallery.SaveImageToGallery(imgData, "truCounter", imgName + ".png", null);
        Debug.Log("Saved image named: " + imgName);*/
    }
}

