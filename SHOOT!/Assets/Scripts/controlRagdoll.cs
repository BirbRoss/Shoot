using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlRagdoll : MonoBehaviour
{
    private Rigidbody[] rigidbodies;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        setRigidBodiesKinematic(true);
    }

    public void setRigidBodiesKinematic(bool kinematic)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = kinematic;
        }
    }
}
