using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// SceneViewWindow class.
/// </summary>
public class SceneViewWindow : EditorWindow
{
	/// <summary>
	/// Tracks scroll position.
	/// </summary>
	private Vector2 scrollPos;

	/// <summary>
	/// Initialize window state.
	/// </summary>
	[MenuItem("Tools/Scene View...")]
	internal static void Init()
	{
		// EditorWindow.GetWindow() will return the open instance of the specified window or create a new
		// instance if it can't find one. The second parameter is a flag for creating the window as a
		// Utility window; Utility windows cannot be docked like the Scene and Game view windows.
		var window = (SceneViewWindow)GetWindow(typeof(SceneViewWindow), false, "Scene View");
		window.position = new Rect(window.position.xMin + 100f, window.position.yMin + 100f, 200f, 400f);
	}

	/// <summary>
	/// Called on GUI events.
	/// </summary>
	internal void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos, false, false);

		GUILayout.Label("Scenes In Build", EditorStyles.boldLabel);
		for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
			var scene = EditorBuildSettings.scenes[i];
			if (scene.enabled)
			{
				var sceneName = Path.GetFileNameWithoutExtension(scene.path);
				var pressed = GUILayout.Button(i + ": " + sceneName, new GUIStyle(GUI.skin.GetStyle("Button")) { alignment = TextAnchor.MiddleLeft });
				if (pressed)
				{
					if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
					{
						EditorSceneManager.OpenScene(scene.path);
					}
				}
			}
		}

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}
}