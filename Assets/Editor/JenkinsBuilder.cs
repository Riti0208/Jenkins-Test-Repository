using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// Jenkinsビルドクラス.
/// </summary>
public class JenkinsBuilder
{
		/// <summary>
		/// iOSビルド.
		/// </summary>
		public static void iOSBuid()
		{
				// スイッチプラットフォーム.
				if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS) // Unity4系は、BuildTarget.iPhone になります.
				{
						EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);
				}

				// シーン名を取得する.
				List<string> scenePathList = new List<string>();
				List<EditorBuildSettingsScene> sceneList = EditorBuildSettings.scenes.ToList();
				foreach (EditorBuildSettingsScene scene in sceneList)
				{
						if (scene.enabled && File.Exists(scene.path))
						{
								scenePathList.Add(scene.path);
						}
				}

				// ビルド.
				if (0 < scenePathList.Count)
				{
						BuildPipeline.BuildPlayer(
							scenePathList.ToArray(),
							"hogehoge",
							BuildTarget.iOS, // Unity4系は、BuildTarget.iPhone になります.
							BuildOptions.None
						);
				}
				else
				{
						// 異常終了.
						Debug.LogWarning("ビルドすべきシーンが見つかりません。処理を終了します。");
						EditorApplication.Exit(1);
						return;
				}

				// 正常終了.
				EditorApplication.Exit(0);
		}
}