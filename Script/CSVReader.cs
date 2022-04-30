using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CSVReader : MonoBehaviour
{
    public GameObject mainScript;
    public string loadCSV;

    public List<float> notesType = new List<float>();
    public List<float> notesTime = new List<float>();
    public List<float> notesTiming = new List<float>();
    public List<float> notesLane = new List<float>();

    TextAsset csvFile; // CSVファイル
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;



    void Start()
    {
        /*
        csvFile = Resources.Load(loadCSV) as TextAsset; // Resouces下のCSV読み込み
        */

        csvFile = MainScript.csvFile;

        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }

        //TODO:エラー箇所　↓
        for ( int i = 1; i < csvDatas.Count; i++ )
        {
            Debug.Log(i + "start");
            notesType.Add(0.0f);
            notesType[i - 1] = float.Parse(csvDatas[i][0]);
            notesTime.Add(0.0f);
            notesTime[i - 1] = float.Parse(csvDatas[i][1]);
            notesTiming.Add(0.0f);
            notesTiming[i - 1] = float.Parse(csvDatas[i][2]);
            notesLane.Add(0.0f);
            notesLane[i - 1] = float.Parse(csvDatas[i][3]);
            Debug.Log(i);
        }



    }



}