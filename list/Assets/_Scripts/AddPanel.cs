using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AddPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;
        [SerializeField] private Button submitButton;
        public Action OnAddPng;

    
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
            var fileStream = File.Create(catalogPath + "/"+ input.text + Constants.FileFormat);
            fileStream.Close();
            OnAddPng?.Invoke();
        }
    }
    
}
