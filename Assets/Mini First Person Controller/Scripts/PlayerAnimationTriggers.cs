using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    public GameObject PlayerController;
    [SerializeField] GroundCheck groundCheck;

    Animator animator;
    FirstPersonMovement personMovement;
    Jump jump;
    Crouch crouch;
    bool wasInAir;

    private void Start()
    {
        animator = GetComponent<Animator>();
        personMovement = PlayerController.GetComponent<FirstPersonMovement>();
        jump = PlayerController.GetComponent<Jump>();
        crouch = PlayerController.GetComponent<Crouch>();
    }
    void Update()
    {
        if (wasInAir == false && !groundCheck.isGrounded)
            animator.SetTrigger("isJumped");
        animator.SetBool("isInAir", !groundCheck.isGrounded);
        animator.SetBool("isCrouched", crouch.IsCrouched);

        wasInAir = !groundCheck.isGrounded;
    }
}
