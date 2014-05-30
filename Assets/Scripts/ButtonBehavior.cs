using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour
{
	public object UserData
	{
		get; set;
	}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
	
	public void OnPress(bool isPressed)
	{
		if (isPressed)
		{
			if (Pressed != null)
				Pressed(this);
		}
		else
		{
			if (Released != null)
				Released(this);
		}
	}
	
	public void OnClick()
	{
		if (Clicked != null)
			Clicked(this);
	}
	
	public event EventHandler Clicked;
	public event EventHandler Pressed;
	public event EventHandler Released;
}
