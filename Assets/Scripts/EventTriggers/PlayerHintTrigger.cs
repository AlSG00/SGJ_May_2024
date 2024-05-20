using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class PlayerHintTrigger : MonoBehaviour
{
    [SerializeField] private string[] _text;
    [SerializeField] private LocalizedString[] _localizedString;

    public static event System.Action<string> ShowingHint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        foreach (var text in _localizedString)
        {
            ShowingHint?.Invoke(text.GetLocalizedString());
        }

        gameObject.GetComponent<Collider>().enabled = false;
    }
}
