using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button submitButton;
    public Action OnAddPng;
    private string fileFormat = ".png";
    
    private string catalogPath;

    public void InitPanel()
    {
        submitButton.onClick.AddListener(Submit);
    }

    public void DeinitPanel()
    {
        submitButton.onClick.RemoveAllListeners();
    }

    public void LoadCatalogPath(string path)
    {
        catalogPath = path;
    }
    

    private void Submit()
    {
        File.Create(catalogPath + "/"+ input.text + fileFormat);
        OnAddPng?.Invoke();
    }
}
