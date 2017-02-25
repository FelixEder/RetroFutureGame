@MenuItem ("Tools/Select Char")
static function SelectMyObject() {
	var obj = GameObject.Find("Char");
	EditorGUIUtility.PingObject(obj);
	Selection.activeGameObject = obj;
}