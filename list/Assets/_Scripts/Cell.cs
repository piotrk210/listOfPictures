using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fileNameText;
    [SerializeField] private TextMeshProUGUI creationTimeText;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private Texture2D texturePlaceholder;

    private int textureWidth = 40;
    private int textureHight = 40;
    
    private DateTime _dateTime;

    public void UpdateCell(string path)
    {
        rawImage.texture = LoadTexture(path);
        fileNameText.text = Path.GetFileNameWithoutExtension(path);
        _dateTime = Directory.GetCreationTimeUtc(path).ToLocalTime();
        creationTimeText.text = CheckIsSingleNumber(_dateTime.Day) + "." + CheckIsSingleNumber(_dateTime.Month) + "." + _dateTime.Year + " " +
                                            CheckIsSingleNumber(_dateTime.Hour) + ":" + CheckIsSingleNumber(_dateTime.Minute) + ":" + CheckIsSingleNumber(_dateTime.Second);
    }
    
    
    private Texture LoadTexture(string path)
    {
        Texture2D texture = new Texture2D(textureWidth,textureHight);
        try
        {
            texture.LoadImage(File.ReadAllBytes(path));
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            texture = texturePlaceholder;
        }
        return texture;
    }

    private string CheckIsSingleNumber(int number)
    {
        if (number < 10) return "0" + number;
        return number.ToString();
    }

}
