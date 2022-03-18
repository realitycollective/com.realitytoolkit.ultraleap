// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.IO;
using UnityEditor;
using XRTK.Editor;
using XRTK.Editor.Utilities;
using XRTK.Extensions;

namespace XRTK.Ultraleap.Editor
{
    [InitializeOnLoad]
    internal static class UltraleapPackageInstaller
    {
        private static readonly string DefaultPath = $"{MixedRealityPreferences.ProfileGenerationPath}Ultraleap";
        private static readonly string HiddenPath = Path.GetFullPath($"{PathFinderUtility.ResolvePath<IPathFinder>(typeof(UltraleapPathFinder)).BackSlashes()}{Path.DirectorySeparatorChar}{MixedRealityPreferences.HIDDEN_PACKAGE_ASSETS_PATH}");

        static UltraleapPackageInstaller()
        {
            EditorApplication.delayCall += CheckPackage;
        }

        [MenuItem(MixedRealityPreferences.Editor_Menu_Keyword + "/Packages/Install Ultraleap Package Assets...", true)]
        private static bool ImportPackageAssetsValidation()
        {
            return !Directory.Exists($"{DefaultPath}{Path.DirectorySeparatorChar}");
        }

        [MenuItem(MixedRealityPreferences.Editor_Menu_Keyword + "/Packages/Install Ultraleap Package Assets...")]
        private static void ImportPackageAssets()
        {
            EditorPreferences.Set($"{nameof(UltraleapPackageInstaller)}.Assets", false);
            EditorApplication.delayCall += CheckPackage;
        }

        private static void CheckPackage()
        {
            if (!EditorPreferences.Get($"{nameof(UltraleapPackageInstaller)}.Assets", false))
            {
                EditorPreferences.Set($"{nameof(UltraleapPackageInstaller)}.Assets", PackageInstaller.TryInstallAssets(HiddenPath, DefaultPath));
            }
        }
    }
}
