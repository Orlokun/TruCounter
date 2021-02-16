﻿using System.Collections;
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
        realImage.material.mainTexture = webCamTexture;
        webCamTexture.Play();
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

        photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        photoTaken = true;
        realImage.texture = photo;
        pictureNumber++;

        byte[] bytes = photo.EncodeToPNG();
        SaveImage(bytes);
    }

    public void RestartImage()
    {
        photoTaken = false;
        Destroy(photo);
    }

    public void SaveImage(byte[] imgData)
    {
        string imgName = Application.productName + pictureNumber.ToString();
        NativeGallery.SaveImageToGallery(imgData, "truCounter", imgName + ".png", null);
        Debug.Log("Saved image named: " + imgName);
    }
}

