using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    private const string NoPngInFile = "No png file in folder";
    private const string CantFindCatalog = "Cant find catalog";
    
    private static string _folderPath = "C:/";
    //private string _folderPath = "C:/Users/Piotrek/Desktop/png";

    [SerializeField] private GameObject cellContainer;
    [SerializeField] private Button refreshButton;
    [SerializeField] private Button addPngButton;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private AddPanel addPngPanel;
    private  bool isPathCorrect;
    
    private List<Cell> _cells = new List<Cell>();

    private List<string> _filePathList;
    private DateTime _dateTime;
    
    private void Start()
    {
        InitList();
        addPngPanel.InitPanel();
        refreshButton.onClick.AddListener(RefreshList);
        addPngButton.onClick.AddListener(EnableAddingPngPanel);
        input.onEndEdit.AddListener(GetPathFromInput);
        addPngPanel.OnAddPng += RefreshList;
        addPngPanel.OnAddPng += DisableAddingPngPanel;
    }

    private void OnDisable()
    {
        addPngPanel.DeinitPanel();
        input.onEndEdit.RemoveAllListeners();
        refreshButton.onClick.RemoveAllListeners();
        addPngButton.onClick.RemoveAllListeners();
        addPngPanel.OnAddPng -= RefreshList;
        addPngPanel.OnAddPng -= DisableAddingPngPanel;
    }

    private void GetPathFromInput(string path)
    {
        //Debug.Log(path);
        _folderPath = path;
        RefreshList();
    }

    private void EnableAddingPngPanel()
    {
        if(addPngButton.isActiveAndEnabled) addPngPanel.gameObject.SetActive(true);
        addPngPanel.LoadCatalogPath(_folderPath);
    }

    private void DisableAddingPngPanel()
    {
        addPngPanel.gameObject.SetActive(false);
    }
    
    
    private void RefreshList()
    {
        ResetList();
        InitList();
    }

    private void InitList()
    {
        GetImagePathList();

        if(_filePathList.Count == 0 && isPathCorrect) EnablePopup(NoPngInFile);

        for (int i = 0; i < _filePathList.Count; i++)
        {
            var pictureCell = Instantiate(cellPrefab, cellContainer.transform);
            _cells.Add(pictureCell);
            pictureCell.UpdateCell(_filePathList[i]);
        }
    }
    
    private void GetImagePathList()
    {
        try
        {
            _filePathList = Directory.GetFiles(_folderPath, "*.png").ToList();
            DisablePopup();
            isPathCorrect = true;
        }
        catch (Exception e)
        {
            _filePathList.Clear();
            EnablePopup(CantFindCatalog);
            ResetList();
            isPathCorrect = false;
            Debug.Log(e.Message);
        }
    }
    private void ResetList()
    {
        for (int j = 0; j < _cells.Count; j++)
        {
            Destroy(_cells[j].gameObject);
        }
        _cells.Clear();
    }

    private void EnablePopup(string messeg)
    {
        popupText.text = messeg;
        popupText.gameObject.SetActive(true);
    }

    private void DisablePopup()
    {
        popupText.gameObject.SetActive(false);
    }
}
