using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public static int score = 0;
    public static int combo = 0;
    public static int maxCombo = 0;
    public static TextAsset csvFile;


    public int gamePhase = 0;
    public string songTitle;
    public float counter = 0;
    public bool musicStart = false;



    public GameObject titleObj;
    public GameObject titleCanvasObj;
    public float titleAlpha;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject bgmObject;
    public GameObject scoreObject;
    public GameObject comboObject;
    public GameObject sceneChange;

    [SerializeField] int secondsCount = 0;

    //引継ぎ用変数

    public static int resultScore = 0;
    public static int perfectTimes = 0;
    public static int greatTimes = 0;
    public static int goodTimes = 0;
    public static int badTimes = 0;


    //以下デバッグ用
    float nowSecond = 0;
    public GameObject debugText;

    // Start is called before the first frame update
    void Start()
    {
        /*
        audioSource = bgmObject.GetComponent<AudioSource>();
        audioClip = bgmObject.GetComponent<AudioSource>().clip;
        */
        bgmObject.GetComponent<AudioSource>().clip = SongChoice.musicData;
        audioClip = bgmObject.GetComponent<AudioSource>().clip;
        audioSource = bgmObject.GetComponent<AudioSource>();
        songTitle = SongChoice.musicTitle;
        titleObj = GameObject.Find("TitleText");
        titleObj.GetComponent<Text>().text = songTitle;

    }

    // Update is called once per frame
    void Update()
    {
        combo = GETKEY.combo;

        if (gamePhase == 0)
        {
            score = 0;
            combo = 0;
            maxCombo = 0;
            if (secondsCount < 180)
            {
                secondsCount++;
            }
            else
            {
                Color color = sceneChange.GetComponent<Image>().color;
                color.a -= 1 * Time.deltaTime;
                if (color.a <= 0)
                {
                    //ゲーム前演出
                    //タイトル表記など

                    StartEffect();
                }
                else
                {
                    sceneChange.GetComponent<Image>().color = color;
                }
            }

        }
        else if (gamePhase == 1)
        {
            secondsCount = 0;
            //BGMの再生開始
            BGMStart();
        }
        else if (gamePhase == 2)
        {
            //スコアの更新
            string scoreString = "Score:" + score;
            string comboString = "Combo:" + combo;
            if (combo > maxCombo)
            {
                maxCombo = combo;
            }

            nowSecond += 1.0f * Time.deltaTime;
            debugText.GetComponent<Text>().text = nowSecond + "/秒数";

            scoreObject.GetComponent<Text>().text = scoreString;
            comboObject.GetComponent<Text>().text = comboString;

            if (audioSource.time >= bgmObject.GetComponent<AudioSource>().clip.length)
            {
                gamePhase = 3;
            }
        }
        else if (gamePhase == 3)
        {
            //デバッグ用秒数表示



            secondsCount++;

            if (secondsCount >= 30)
            {
                Color color = sceneChange.GetComponent<Image>().color;
                color.a += 1 * Time.deltaTime;
                sceneChange.GetComponent<Image>().color = color;
                if (color.a >= 1)
                {
                    SceneManager.LoadScene("result");
                }

            }

        }

    

    }
    void StartEffect()
    {
        counter += 1 * Time.deltaTime;
        if (counter <= 2)
        {

            return;

        }
        else
        {
            titleCanvasObj = GameObject.Find("Canvas_StartEffect");
            titleAlpha = titleCanvasObj.GetComponent<CanvasGroup>().alpha;
            if (titleAlpha <= 0)
            {
                counter = 0;
                gamePhase = 1;
            }
            titleAlpha -= 1 * Time.deltaTime;
            titleCanvasObj.GetComponent<CanvasGroup>().alpha = titleAlpha;

        }

    }
    void BGMStart()
    {
        bgmObject.SetActive(true);
        gamePhase = 2;
    }
}
