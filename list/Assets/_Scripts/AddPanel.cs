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
        public Action<string> OnAddPng;

    
        private string catalogPath;
        private bool isPathCorretc;

        public void InitPanel()
        {
            submitButton.onClick.AddListener(Submit);
        }

        public void DeinitPanel()
        {
            submitButton.onClick.RemoveAllListeners();
        }

        public void LoadCatalogPath(string path, bool isPathCorretc)
        {
            catalogPath = path;
            this.isPathCorretc = isPathCorretc;
        }
    

        private void Submit()
        {
            if(isPathCorretc && input.text != "")
            {
                var fileStream = File.Create(catalogPath + "/"+ input.text + Constants.FileFormat);
                fileStream.Close();
                OnAddPng?.Invoke(fileStream.Name);
            }
            gameObject.SetActive(false);
            
        }
    }
    
}
