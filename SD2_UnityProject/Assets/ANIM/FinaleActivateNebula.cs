using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleActivateNebula : MonoBehaviour
{
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel("SCN_Control");
        }


    }
}
