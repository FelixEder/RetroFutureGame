using UnityEditor;
using UnityEngine;

public class GroupCommand : EditorWindow {
	string groupName;

	[MenuItem("Tools/Group Selected %g")]
	static void Init () {
		GroupCommand window = (GroupCommand)EditorWindow.GetWindow (typeof(GroupCommand));
		window.Show ();
	}

	void OnGUI () {
		EditorGUIUtility.labelWidth = 80;
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		GUI.SetNextControlName ("input");
		groupName = EditorGUILayout.TextField ("Group Name", groupName);

		EditorGUILayout.Space ();
		GUILayout.BeginHorizontal ();

		GroupCommand window = (GroupCommand)EditorWindow.GetWindow (typeof(GroupCommand));
		Event e = Event.current;
		if (GUILayout.Button ("Close", GUILayout.Width (100))) {
			window.Close ();
		}
		if (GUILayout.Button ("Create Group", GUILayout.Width (200)) || e.keyCode == KeyCode.Return) {
			GroupSelected ();
			window.Close ();
		}

		GUILayout.FlexibleSpace ();
		EditorGUI.FocusTextInControl ("input");
	}

	private void GroupSelected () {
		if (!Selection.activeTransform) return;
		var go = new GameObject (groupName);
		Undo.RegisterCreatedObjectUndo (go, "Group Selected");
		go.transform.SetParent (Selection.activeTransform.parent, false);
		foreach (var transform in Selection.transforms) Undo.SetTransformParent (transform, go.transform, "Group Selected");
		Selection.activeGameObject = go;
	}
}


/*
using UnityEditor;
using UnityEngine;

public static class GroupCommand {
	[MenuItem("Tools/Group Selected %g")]
	private static void GroupSelected() {
		if (!Selection.activeTransform) return;
		var go = new GameObject(Selection.activeTransform.name + " Group");
		Undo.RegisterCreatedObjectUndo(go, "Group Selected");
		go.transform.SetParent(Selection.activeTransform.parent, false);
		foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
		Selection.activeGameObject = go;
	}
}
*/