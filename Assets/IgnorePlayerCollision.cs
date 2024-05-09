using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayerCollision : MonoBehaviour
{
    private Collider _collider;

    private void Start()
    {
        _collider = gameObject.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Physics.IgnoreCollision(_collider, collision.collider);
        }
    }
}
