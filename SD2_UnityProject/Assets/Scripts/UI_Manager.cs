using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
   // public Text T_Next;
   // public Text T_Current;
   // public Text T_Noir;
    public string T_FX;
   // public Text T_Grain;

    public GameObject UI_Next;
    public GameObject UI_Current;
    public GameObject UI_Noir;
    public GameObject UI_FX;
    public GameObject UI_Sound;
    public GameObject UI_SoundControl01;
    public GameObject UI_SoundControl02;
    public GameObject UI_SoundControl03;
    public GameObject UI_Grain;
    public GameObject UI_Deform;
    public GameObject UI_SettingShape;
    void Start()
    {
        UI_FX.SetActive(false);
        UI_SoundControl01.SetActive(false);
        UI_SoundControl02.SetActive(false);
        UI_SoundControl03.SetActive(false);
        //UI_Current.GetComponentInChildren<Text>().text = T_Current;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
