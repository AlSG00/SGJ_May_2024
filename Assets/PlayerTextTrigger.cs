using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class PlayerTextTrigger : MonoBehaviour
{
    [SerializeField] private string[] _text;
    [SerializeField] private LocalizedString[] _localizedString;
    
    public static event System.Action<string> ShowingText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        foreach (var text in _localizedString)
        {
            ShowingText?.Invoke(text.GetLocalizedString());
        }
        
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
