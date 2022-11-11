using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public Texture m_Default, m_01, m_02;
    public Renderer R;
    public render QuadRender;
    public ComputeShader CS_Default, CS02, CS03, CS04;
    public string Scene;
    private int Nbr_Scene;
  //  public GameObject GO01;
  //  public Material MatScreen01;
 //   public RenderTexture RT_Flux01;
    //public Texture Default;
    //public Texture TXT01;
    void Start()
    {
        Nbr_Scene = 1;
        Scene = "SCENE_01";
        QuadRender.compute_shader = CS_Default;
        //R.material.EnableKeyword("_Default");
        //R.material.SetTexture("_MainTex", m_Default);
        R.material.EnableKeyword("_01");

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

    public void ChangeTexture()
    {
       // R.material.SetTexture("_MainTex", m_01);
        Debug.Log("Text01");
        if (Nbr_Scene == 1)
        {
            Nbr_Scene++;
            QuadRender.compute_shader = CS02;
            Debug.Log("Okçalaunch");
            Scene = "SCENE_02";
        }else if (Nbr_Scene == 2)
        {
            Nbr_Scene++;
            QuadRender.compute_shader = CS03;
            Scene = "SCENE_03";
        }else if (Nbr_Scene == 3)
        {
            Nbr_Scene++;
            QuadRender.compute_shader = CS04;
            Scene = "SCENE_04";
        }
    }
}
