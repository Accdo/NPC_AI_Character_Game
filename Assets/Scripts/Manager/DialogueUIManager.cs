using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager Instance { get; private set; }

    public GameObject TalkPanel;

    // 플레이어
    public PlayerController player;

    // NPC 대화 텍스트 관련
    private NPC currentNPC;
    public NPCDialogueController currentNPCDialogueCon;

    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.03f;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowDialogue(string npcResponse)
    {
        // 👉 대화 UI에 npcResponse 출력
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(npcResponse));
    }

    IEnumerator TypeText(string fullText)
    {
        dialogueText.text = "";

        foreach (char letter in fullText)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void OpenDialogue(NPC npc)
    {
        currentNPC = npc;
        currentNPCDialogueCon = npc.NPCDialogueCon;
        currentNPC.ActiveInteractionUI(false);

        TalkPanel.SetActive(true);
        player.SetMovement(false);
    }

    public void CloseDialogue()
    {
        currentNPC.ActiveInteractionUI(true);
        currentNPC = null;
        currentNPCDialogueCon = null;

        TalkPanel.SetActive(false);
        player.SetMovement(true);
    }
}
