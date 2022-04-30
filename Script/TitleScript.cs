using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public GameObject StartFade;
    public GameObject startText;
    public GameObject seObject;

    public int phase = 1;
    public Color color;
    bool textSwitch = true;
    int sceneChangePhase = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TextEffect();
        if (phase == 1)
        {
            

            color = StartFade.GetComponent<Image>().color;
            color.a -= 1 * Time.deltaTime;
            StartFade.GetComponent<Image>().color = color;

            if (color.a <= 0)
            {
                phase = 2;
            }
        }
        else if (phase == 2)
        {
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Instantiate(seObject);
                phase = 3;
                

            }
        }
        else if (phase == 3)
        {
            if (sceneChangePhase == 1)
            {
                color = StartFade.GetComponent<Image>().color;
                color.a += 1 * Time.deltaTime;
                StartFade.GetComponent<Image>().color = color;

                if (color.a >= 1)
                {
                    sceneChangePhase = 2;
                }
            }
            if (sceneChangePhase == 2)
            {
                SceneManager.LoadScene("SongSelect");

            }
         
        }




    }
    void TextEffect()
    {
        Color color = startText.GetComponent<Text>().color;
        if (textSwitch)
        {
            color.a -= 0.7f * Time.deltaTime;
            startText.GetComponent<Text>().color = color;
            if (color.a <= 0)
            {
                textSwitch = false;
            }
        }
        else
        {
            color.a += 0.7f * Time.deltaTime;
            startText.GetComponent<Text>().color = color;
            if (color.a >= 1)
            {
                textSwitch = true;
            }

        }


    }
}
