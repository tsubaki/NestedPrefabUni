using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AttatchedPrefab : MonoBehaviour {
	
	#if UNITY_EDITOR
	
	void Start () {
		var pip = gameObject.AddComponent<PIP>();
		pip.basePrefab =(GameObject) UnityEditor.PrefabUtility.GetPrefabParent(gameObject);
		
		DestroyImmediate (this);
	}
	#endif
}