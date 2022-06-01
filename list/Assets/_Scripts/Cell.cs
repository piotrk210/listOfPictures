using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fileNameText;
        [SerializeField] private TextMeshProUGUI creationTimeText;
        [SerializeField] private RawImage rawImage;
        [SerializeField] private Texture2D texturePlaceholder;

        private int textureWidth = 40;
        private int textureHight = 40;

        private DateTime creationTime;
        private TimeSpan timeSpan;
        public string currentPath = "";


        public void UpdateCell(string path)
        {
            if (string.Equals(currentPath, path) &&
                DateTime.Equals(creationTime, Directory.GetCreationTimeUtc(path).ToLocalTime())) return;

            currentPath = path;
            fileNameText.text = Path.GetFileNameWithoutExtension(path);
            creationTime = Directory.GetCreationTimeUtc(path);
            timeSpan = DateTime.Now.Subtract(creationTime);
            creationTimeText.text = timeSpan.ToString();
        }


        public void LoadTexture(string path)
        {
            if (string.Equals(currentPath, path) &&
                DateTime.Equals(creationTime, Directory.GetCreationTimeUtc(path).ToLocalTime())) return;


            Texture2D texture = new Texture2D(textureWidth, textureHight);
            try
            {
                texture.LoadImage(File.ReadAllBytes(path));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                texture = texturePlaceholder;
            }

            rawImage.texture = texture;
        }
    }
}