using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private Image _textBackground;

    private void Awake()
    {
        _textBackground.enabled = false;
        _textMesh.text = "";
    }

    private void OnEnable()
    {
        ObjectInteraction.InteractResult += ShowText;
    }

    private void OnDisable()
    {
        ObjectInteraction.InteractResult -= ShowText;
    }

    private async void ShowText(string text)
    {
        _textBackground.enabled = true;
        _textMesh.text = text;
        await System.Threading.Tasks.Task.Delay(5000);
        _textMesh.text = "";
        _textBackground.enabled = false;
    }
}
