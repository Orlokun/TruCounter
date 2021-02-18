using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class WebCamManager : MonoBehaviour
{
    //TODO: Delete
    int pictureNumber = 0;

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
        pictureNumber++;

        byte[] bytes = photo.EncodeToPNG();
        SaveImage(width, height, bytes);
    }

    public void RestartImage()
    {
        photoTaken = false;
        Destroy(photo);
    }

    public void SaveImage(int width, int height, byte[] imgData)
    {
        GameManager gManager = FindObjectOfType<GameManager>();
        gManager.StartImageSaving(width, height, imgData);
        
        
        /*          NATIVE SAVING
        string imgName = Application.productName + pictureNumber.ToString();
        NativeGallery.SaveImageToGallery(imgData, "truCounter", imgName + ".png", null);
        Debug.Log("Saved image named: " + imgName);*/
    }
}

