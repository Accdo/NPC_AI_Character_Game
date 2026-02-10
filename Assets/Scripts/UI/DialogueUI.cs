using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public TMP_InputField inputField;
    public NPCDialogueController npc;

    private void OnEnable()
    {
        inputField.onSubmit.AddListener(OnSubmit);
        // 또는 onEndEdit
    }

    private void OnDisable()
    {
        inputField.onSubmit.RemoveListener(OnSubmit);
    }

    private void OnSubmit(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return;

        npc.TalkToNPC(text);
        inputField.text = "";
        inputField.ActivateInputField(); // 다시 포커스
    }

    public void OnSendButton()
    {
        string text = inputField.text;
        inputField.text = "";

        npc.TalkToNPC(text);
    }
}