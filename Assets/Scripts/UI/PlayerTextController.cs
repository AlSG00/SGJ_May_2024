using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextController : MonoBehaviour
{
    [SerializeField] private Queue<string> _textQueue = new Queue<string>();
    [SerializeField] private Queue<string> _textHintQueue = new Queue<string>();
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private TextMeshProUGUI _textHintMesh;
    [SerializeField] private Image _textBackground;
    [SerializeField] private int timeToShow;
    [SerializeField] private int timeToShowHint;
    private float _elapsedTime;
    private bool _displayingMessage = false;
    private bool _displayingHint = false;
    private void Awake()
    {
        _textBackground.enabled = false;
        _textMesh.text = "";
    }

    private void OnEnable()
    {
        ObjectInteraction.InteractResult += Receive;
        ObjectInteraction.InteractHint += ReceiveHint;
        PlayerTextTrigger.ShowingText += Receive;
        PlayerHintTrigger.ShowingHint += ReceiveHint;
    }

    private void OnDisable()
    {
        ObjectInteraction.InteractResult -= Receive;
        ObjectInteraction.InteractHint -= ReceiveHint;
        PlayerTextTrigger.ShowingText -= Receive;
        PlayerHintTrigger.ShowingHint -= ReceiveHint;
    }

    private void Update()
    {
        if (_textQueue.Count != 0)
        {
            if (_displayingMessage == false)
            {
                _displayingMessage = true;
                ShowText(_textQueue.Dequeue());
            }
        }

        if (_textHintQueue.Count != 0)
        {
            if (_displayingHint == false)
            {
                _displayingHint = true;
                ShowHint(_textHintQueue.Dequeue());
            }
        }
    }

    private async void ShowText(string text)
    {
        _textBackground.enabled = true;
        _textMesh.text = text;
        await System.Threading.Tasks.Task.Delay(timeToShow);
        _textMesh.text = "";
        _textBackground.enabled = false;
        _displayingMessage = false;
    }

    private async void ShowHint(string text)
    {
        _textHintMesh.text = text;
        await System.Threading.Tasks.Task.Delay(timeToShowHint);
        _textHintMesh.text = "";
        _displayingHint = false;
    }

    private void Receive(string text)
    {
        if (_textQueue.Contains(text))
        {
            return;
        }
        _textQueue.Enqueue(text);
    }

    private void ReceiveHint(string text)
    {
        if (_textHintQueue.Contains(text))
        {
            return;
        }

        _textHintQueue.Enqueue(text);
    }
}
