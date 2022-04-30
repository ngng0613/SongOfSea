using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float endTime = GetComponent<AudioSource>().clip.length * Time.timeScale;
        Destroy(gameObject, endTime);



    }
}
