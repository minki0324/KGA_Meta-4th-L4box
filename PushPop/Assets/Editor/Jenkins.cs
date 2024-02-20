// Assets/Editor/Jenkins.cs
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Jenkins
{
    [MenuItem("Build/Android")]
    public static void PerformBuild_Android()
    {
        BuildPlayerOptions options = new BuildPlayerOptions();
        // 씬 추가
        options.scenes = new[] { "Assets/00_Maingame.unity", "Assets/01_Network.unity" };
        /*List<string> scenes = new List<string>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            scenes.Add(scene.path);
        }
        options.scenes = scenes.ToArray();*/
        // 타겟 경로(빌드 결과물이 여기 생성됨)
        options.locationPathName = $"../Build/PushPop{PlayerSettings.bundleVersion}.apk";
        // 빌드 타겟
        options.target = BuildTarget.Android;

        // 빌드
        BuildPipeline.BuildPlayer(options);
    }
}