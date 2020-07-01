using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


static class StickerSettingsProvider
{
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        // First parameter is the path in the Settings window.
        // Second parameter is the scope of this setting: it only appears in the Project Settings window.
        var provider = new SettingsProvider("Project/iMessage Stickers", SettingsScope.Project)
        {
            // By default the last token of the path is used as display name if no label is provided.
            label = "iMessage Stickers",
            // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
            guiHandler = (searchContext) =>
            {
                var settings = StickerSettings.GetSerializedSettings();
                EditorGUILayout.PropertyField(settings.FindProperty("path"), new GUIContent("Source path"));
                EditorGUILayout.PropertyField(settings.FindProperty("exportEnabled"), new GUIContent("Export"));
                settings.ApplyModifiedProperties();
            },

            // Populate the search keywords to enable smart search filtering and label highlighting:
            keywords = new HashSet<string>(new[] { "Stickers" })
        };

        return provider;
    }
}