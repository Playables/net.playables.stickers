using System;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode.PBX;

namespace UnityEditor.iOS.Xcode.Extensions.Custom
{
	public static class PBXProjectExtensions
	{
		internal static PBXProjectExtensions.FlagList appExtensionReleaseBuildFlags = new PBXProjectExtensions.FlagList()
		{
			{
				"LD_RUNPATH_SEARCH_PATHS",
				"$(inherited)"
			},
			{
				"LD_RUNPATH_SEARCH_PATHS",
				"@executable_path/Frameworks"
			},
			{
				"LD_RUNPATH_SEARCH_PATHS",
				"@executable_path/../../Frameworks"
			},
			{
				"PRODUCT_NAME",
				"$(TARGET_NAME)"
			},
			{
				"SKIP_INSTALL",
				"YES"
			}
		};
		internal static PBXProjectExtensions.FlagList appExtensionDebugBuildFlags = new PBXProjectExtensions.FlagList()
		{
			{
				"LD_RUNPATH_SEARCH_PATHS",
				"$(inherited)"
			},
			{
				"LD_RUNPATH_SEARCH_PATHS",
				"@executable_path/Frameworks"
			},
			{
				"LD_RUNPATH_SEARCH_PATHS",
				"@executable_path/../../Frameworks"
			},
			{
				"PRODUCT_NAME",
				"$(TARGET_NAME)"
			},
			{
				"SKIP_INSTALL",
				"YES"
			}
		};

		private static void SetBuildFlagsFromDict(
			this PBXProject proj,
			string configGuid,
			IEnumerable<KeyValuePair<string, string>> data)
		{
			foreach (KeyValuePair<string, string> keyValuePair in data)
				proj.AddBuildPropertyForConfig(configGuid, keyValuePair.Key, keyValuePair.Value);
		}

		internal static void SetDefaultAppExtensionReleaseBuildFlags(
			this PBXProject proj,
			string configGuid)
		{
			proj.SetBuildFlagsFromDict(configGuid, (IEnumerable<KeyValuePair<string, string>>) PBXProjectExtensions.appExtensionReleaseBuildFlags);
		}

		internal static void SetDefaultAppExtensionDebugBuildFlags(
			this PBXProject proj,
			string configGuid)
		{
			proj.SetBuildFlagsFromDict(configGuid, (IEnumerable<KeyValuePair<string, string>>) PBXProjectExtensions.appExtensionDebugBuildFlags);
		}

		
		public static string AddStickerExtension(
			this PBXProject proj,
			string mainTargetGuid,
			string name,
			string bundleId,
			string infoPlistPath)
		{
			string ext = ".appex";
			string str = proj.AddTarget(name, ext, "com.apple.product-type.app-extension.messages-sticker-pack");
			foreach (string buildConfigName in proj.BuildConfigNames())
			{
				string configGuid = proj.BuildConfigByName(str, buildConfigName);
				if (buildConfigName.Contains("Debug"))
					proj.SetDefaultAppExtensionDebugBuildFlags(configGuid);
				else
					proj.SetDefaultAppExtensionReleaseBuildFlags(configGuid);
				proj.SetBuildPropertyForConfig(configGuid, "INFOPLIST_FILE", infoPlistPath);
				proj.SetBuildPropertyForConfig(configGuid, "PRODUCT_BUNDLE_IDENTIFIER", bundleId);
			}
			proj.AddSourcesBuildPhase(str);
			proj.AddResourcesBuildPhase(str);
			proj.AddFrameworksBuildPhase(str);
			string sectionGuid = proj.AddCopyFilesBuildPhase(mainTargetGuid, "Embed App Extensions", "", "13");
			proj.AddFileToBuildSection(mainTargetGuid, sectionGuid, proj.GetTargetProductFileRef(str));
			proj.AddTargetDependency(mainTargetGuid, str);
			return str;
		}

		internal class FlagList : List<KeyValuePair<string, string>>
		{
			public void Add(string flag, string value)
			{
				this.Add(new KeyValuePair<string, string>(flag, value));
			}
		}
	}
}