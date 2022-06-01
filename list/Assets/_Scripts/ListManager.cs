using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ListManager : MonoBehaviour
    {
        private string _folderPath = "C:/";
    
        [SerializeField] private GameObject cellContainer;
        [SerializeField] private Button refreshButton;
        [SerializeField] private Button addPngButton;
        [SerializeField] private TMP_InputField input;
        [SerializeField] private Cell cellPrefab;
        [SerializeField] private TextMeshProUGUI popupText;
        [SerializeField] private AddPanel addPngPanel;
        
        private bool isPathCorrect;
        
        private List<Cell> _cells = new List<Cell>();
        private List<string> _filePathList = new List<string>();
        
        public int startingCellNumber = 20;
        
        private void Start()
        {
            UpdateList();
            addPngPanel.InitPanel();
            InstantiateCells(startingCellNumber);
            refreshButton.onClick.AddListener(RefreshList);
            addPngButton.onClick.AddListener(EnableAddingPngPanel);
            input.onEndEdit.AddListener(GetPathFromInput);
            addPngPanel.OnAddPng += AddNewPngToList;
        }
    
        private void OnDisable()
        {
            addPngPanel.DeinitPanel();
            input.onEndEdit.RemoveAllListeners();
            refreshButton.onClick.RemoveAllListeners();
            addPngButton.onClick.RemoveAllListeners();
            addPngPanel.OnAddPng -= AddNewPngToList;
        }
    
        private void GetPathFromInput(string path)
        {
            if(string.Equals(_folderPath, path)) return;
            _folderPath = path;
            RefreshList();
        }
    
        private void EnableAddingPngPanel()
        {
            if(addPngButton.isActiveAndEnabled) addPngPanel.gameObject.SetActive(true);
            addPngPanel.LoadCatalogPath(_folderPath, isPathCorrect);
        }

        private void RefreshList()
        {
            ResetList();
            UpdateList();
        }
        
        private void AddNewPngToList(string pathOfNewPng)
        {
            _filePathList.Add(pathOfNewPng);
            UpdateList();
        }
    
        private void UpdateList()
        {
            GetImagePathList();
    
            if(_filePathList.Count == 0 && isPathCorrect) EnablePopup(Constants.NoPngInFile);
    
            int newCellsToInstantiate = (_filePathList.Count > _cells.Count) ? _filePathList.Count - _cells.Count : 0;
            if(newCellsToInstantiate > 0) InstantiateCells(newCellsToInstantiate);


            StartCoroutine(LoadCell());

        }

        private IEnumerator LoadCell()
        {
            for (int i = 0; i < _filePathList.Count; i++)
            {
                _cells[i].gameObject.SetActive(true);
                _cells[i].UpdateCell(_filePathList[i]);
                _cells[i].LoadTexture(_filePathList[i]);
                yield return null;
            }
        }
        
        private void GetImagePathList()
        {
            try
            {
                _filePathList = Directory.GetFiles(_folderPath, Constants.SearchPattern).ToList();
                DisablePopup();
                isPathCorrect = true;
            }
            catch (Exception e)
            {
                _filePathList.Clear();
                EnablePopup(Constants.CantFindCatalog);
                ResetList();
                isPathCorrect = false;
                Debug.Log(e.Message);
            }
        }
    
        private void ResetList()
        {
            foreach (var cell in _cells)
            {
                cell.gameObject.SetActive(false);;
            }
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

        private void InstantiateCells(int numberToInstantiate)
        {
            for (int i = 0; i < numberToInstantiate; i++)
            {
                var pictureCell = Instantiate(cellPrefab, cellContainer.transform);
                pictureCell.gameObject.SetActive(false);
                _cells.Add(pictureCell);
            }
        }
    }
}
