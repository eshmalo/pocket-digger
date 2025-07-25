using UnityEditor;
using UnityEngine;
using System.IO;

public class BuildHelper
{
    [MenuItem("Pocket Digger/Build Internal Test AAB")]
    public static void BuildInternalAAB()
    {
        string buildPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Builds", "PocketDigger-0.1-int.aab");
        
        // Ensure build directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(buildPath));
        
        // Configure player settings
        PlayerSettings.Android.bundleVersionCode = 1;
        PlayerSettings.bundleVersion = "0.1.0";
        PlayerSettings.companyName = "EshMalo";
        PlayerSettings.productName = "Pocket Digger";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.eshmalo.pocketdigger");
        
        // Keystore settings
        PlayerSettings.Android.keystoreName = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "pocket-digger.keystore");
        PlayerSettings.Android.keystorePass = "pocket123";
        PlayerSettings.Android.keyaliasName = "pocket-digger";
        PlayerSettings.Android.keyaliasPass = "pocket123";
        
        // Build options
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = new[] { "Assets/Scenes/SampleScene.unity" },
            locationPathName = buildPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };
        
        // Set to build App Bundle
        EditorUserBuildSettings.buildAppBundle = true;
        
        Debug.Log("Starting AAB build...");
        var report = BuildPipeline.BuildPlayer(buildOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"Build succeeded! AAB location: {buildPath}");
            Debug.Log($"Size: {new FileInfo(buildPath).Length / 1024 / 1024} MB");
            EditorUtility.RevealInFinder(buildPath);
        }
        else
        {
            Debug.LogError("Build failed!");
        }
    }
}