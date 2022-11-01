using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class webcam : MonoBehaviour {
    public Material[] mat;
    //public float slot;
    void Start () {
        WebCamTexture WebCamTexture = new WebCamTexture();
        for (int i = 0; i < mat.Length; i++)
        {
            mat[i].SetTexture("_MainTex", WebCamTexture);
        }
        WebCamTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
