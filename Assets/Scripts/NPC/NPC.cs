using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject interactionUI;

    public void Interact()
    {
        Debug.Log("NPC와 상호작용!");

        DialogueUIManager.Instance.OpenDialogue(this);
    }

    public void ActiveInteractionUI(bool isActive)
    {
        interactionUI.SetActive(isActive);
    }
}
