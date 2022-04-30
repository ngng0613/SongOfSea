using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class TextEffect : MonoBehaviour
{

    //NICEとかの文字表示の変数
    public string text;
   
    public int textPhases;
    public GameObject getKey;

    public int textNumber = 0;

    public float textAlpha;
    Vector3 textPos;
    Color textColor;

    public float delayTime = 0;




    // Start is called before the first frame update
    public void Start()
    {

    }

    public void DataInput(string textInput,GameObject getKeyInput)
    {
        
        text = textInput;
        getKey = getKeyInput;
        
    }


    // Update is called once per frame
    void Update()
    {


        textPos = GetComponent<Transform>().position;
        textColor = GetComponent<Text>().color;
        Color textOutLineColor = GetComponent<Outline>().effectColor;
        textAlpha = textColor.a;
        GetComponent<Text>().text = text;


        if (textAlpha < 1)
        {

            textPos.y += 1 * Time.deltaTime;
            textColor.a += 2 * Time.deltaTime;
            textOutLineColor.a += 2 * Time.deltaTime;

            GetComponent<Transform>().position = textPos;
            GetComponent<Text>().color = textColor;
            GetComponent<Outline>().effectColor = textOutLineColor;
        }
        else if (textAlpha >= 1)
        {
            delayTime += 1 * Time.deltaTime;
            if (delayTime < 0.8)
            {
                return;
            }
            else
            {
                if (gameObject != null)
                {
                    GETKEY getKeyComponent = getKey.GetComponent<GETKEY>();
                    getKeyComponent.textList.RemoveAt(0);
                    DestroyImmediate(gameObject);
                }
                delayTime = 0;
            }


        }
    }
}
