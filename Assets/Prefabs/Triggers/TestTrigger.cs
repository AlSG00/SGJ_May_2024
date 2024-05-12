using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class TestTrigger : MonoBehaviour
{
    public LocalizedString test /*= new()*/;

    private void OnTriggerEnter(Collider other)
    {
        //test = new LocalizedString("LocalizationStringTable", "YoloEntry");
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        if (other.CompareTag("Player"))
        {


            //var collection = LocalizationEditorSettings.GetStringTableCollection("LocalizationStringTable");
            //var entry = collection.SharedData.GetEntry("YoloEntry");
            //test = new LocalizedString(collection.SharedData.TableCollectionNameGuid, entry.Id);
            var translatedValue = LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationStringTable", "YoloEntry");
            Debug.Log(translatedValue);
        }

        
    }
}
