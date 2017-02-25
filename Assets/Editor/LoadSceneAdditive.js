@MenuItem("Tools/Load Selected Scene [Additive]") 
static function Apply () 
{ 
	var strScenePath : String = AssetDatabase.GetAssetPath(Selection.activeObject); 
	if (strScenePath == null) 
	{
		EditorUtility.DisplayDialog("Select Scene", "You Must Select a Scene first!", "Ok"); 
		return; 
	} 
	if (!strScenePath.Contains(".unity"))
	{
		EditorUtility.DisplayDialog("Select Scene","You Must Select a SCENE first, you selected "+strScenePath, "Ok"); 
		return; 
	}
 
	Debug.Log("Opening " + strScenePath + " additively"); 
	EditorApplication.OpenSceneAdditive(strScenePath); 
	return; 
}