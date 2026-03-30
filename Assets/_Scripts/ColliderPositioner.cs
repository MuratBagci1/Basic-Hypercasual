using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPositioner : MonoBehaviour
{
    private BoxCollider collider;
    [SerializeField] private Transform muzzleT;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    public void SetStackCollider(int change)
    {
        collider.size = new Vector3(collider.size.x, collider.size.y, collider.size.z + change);
        collider.center = new Vector3(collider.center.x, collider.center.y, collider.center.z + change / 2f);
    }

    public void SetMuzzlePosition(int change)
    {
        muzzleT.position = new Vector3(muzzleT.position.x, muzzleT.position.y, muzzleT.position.z + change);
    }
}
