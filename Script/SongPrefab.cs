using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongPrefab : MonoBehaviour
{
    public string songTitle;
    public AudioClip songData;
    public Sprite songImage;
    public string songCreater;
    public int difficult;
    public TextAsset csvFile;
    public bool fontChange = false;

    void Update()
    {
        
    }
}
