//All Creds for this to http://jupiterlighthousestudio.com/custom-inspectors-unity/
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(TagSelectorAttribute))]
public class TagSelectorPropertyDrawer : PropertyDrawer
{
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		if (property.propertyType == SerializedPropertyType.String)
		{
			EditorGUI.BeginProperty(position, label, property);
			
			var attrib = this.attribute as TagSelectorAttribute;
			
			if (attrib.AllowUntagged)
			{
				property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
			}
			else
			{
				var tags = UnityEditorInternal.InternalEditorUtility.tags;
				var stag = property.stringValue;
				int index = -1;
				for(int i = 0; i < tags.Length; i++)
				{
					if(tags[i] == stag)
					{
						index = i;
						break;
					}
				}
				index = EditorGUI.Popup(position, label.text, index, tags);
				if(index >= 0)
				{
					property.stringValue = tags[index];
				}
				else
				{
					property.stringValue = null;
				}
			}
			
			EditorGUI.EndProperty();
		}
		else
		{
			EditorGUI.PropertyField(position, property, label);
		}
		
	}
	
}

