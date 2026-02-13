using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public TMP_InputField inputField;

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

        DialogueUIManager.Instance.currentNPCDialogueCon.TalkToNPC(text);
        inputField.text = "";
        inputField.ActivateInputField(); // 다시 포커스
    }

    public void OnSendButton()
    {
        string text = inputField.text;
        inputField.text = "";

        DialogueUIManager.Instance.currentNPCDialogueCon.TalkToNPC(text);
    }
}