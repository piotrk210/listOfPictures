using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    private const string FolderPath = "C:/Users/Piotrek/Desktop/png";

    [SerializeField] private GameObject container;
    [SerializeField] private Button refreshButton;
    [SerializeField] private Cell cellPrefab;
    
    private List<Cell> _cells = new List<Cell>();

    private string[] _filePathList;
    private DateTime _dateTime;
    
    private void Start()
    {
        InitList();
        refreshButton.onClick.AddListener(RefreshList);
    }

    private void InitList()
    {
        GetImagePathList();
        
        for (int i = 0; i < _filePathList.Length; i++)
        {
            var pictureCell = Instantiate(cellPrefab, container.transform);
            _cells.Add(pictureCell);
            
            pictureCell.RawImage.texture = LoadTexture(_filePathList[i]);
            pictureCell.fileNameText.text = Path.GetFileNameWithoutExtension(_filePathList[i]);
            _dateTime = Directory.GetCreationTimeUtc(_filePathList[i]).ToLocalTime();
            pictureCell.creationTimeText.text = CheckIsSingleNumber(_dateTime.Day) + "." + CheckIsSingleNumber(_dateTime.Month) + "." + _dateTime.Year + " " +
                                                CheckIsSingleNumber(_dateTime.Hour) + ":" + CheckIsSingleNumber(_dateTime.Minute) + ":" + CheckIsSingleNumber(_dateTime.Second);
            Debug.Log(_dateTime.Date);
        }
    }

    private void RefreshList()
    {
        for (int j = 0; j < _cells.Count; j++)
        {
            Destroy(_cells[j].gameObject);
        }
        _cells.Clear();
        InitList();
    }

    private void GetImagePathList()
    {
        _filePathList = Directory.GetFiles(FolderPath, "*.png");
    }

    private Texture LoadTexture(string path)
    {
        Texture2D texture = new Texture2D(40,40);
        texture.LoadImage(File.ReadAllBytes(path));
        return texture;
    }

    private string CheckIsSingleNumber(int number)
    {
        if (number < 10) return "0" + number;
        return number.ToString();
    }

}
