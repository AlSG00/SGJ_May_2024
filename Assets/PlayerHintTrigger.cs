using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class PlayerHintTrigger : MonoBehaviour
{
    [SerializeField] private string[] _text;
    [SerializeField] private LocalizedString test;

    public static event System.Action<string> ShowingHint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        foreach (var text in _text)
        {
            ShowingHint?.Invoke(test.GetLocalizedString());
        }

        gameObject.GetComponent<Collider>().enabled = false;
    }
}
