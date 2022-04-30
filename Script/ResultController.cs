using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    [SerializeField] int resultScore;
    [SerializeField] int resultCombo;

    public GameObject scoreObject;
    public GameObject comboObject;
    public GameObject charObject;

    public GameObject fadeObject;

    public Color color;

    public int resultPhase = 0;
    public int count = 0;
    int stopCount = 0;

    private void Start()
    {
        scoreObject.GetComponent<Text>().text = "";
        comboObject.GetComponent<Text>().text = "";
        charObject.GetComponent<Text>().text = "";

        resultScore = MainScript.score;
        resultCombo = MainScript.maxCombo;
        /*
        //デバッグ用
        resultScore = 2000;
        resultCombo = 100;
        */
    }


    // Update is called once per frame
    void Update()
    {

        if (resultPhase == 0)
        {
            //フェードイン
            Color color = fadeObject.GetComponent<Image>().color;
            color.a -= 2 * Time.deltaTime;
            fadeObject.GetComponent<Image>().color = color;
            if (color.a <= 0)
            {
                resultPhase = 1;

            }
        }
        else if ((count < resultScore || scoreObject.GetComponent<Text>().color.a < 1 )&& resultPhase == 1)
        {
            //スコア表示
            scoreObject.GetComponent<Text>().text = count + "";
            if (count < resultScore)
            {
                count += 50;
            }
       

            Color color = scoreObject.GetComponent<Text>().color;
            color.a += 1 * Time.deltaTime;
            scoreObject.GetComponent<Text>().color = color;
        }
        else if (resultPhase == 1 && scoreObject.GetComponent<Text>().color.a >= 1 && count >= resultScore)
        {

            if (stopCount < 30)
            {
                stopCount++;
            }
            else
            {
                count = 0;
                stopCount = 0;
                resultPhase = 2;
            }

        }

        if ((count < resultCombo || comboObject.GetComponent<Text>().color.a < 1)&& resultPhase == 2)
        {
            //最大コンボ表示
            comboObject.GetComponent<Text>().text = count + "";
            if (count < resultCombo)
            {
                count++;
            }
            
            Color color = comboObject.GetComponent<Text>().color;
            color.a += 1 * Time.deltaTime;
            comboObject.GetComponent<Text>().color = color;
        }
        else if (resultPhase == 2 && comboObject.GetComponent<Text>().color.a >= 1 && count >=resultCombo)
        {
            if (stopCount < 0)
            {
                stopCount++;
            }
            else
            {
                count = 0;
                stopCount = 0;
                resultPhase = 3;
            }
        }
        if (resultPhase == 3)
        {
            if (resultScore >= 3000)
            {

                charObject.GetComponent<Text>().text = "A";
                color = charObject.GetComponent<Text>().color;
                color.r = 255 / 255.0f;
                color.g = 135 / 255.0f;
                color.b = 135 / 255.0f;
                color.a += 1 * Time.deltaTime;
                charObject.GetComponent<Text>().color = color;

            }
            else if (resultScore >= 1500)
            {
                charObject.GetComponent<Text>().text = "B";
                color = charObject.GetComponent<Text>().color;
                color.r = 0 / 255.0f; ;
                color.g = 185 / 255.0f;
                color.b = 235 / 255.0f;
                color.a += 1 * Time.deltaTime;
                charObject.GetComponent<Text>().color = color;

            }
            else
            {
                charObject.GetComponent<Text>().text = "C";
                color = charObject.GetComponent<Text>().color;
                color.r = 135 / 255.0f;
                color.g = 255 / 255.0f;
                color.b = 135 / 255.0f;
                color.a += 1 * Time.deltaTime;
                charObject.GetComponent<Text>().color = color;


            }
            if (color.a >= 1)
            {
                resultPhase = 4;
            }
           
        }
        else if (resultPhase == 4)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

                resultPhase = 5;
            }

        }
        else if (resultPhase == 5)
        {
            //フェードイン
            Color color = fadeObject.GetComponent<Image>().color;
            color.a += 1 * Time.deltaTime;
            fadeObject.GetComponent<Image>().color = color;
            if (color.a >= 1)
            {
                SceneManager.LoadScene("SongSelect");

            }
        }



    }
}
