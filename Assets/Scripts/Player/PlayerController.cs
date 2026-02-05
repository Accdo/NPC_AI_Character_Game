using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (inputX != 0 || inputY != 0)
            anim.SetBool("isMove", true);
        else
            anim.SetBool("isMove", false);

        anim.SetFloat("MoveX", inputX);
        anim.SetFloat("MoveY", inputY);

        transform.Translate(new Vector2(inputX, inputY) * moveSpeed * Time.deltaTime);
    }
}
