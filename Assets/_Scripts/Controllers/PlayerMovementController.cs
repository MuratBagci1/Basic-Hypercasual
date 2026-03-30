using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private Vector3 walkVelocity;
    [SerializeField] private bool isWalking;

    private bool finishLinePassed;

    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ActionManager.OnTapToPlayPressed += StartWalking;
        ActionManager.OnFinishLine += FinishLinePassed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            StopWalking();

            if (finishLinePassed)
                ActionManager.OnSuccess?.Invoke();
            else
                ActionManager.OnFail?.Invoke();
        }
    }

    private void Update()
    {
        if(isWalking)
        {
            rb.velocity = walkVelocity;
        }
    }

    private void StartWalking()
    {
        isWalking = true;
        animator.SetBool(Params.IsWalking, true);
    }

    private void StopWalking()
    {
        isWalking = false;
        animator.SetBool(Params.IsWalking, false);
        rb.velocity = Vector3.zero;
        ActionManager.OnStoppedWalking?.Invoke();
    }

    private void FinishLinePassed()
    {
        finishLinePassed = true;
    }

    private void OnDestroy()
    {
        ActionManager.OnTapToPlayPressed -= StartWalking;
        ActionManager.OnFinishLine -= FinishLinePassed;
    }
}