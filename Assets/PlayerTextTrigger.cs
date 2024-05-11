using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextTrigger : MonoBehaviour
{
    [SerializeField] private string[] _text;
    
    public static event System.Action<string> ShowingText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        foreach (var text in _text)
        {
            ShowingText?.Invoke(text);
        }
        
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
