using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class NavMeshRenderer : MonoBehaviour {
	
	/** Last level loaded. Used to check for scene switches */
	string lastLevel = "";
	
	/** Used to get rid of the compiler warning that #lastLevel is not used */
	public string SomeFunction () {
		return lastLevel;
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		if (lastLevel == "") {
#pragma warning disable CS0618 // Type or member is obsolete
            lastLevel = EditorApplication.currentScene;
#pragma warning restore CS0618 // Type or member is obsolete
        }

#pragma warning disable CS0618 // Type or member is obsolete
        if ( lastLevel != EditorApplication.currentScene) {
#pragma warning restore CS0618 // Type or member is obsolete
            DestroyImmediate (gameObject);
		}
		#endif
	}
}