using UnityEngine;

using System.Collections;



public class PanelAnimation : MonoBehaviour
{

	[ SerializeField ] private Gameplay _gameplay;
	
	
	
	void Pause()
	{
		
		_gameplay.PauseAnimation();
		
	}
	
	
	
	void Unpause()
	{
		
		_gameplay.UnpauseAnimation();
		
	}
	
}
