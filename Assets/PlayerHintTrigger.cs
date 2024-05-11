using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHintTrigger : MonoBehaviour
{
    [SerializeField] private string[] _text;

    public static event System.Action<string> ShowingHint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        foreach (var text in _text)
        {
            ShowingHint?.Invoke(text);
        }

        gameObject.GetComponent<Collider>().enabled = false;
    }
}
