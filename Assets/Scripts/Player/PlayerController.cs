using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동
    [SerializeField] private float moveSpeed;
    public bool canMove = true;
    private Animator anim;
    private Rigidbody2D rb;
    Vector2 inputDir;

    // NPC 상호작용
    bool canInteract = false;
    NPC currentNPC = null;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove) return;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        inputDir = new Vector2(inputX, inputY);

        anim.SetBool("isMove", inputDir != Vector2.zero);
        anim.SetFloat("MoveX", inputX);
        anim.SetFloat("MoveY", inputY);

        if (!canInteract) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            currentNPC?.Interact();
        }
    }
    void FixedUpdate()
    {
        Vector2 dir = inputDir.normalized;
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetMovement(bool value)
    {
        canMove = value;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            Debug.Log("NPC 범위 들어옴");
            currentNPC = collision.GetComponent<NPC>();
            currentNPC.ActiveInteractionUI(true);

            canInteract = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            Debug.Log("NPC 범위 나감");
            currentNPC.ActiveInteractionUI(false);
            currentNPC = null;

            canInteract = false;
        }
    }
}
