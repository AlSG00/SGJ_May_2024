using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class SetLocaleButton : MonoBehaviour
{
    [SerializeField] private int _localeIndex;

    public static event System.Action LocalizationPicked;

    public void OnClick()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeIndex];
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        //LocalizationPicked?.Invoke();
    }
}
