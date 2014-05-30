using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PIP : MonoBehaviour
{
	#if UNITY_EDITOR


	System.Type[] ignoreType =
	new System.Type[]{
		typeof( PIP),
		typeof( Transform),
		typeof( AttatchedPrefab),
	};


	[HideInInspector]
	public GameObject basePrefab;
	
	void Update()
	{
		UpdateComponents();
	}
	
	[ContextMenu("Reset Prefab")]
	void UpdateComponents()
	{
		UpdateComponent(basePrefab, gameObject);
	}
	
	[ContextMenu("Apply Prefab")]
	void ApplyComponents()
	{
		UpdateComponent(gameObject, basePrefab);
	}

	bool IgnoreComponentType(Component component)
	{
		for( int i=0; i< ignoreType.Length; i++)
		{
			var type = ignoreType[i];
			if( component.GetType() == type )
			{
				return true;
			}
		}

		return false;
	}




	
	void UpdateComponent(GameObject baseObject, GameObject postObject)
	{
		var baseComponents = new List<Component>(baseObject.GetComponents(typeof(Component)));
		var postComponents = new List<Component>( postObject.GetComponents(typeof(Component)) );
		
		foreach( var component in baseComponents){

			if( IgnoreComponentType(component)){
				continue;
			}

			var targetComponent =  postComponents.Find( (item )=> item.GetType() == component.GetType ());
			
			UnityEditorInternal.ComponentUtility.CopyComponent(component);
			
			if( targetComponent != null ){
				UnityEditorInternal.ComponentUtility.PasteComponentValues(targetComponent);
			}else{
				UnityEditorInternal.ComponentUtility.PasteComponentAsNew(gameObject);
			}
		}
		
		
		foreach( var component in postComponents ){

			if( IgnoreComponentType(component)){
				continue;
			}

			var targetComponent =  baseComponents.Find( (item )=> item.GetType() == component.GetType ());
			if( targetComponent == null )
			{
				DestroyImmediate (component);
			}
		}
	}
	#endif
}