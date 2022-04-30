using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class NotesCreater : MonoBehaviour
{
    public GameObject notesObject;
    public GameObject notesObjectLong;

    GameObject obj;

    public List<float> notesType = new List<float>();
    public List<float> notesTime = new List<float>();
    public List<float> notesTiming = new List<float>();
    public List<float> notesLane = new List<float>();

    //何個目のノーツか数える
    int notesCount = 0;

    //ノーツポジション　
    float notesPositionX;

    //総ノーツ数
    int notesNumber;

    public GameObject csvLoadObject;
    public GameObject MainScriptObject;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public float audioPlayTime;
    float audioLength;

    public List<FlowNotes> flowNotes = new List<FlowNotes>();
    private object notes;

    int phase = 0;


    // Update is called once per frame
    void Update()
    {
        if (phase == 0)
        {
            audioSource = MainScriptObject.GetComponent<MainScript>().audioSource;

            if (audioSource == null)
            {
                return;
            }
            else
            {
                audioClip = MainScriptObject.GetComponent<MainScript>().audioClip;
                audioLength = audioClip.length;

                notesNumber = csvLoadObject.GetComponent<CSVReader>().notesType.Count;
                //最初の行は不要な行なので-1する
                notesNumber -= 1;

                for (int i = 0; i <= notesNumber; i++)
                {
                    notesType.Add(0);
                    notesType[i] = csvLoadObject.GetComponent<CSVReader>().notesType[i];
                    notesTime.Add(0);
                    notesTime[i] = csvLoadObject.GetComponent<CSVReader>().notesTime[i];
                    notesTiming.Add(0);
                    notesTiming[i] = csvLoadObject.GetComponent<CSVReader>().notesTiming[i];
                    notesLane.Add(0);
                    notesLane[i] = csvLoadObject.GetComponent<CSVReader>().notesLane[i];
                }
                phase = 1;
            }
        }

        //楽曲が終了するまで処理を続ける
        audioPlayTime = audioSource.time;
        if (audioPlayTime <= audioLength)
        {


            //ノーツカウントが総数より大きくなった場合、複製処理をしない
            if (notesCount > notesNumber)
            {
                return;
            }
            //ノーツの複製タイミングが現在の楽曲再生時間より小さい場合、複製処理を行う
            else if (notesTiming[notesCount] <= audioPlayTime)
            {


                //ノーツのレーンチェック
                if (notesLane[notesCount] == 1)
                {
                    notesPositionX = -5;
                    
                }
                else if(notesLane[notesCount] == 2)
                {
                    notesPositionX = -2.5f;

                }
                else if (notesLane[notesCount] == 3)
                {
                    notesPositionX = 0;

                }
                else if (notesLane[notesCount] == 4)
                {
                    notesPositionX = 2.5f;

                }
                else if (notesLane[notesCount] == 5)
                {
                    notesPositionX = 5;

                }
                //ノーツタイプによって生成する形が違う
                if (notesType[notesCount] == 1 || notesType[notesCount] == 3)
                {
                    obj = Instantiate(notesObject, new Vector3(notesPositionX, 6.5f, 0), Quaternion.identity);
                }
                else if (notesType[notesCount] == 2)
                {
                    obj = Instantiate(notesObjectLong, new Vector3(notesPositionX, 6.5f,  0), Quaternion.identity);
                    obj.transform.localScale = new Vector3(1,notesTime[notesCount],1);
                }

                obj.GetComponent<FlowNotes>().notesType = notesType[notesCount];

                
                flowNotes.Add(obj.GetComponent<FlowNotes>());
                obj.GetComponent<FlowNotes>().notesLane = notesLane[notesCount];

                notesCount++;

            }
        
            

        }




    }
}
