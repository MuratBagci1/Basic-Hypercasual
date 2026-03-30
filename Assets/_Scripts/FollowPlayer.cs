using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerT;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool lockX;
    private void FixedUpdate()
    {
        if(lockX)
            transform.position = new Vector3(transform.position.x, transform.position.y, playerT.position.z) + offset;
        else
            transform.position = new Vector3(playerT.position.x, transform.position.y, playerT.position.z ) + offset;
    }
}