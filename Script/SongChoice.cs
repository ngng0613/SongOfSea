using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongChoice : MonoBehaviour
{

    int phase = 0;

    public int nowChoiceSong = 1;
    public GameObject nowChoicePrehab;

    public GameObject sceneChange;
    int sceneChangePhase = 0;

    public GameObject musicTitleObject;
    public GameObject musicComposerObject;

    public GameObject seObject;

    [SerializeField] List<GameObject> musicList = null;
    public List<GameObject> musicListDisplay;
    [SerializeField] Canvas canvas = null;

    [SerializeField] GameObject bgm;

    enum KEYTYPE
    {
        Up, Down,

    }
    KEYTYPE keyType;

    public Vector3 temp;
    public static string musicTitle;
    public static AudioClip musicData;
    public static TextAsset musicCsv;
    public static string musicComposer;

    public int secondsCount = 0;

    private void Start()
    {
        nowChoicePrehab = musicList[0];
        musicTitle = nowChoicePrehab.GetComponent<SongPrefab>().songTitle;
        musicTitleObject.GetComponent<Text>().text = musicTitle;
        musicComposer = nowChoicePrehab.GetComponent<SongPrefab>().songCreater;
        musicComposerObject.GetComponent<Text>().text = musicComposer;
        //CSV読み込み
        MainScript.csvFile = nowChoicePrehab.GetComponent<SongPrefab>().csvFile;

        bgm.GetComponent<AudioSource>().clip = nowChoicePrehab.GetComponent<SongPrefab>().songData;
        bgm.GetComponent<AudioSource>().volume = 0;
        bgm.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        /*
        ◆主な流れ
        1：曲全体の読み込み処理(曲本体、曲タイトル、曲アルバム表紙)
        2：曲一覧の描画
        2.5：曲選択ループ（時間あったら）
        3：曲選択（y軸0地点のprehabのデータを取得）
        4：曲データを持ちつつシーン移動


        1：曲データを配列で保存

        2：決定されたらprehabに保存されている曲タイトル、曲データ、譜面データを読み込む

        */

        if (phase == 0)
        {
            //配列に、曲prehabを代入（手動）




            //配列に存在する曲のprehabを順に表示

            int musicId;
            int loop = 7;
            if (musicList.Count > 7)
            {
                loop = musicList.Count;
            }

            for (int i = 0; i < 7; i++)
            {
                musicId = i;

                while (musicId >= musicList.Count)
                {
                    musicId -= musicList.Count;
                }


                musicListDisplay.Add(null);
                musicListDisplay[i] = Instantiate(musicList[musicId]);
                musicListDisplay[i].transform.SetParent(canvas.transform, false);

                if (i == 0)
                {
                    Vector3 namePos = musicListDisplay[i].transform.position;
                    namePos.x = 1300;
                    musicListDisplay[i].transform.position = namePos;
                }
                else if (i == 1)
                {
                    Vector3 namePos = musicListDisplay[i].transform.position;
                    namePos.x = 1400;
                    namePos.y -= 175;
                    musicListDisplay[i].transform.position = namePos;
                }
                else if (i == 2)
                {
                    Vector3 namePos = musicListDisplay[i].transform.position;
                    namePos.x = 1400;
                    namePos.y -= 350;
                    musicListDisplay[i].transform.position = namePos;
                }
                else if (i == 3)
                {
                    Vector3 namePos = musicListDisplay[i].transform.position;
                    namePos.x = 1400;
                    namePos.y -= 525;
                    musicListDisplay[i].transform.position = namePos;
                }
                else if (i == 4)
                {
                    Vector3 namePos = musicListDisplay[i].transform.position;
                    namePos.x = 1400;
                    namePos.y += 525;
                    musicListDisplay[i].transform.position = namePos;
                }
                else if (i == 5)
                {
                    Vector3 namePos = musicListDisplay[i].transform.position;
                    namePos.x = 1400;
                    namePos.y += 350;
                    musicListDisplay[i].transform.position = namePos;
                }
                else if (i == 6)
                {
                    Vector3 namePos = musicListDisplay[i].transform.position;
                    namePos.x = 1400;
                    namePos.y += 175;
                    musicListDisplay[i].transform.position = namePos;
                }

            }
            phase = 1;

        }
        else if (phase == 1)
        {
            //フェードイン

            Color colorAlpha = sceneChange.GetComponent<Image>().color;
            colorAlpha.a -= 1 * Time.deltaTime;
            sceneChange.GetComponent<Image>().color = colorAlpha;
            if (colorAlpha.a <= 0)
            {
                sceneChangePhase = 0;
                phase = 2;
            }



        }
        else if (phase == 2)
        {
            float bgmVolume = bgm.GetComponent<AudioSource>().volume;
            if (bgmVolume < 0.3)
            {
                bgmVolume += 0.6f * Time.deltaTime;
                bgm.GetComponent<AudioSource>().volume = bgmVolume;
            }

            //曲選択操作

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (nowChoiceSong > musicList.Count)
                {

                    nowChoiceSong = 1;
                }
                else
                {
                    nowChoiceSong += 1;
                }



                for (int i = 0; i < musicListDisplay.Count; i++)
                {
                    temp = musicListDisplay[i].transform.position;
                    temp.y += 175;

                    if (temp.y == 540)
                    {
                        temp.x = 1300;
                        nowChoicePrehab = musicListDisplay[i];


                    }
                    else if (temp.y == 715 || temp.y == 365)
                    {
                        temp.x = 1400;
                    }
                    else if (temp.y > 1065)
                    {
                        temp.y = 15;
                    }
                    musicListDisplay[i].transform.position = temp;


                }

                cursor();

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (nowChoiceSong == 1)
                {

                    nowChoiceSong = musicList.Count;
                }
                else
                {
                    nowChoiceSong -= 1;

                }



                for (int i = 0; i < musicListDisplay.Count; i++)
                {
                    temp = musicListDisplay[i].transform.position;
                    temp.y -= 175;

                    if (temp.y == 540)
                    {
                        temp.x = 1300;
                        nowChoicePrehab = musicListDisplay[i];
                    }
                    else if (temp.y == 715 || temp.y == 365)
                    {
                        temp.x = 1400;
                    }
                    else if (temp.y < 15)
                    {
                        temp.y = 1065;
                    }

                    musicListDisplay[i].transform.position = temp;


                }

                cursor();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                phase = 3;
            }



        }
        else if (phase == 3)
        {
            if (sceneChangePhase == 0)
            {
                Color colorAlpha = sceneChange.GetComponent<Image>().color;
                colorAlpha.a += 0.01f;
                sceneChange.GetComponent<Image>().color = colorAlpha;
                if (colorAlpha.a >= 1)
                {
                    sceneChangePhase = 1;
                }
            }
            else if (sceneChangePhase == 1)
            {

                Color colorAlpha = sceneChange.GetComponent<Image>().color;
                colorAlpha.a += 1 * Time.deltaTime;
                sceneChange.GetComponent<Image>().color = colorAlpha;
                if (colorAlpha.a >= 1)
                {
                    sceneChangePhase = 0;
                    musicTitle = nowChoicePrehab.GetComponent<SongPrefab>().songTitle;
                    musicData = nowChoicePrehab.GetComponent<SongPrefab>().songData;
                    SceneManager.LoadScene("SampleScene");
                }
   

            }
        }

    }
    void cursor()
    {
        //BGMの音量を下げる
        bgm.GetComponent<AudioSource>().volume = 0;

        Instantiate(seObject);

        musicTitle = nowChoicePrehab.GetComponent<SongPrefab>().songTitle;
        musicTitleObject.GetComponent<Text>().text = musicTitle;

        musicComposer = nowChoicePrehab.GetComponent<SongPrefab>().songCreater;
        musicComposerObject.GetComponent<Text>().text = musicComposer;


        //CSV読み込み
        MainScript.csvFile = nowChoicePrehab.GetComponent<SongPrefab>().csvFile;

        bgm.GetComponent<AudioSource>().clip = nowChoicePrehab.GetComponent<SongPrefab>().songData;
        bgm.GetComponent<AudioSource>().Play();


    }

}
