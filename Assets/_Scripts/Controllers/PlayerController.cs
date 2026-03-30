using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 touchStr;
    [SerializeField] private Vector3 touchEnd;
    [SerializeField] private float diff;
    [SerializeField] private float sensitivity;
    [SerializeField] private float lerpTime;
    [SerializeField] private float edge;
    [SerializeField] private Transform playerT;
    [SerializeField] private Camera UICamera;

    private bool canControl;

    void Start()
    {
        ActionManager.OnTapToPlayPressed += EnableControl;
        ActionManager.OnStoppedWalking += DisableControl;
    }

    private void Update()
    {
        if(canControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseDown();
            }
            if (Input.GetMouseButton(0))
            {
                MouseHold();
            }
            if (Input.GetMouseButtonUp(0))
            {
                MouseUp();
            }
        }
    }

    private void EnableControl()
    {
        canControl = true;
    }

    private void DisableControl()
    {
        canControl = false;
    }

    private void MouseDown()
    {
        touchStr = UICamera.ScreenToWorldPoint(Input.mousePosition);
    }
    private void MouseHold()
    {
        touchEnd = UICamera.ScreenToWorldPoint(Input.mousePosition);
        diff = touchEnd.x - touchStr.x;
        touchStr = touchEnd;
        MovePlayer(diff);
    }

    private void MouseUp()
    {
        touchStr = Vector3.zero;
        touchEnd = Vector3.zero;
        diff = 0;
    }

    private void MovePlayer(float diff)
    {
        Vector3 targetPosition = CalculatePosition(diff);
        playerT.transform.position = Vector3.Lerp(playerT.transform.position, targetPosition, lerpTime);
    }

    private Vector3 CalculatePosition(float diff)
    {
        float positionX = playerT.position.x + diff * sensitivity;
        if(diff > 0 && positionX > 0)
        {
            positionX = Mathf.Min(positionX, edge);
        }
        else if(diff < 0 && positionX < 0)
        {
            positionX = Mathf.Max(positionX, -edge);
        }
        return new Vector3(positionX, playerT.position.y, playerT.position.z);
    }
}