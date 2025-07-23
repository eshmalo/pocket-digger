using UnityEditor;
using UnityEngine;
using System.Linq;

namespace UnityBuilderAction
{
    public static class BuildScript
    {
        private static readonly string[] Scenes = FindEnabledEditorScenes();

        public static void Build()
        {
            string buildPath = GetBuildPath();
            string buildFile = GetBuildFile();
            string buildPathFull = buildPath + "/" + buildFile;

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = Scenes,
                locationPathName = buildPathFull,
                target = EditorUserBuildSettings.activeBuildTarget,
                options = BuildOptions.None
            };

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }

        private static string[] FindEnabledEditorScenes()
        {
            return EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();
        }

        private static string GetBuildPath()
        {
            return System.Environment.GetEnvironmentVariable("BUILD_PATH") ?? "build/Android";
        }

        private static string GetBuildFile()
        {
            return System.Environment.GetEnvironmentVariable("BUILD_FILE") ?? "PocketDigger.apk";
        }
    }
}