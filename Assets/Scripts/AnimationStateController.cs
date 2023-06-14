using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    private float hInput;
    private float vInput;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool forwardPressed = false;
        bool runningPressed = Input.GetKey("left shift");
        bool sneakingPressed = Input.GetKey("space");

        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (vInput > 0)
            forwardPressed = true;

        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && (runningPressed && forwardPressed))
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!runningPressed || !forwardPressed))
        {
            animator.SetBool(isRunningHash, false);
        }

        if (!isJumping && (sneakingPressed && forwardPressed))
        {
            animator.SetBool(isJumpingHash, true);
        }
        if (isJumping && (!sneakingPressed || !forwardPressed))
        {
            animator.SetBool(isJumpingHash, false);
        }
    }
}
