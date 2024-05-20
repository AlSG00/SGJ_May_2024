using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOrderController : MonoBehaviour
{
    [SerializeField] private bool _enabledOnStart;
    [SerializeField] private Collider _nextCollider;

    private void Start()
    {
        gameObject.GetComponent<Collider>().enabled = _enabledOnStart;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_nextCollider == null) { return; }

        _nextCollider.enabled = true;
    }
}
