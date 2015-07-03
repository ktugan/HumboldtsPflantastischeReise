using UnityEngine;


public class TagSelectorAttribute : PropertyAttribute
{
	public bool AllowUntagged;

	public TagSelectorAttribute( bool allowUntagged)
	{
		this.AllowUntagged = allowUntagged;
	}
	public TagSelectorAttribute()
	{

	}
}