using System.IO;
using UnityEditor;
using UnityEngine;

class StickerSettings : ScriptableObject
{
	public const string k_MyCustomSettingsPath = "Assets/Editor/Stickers.asset";

	public bool exportEnabled;
	public string path;

	internal static StickerSettings GetOrCreateSettings()
	{
			Directory.CreateDirectory(Path.GetDirectoryName(k_MyCustomSettingsPath));
		
		var settings = AssetDatabase.LoadAssetAtPath<StickerSettings>(k_MyCustomSettingsPath);
		if (settings == null)
		{
			settings = ScriptableObject.CreateInstance<StickerSettings>();
			settings.path = "Stickers";
			settings.exportEnabled = true;
			AssetDatabase.CreateAsset(settings, k_MyCustomSettingsPath);
			AssetDatabase.SaveAssets();
		}
		return settings;
	}

	internal static SerializedObject GetSerializedSettings()
	{
		return new SerializedObject(GetOrCreateSettings());
	}
}