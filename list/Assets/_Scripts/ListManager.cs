using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    private string _folderPath = "C:/";
    //private string _folderPath = "C:/Users/Piotrek/Desktop/png";

    [SerializeField] private GameObject container;
    [SerializeField] private Button refreshButton;
    [SerializeField] private Button addPngButton;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private TextMeshProUGUI noItemsText;
    
    private List<Cell> _cells = new List<Cell>();

    private string[] _filePathList;
    private DateTime _dateTime;
    
    private void Start()
    {
        InitList();
        refreshButton.onClick.AddListener(RefreshList);
        addPngButton.onClick.AddListener(ShowAddingPngPanel);
        input.onEndEdit.AddListener(GetPathFromInput);
    }

    private void OnDisable()
    {
        input.onEndEdit.RemoveAllListeners();
        refreshButton.onClick.RemoveAllListeners();
        addPngButton.onClick.RemoveAllListeners();
    }

    private void GetPathFromInput(string path)
    {
        Debug.Log(path);
        //convert /
        // path.Replace("\\", "/");
        // Debug.Log(path);
        _folderPath = path;
        RefreshList();
    }

    private void ShowAddingPngPanel()
    {
        
    }

    private void InitList()
    {
        GetImagePathList();

        noItemsText.gameObject.SetActive(_filePathList.Length == 0);

        for (int i = 0; i < _filePathList.Length; i++)
        {
            var pictureCell = Instantiate(cellPrefab, container.transform);
            _cells.Add(pictureCell);
            pictureCell.UpdateCell(_filePathList[i]);
        }
    }

    private void RefreshList()
    {
        ResetList();
        InitList();
    }

    private void GetImagePathList()
    {
        try
        {
            _filePathList = Directory.GetFiles(_folderPath, "*.png");
        }
        catch (Exception e)
        {
            ResetList();
            Debug.Log(e.Message);
            //cant find catalog
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
    
}
