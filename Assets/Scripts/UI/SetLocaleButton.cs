using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class SetLocaleButton : MonoBehaviour
{
    [SerializeField] private int _localeIndex;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _mouseOverAudio;
    [SerializeField] private AudioClip _mouseClickAudio;

    public static event System.Action LocalizationPicked;

    public void OnClick()
    {
        _audioSource.PlayOneShot(_mouseClickAudio);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeIndex];
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    //private void OnMouseEnter()
    //{
    //    Debug.Log("HSDASD");
    //    _audioSource.PlayOneShot(_mouseOverAudio);
    //}
}
