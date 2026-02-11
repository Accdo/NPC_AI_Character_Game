using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueController : MonoBehaviour
{
    [TextArea]
    public string npcPrompt;

    private bool isWaiting = false;

    public void TalkToNPC(string playerInput)
    {
        if (isWaiting) return;

        isWaiting = true;

        StartCoroutine(
            OpenAIManager.Instance.SendMessage(
                playerInput,
                npcPrompt,
                response =>
                {
                    isWaiting = false;
                    OnAIResponse(response);
                }
            )
        );
    }

    private void OnAIResponse(string response)
    {
        Debug.Log("NPC 응답: " + response);
        // 👉 여기서 대화 UI에 출력
        DialogueUIManager.Instance.ShowDialogue(response);
    }
}