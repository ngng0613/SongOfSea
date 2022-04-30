using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowNotes : MonoBehaviour
{
    public int gamePhase;
    public bool flowStop = false;
    public bool longDestroy = false;

    //ノーツのスピード
    const int notesSpeed = 8;

    //notes関係の変数
    public float notesType;

    public float notesRadius;
    public Vector3 notesPos;
    public Vector3 notesSize;
    public float notes2X;
    public float notes2Y;
    public Color notesColor;
    public float notesLane;
    public Vector3 notesLongScale;

    GameObject mainObj;
    GameObject notesCreate;

    private void Start()
    {
        notesCreate = GameObject.Find("NotesCreate");
    }


    // Update is called once per frame
    void Update()
    {
        mainObj = GameObject.Find("MainScript");
        gamePhase = mainObj.GetComponent<MainScript>().gamePhase;

        if (gamePhase < 1)
        {
            return;

        }

        if (longDestroy)
        {
            Destroy(gameObject);
        }


        if (flowStop == false)
        {
            Vector3 notesPos = gameObject.transform.position;
            
            if (notesPos.y <= -6)
            {
                //画面外にでた場合、消滅する
                GETKEY.combo = 0;

                notesCreate.GetComponent<NotesCreater>().flowNotes.RemoveAt(0);
                Destroy(gameObject);
                
            }
            
            
            notesPos.y -= notesSpeed * Time.deltaTime;
            gameObject.transform.position = notesPos;

            
        }
        else if (flowStop)
        {

            if (notesType != 2)
            {
                notesSize = GetComponent<Transform>().localScale;
                notesColor = GetComponent<SpriteRenderer>().color;

                if (notesColor.a <= 0)
                {
                    //オブジェクト削除
                    Destroy(gameObject);

                }

                notesColor.a -= 5f * Time.deltaTime;
                notesSize *= 1.02f;


                GetComponent<SpriteRenderer>().color = notesColor;
                GetComponent<Transform>().localScale = notesSize;
            }
            else if (notesType == 2)
            {
                notesLongScale = gameObject.GetComponent<Transform>().localScale;
                if (notesLongScale.y > 0)
                {
                    notesLongScale.y -= notesSpeed * 60 * Time.deltaTime;
                    gameObject.GetComponent<Transform>().localScale = notesLongScale;

                }
                else
                {
                    //Destroy(gameObject);
                }

            }


        }








    }
}
