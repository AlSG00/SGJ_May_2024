using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnobjectTrigger : MonoBehaviour
{

    [SerializeField] private GameObject[] _objects;
    [SerializeField] private bool _show;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        foreach (var obj in _objects)
        {
            obj.SetActive(_show);
        }

        gameObject.GetComponent<Collider>().enabled = false;
    }
}
