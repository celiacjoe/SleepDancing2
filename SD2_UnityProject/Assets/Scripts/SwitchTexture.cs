using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTexture : MonoBehaviour
{
    public Texture m_Default, m_01, m_02;
    public Renderer R;
    public bool B;
  //  public GameObject GO01;
  //  public Material MatScreen01;
 //   public RenderTexture RT_Flux01;
    //public Texture Default;
    //public Texture TXT01;
    void Start()
    {
        R.material.EnableKeyword("_Default");
        R.material.SetTexture("_MainTex", m_Default);
        // R.material.mainTexture = Default;
       //R = GO01.GetComponent<Renderer>();
        R.material.EnableKeyword("_01");
        R.material.EnableKeyword("_02");
        Debug.Log("Started");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            R.material.SetTexture("_MainTex", m_01);
            // R.material.mainTexture = TXT01;
            //GO01.renderer.material.mainTexture = RT_Flux01;
            // MatScreen01.mainTexture=TXT01;
            Debug.Log("COUCOU");
        }
    }
}
