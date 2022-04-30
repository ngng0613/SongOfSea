using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GETKEY : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite pressSprite;

    public int check = 0;

    public float key;
    public bool pressKeyNow;

    
    public KeyCode actionKey;
    public float thisLane;
    KeyCode pressKey;

    public bool pressKeyLong;
    public bool pressKeyRelease = false;

    //mainScript関係
    public GameObject mainObj;

    //Button関係の取得用変数
    public float buttonRadius;
    public Vector3 buttonPos;

    //判定距離
    const float judgeSpace = 0.4f;

    //notes関係の取得用変数
    FlowNotes notes;
    public int notesType = 0;
    public float notesRadius;
    public Vector3 notesPos;
    public Vector3 notesSize;
    public float notes2X;
    public float notes2Y;
    public Color notesColor;

    public bool flowStop = false;

    //SE関係の変数
    public GameObject seObject;

    //NICEとかの文字表示の変数

    public GameObject textPrehab;
    
    //mainScriptのobject

    //notesとbuttonの距離
    public float distance;

    //判定
    public string scoreStr;
    public int scorePoint;

    public NotesCreater notesCreater = null;
    public Transform canvasTransform = null;

    public List<TextEffect> textList = new List<TextEffect>();

    int countSecond = 0;

    //コンボ数
    public static int combo = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        // 何かのキーが押下されたら
        if (Input.GetKey(actionKey))
        {
            pressKeyNow = true;
            
        }
        else
        {
            pressKeyNow = false;
            //離した瞬間を判定
            if (pressKeyLong)
            {
                //キーを離した瞬間の判定に猶予を与える
                if (countSecond < 6)
                {
                    countSecond++;
                    pressKeyRelease = true;
                }
                else 
                {
                    countSecond = 0;
                    pressKeyLong = false;
                    pressKeyRelease = false;
                    flowStop = false;
                }



            }

            
        }
        
        if (pressKeyNow)
        {
            if (Input.GetKeyDown(actionKey))
            {
                Instantiate(seObject);
            }

            //色を暗くする場合はこれ↓
            //button.GetComponent<SpriteRenderer>().color = new Color32(100, 100, 100, 255);

            //画像差し替えはこれ↓
            GetComponent<SpriteRenderer>().sprite = pressSprite;

            check = 1;

            pressKeyLong = true;

            UpdatePressKey();


        }
        else
        {
            //button.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            GetComponent<SpriteRenderer>().sprite = normalSprite;
            check = 0;

            if (pressKeyRelease)
            {
                UpdatePressKey();
            }
        }

        


    }
    void UpdatePressKey()
    {
        
        //以下処理で一番先に生成されたノーツ、かつ,このキーのレーンと等しいものを指定したりする
        for (int i = 0; i < notesCreater.flowNotes.Count; i++)
        {

            notes = notesCreater.flowNotes[i];
            
            if (notesType == 2)
            {
                GameObject temp = notes.transform.GetChild(0).gameObject;
                notes = temp.GetComponent<FlowNotes>();
            }
            

            if (notes.notesLane != thisLane)
            {
                continue;
            }


            notesRadius = notes.GetComponent<SpriteRenderer>().bounds.size.y;
            notesRadius /= 2;

            //buttonの半径を調べる
            buttonRadius = GetComponent<SpriteRenderer>().bounds.size.y;
            buttonRadius /= 2;

            //Notesとbuttonの距離を調べる
            notesPos = notes.GetComponent<Transform>().position;
            buttonPos = GetComponent<Transform>().position;

            //notesからbuttonの距離とbuttonの半径との誤差が
            //notesの半径以内の場合

            distance = notesPos.y - buttonPos.y;

            if (distance - notesRadius <= judgeSpace)
            {
                UpdateTouchJudge(i);

                check = 2;

            }
        }
    }
    void UpdateTouchJudge(int i)
    {

        if (notes.notesType == 2)
        {

            if (!pressKeyLong)
            {
                //長押しノーツ全消滅
                notes.longDestroy = true;
            }
        }


        if (notes.notesType == 3)
        {
            if (pressKeyRelease == false)
            {
                return;
            }

        }
        



        notes.flowStop = true;
        if (transform.position.x == notesPos.x)
        {
            //判定処理
            if (distance >= 0.5|| distance <= -0.4f)
            {
                scoreStr = "BAD…";
                scorePoint = 50;
                MainScript.badTimes++;
                combo = 0;
            }
            else if (distance >= 0.35 || distance < -0.35f)
            {
                scoreStr = "GOOD!";
                scorePoint = 100;
                MainScript.goodTimes++;
                combo++;
            }
            else if (distance >= 0.2 || distance < -0.2f)
            {
                scoreStr = "GREAT!!";
                scorePoint = 150;
                MainScript.greatTimes++;
                combo++;
            }
            else
            {
                scoreStr = "PERFECT!!";
                scorePoint = 200;
                MainScript.perfectTimes++;
                combo++;
            }
            MainScript.score += scorePoint;
            

            if (notes.notesType != 2)
            {
                Vector3 textCreatePosition = GetComponent<GETKEY>().transform.position;

                GameObject obj = Instantiate(textPrehab, canvasTransform);

                TextEffect textEffect = obj.GetComponent<TextEffect>();
                RectTransform textRectTransform = obj.GetComponent<RectTransform>();

                textRectTransform.position = new Vector3(textCreatePosition.x, -3.17f, 0.0f);
                textList.Add(textEffect);
                textEffect.DataInput(scoreStr, gameObject);
            }


            notesCreater.flowNotes.RemoveAt(i);


        }
        



    }

}
