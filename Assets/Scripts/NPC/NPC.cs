using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject TalkPanel;

    void Start()
    {
        
    }

    void Update()
    {

    }
    
    public void Interact()
    {
        Debug.Log("NPC와 상호작용!");

        TalkPanel.SetActive(true);
    }
}
