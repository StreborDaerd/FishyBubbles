using UnityEngine;

using UnityEngine.UI;

//using System.Collections;



public class GameOverButtons : MonoBehaviour
{

	[ SerializeField ] private Gameplay _gameplay;
	
	[ SerializeField ] private Button _retireButtonGameOver;
	
	[ SerializeField ] private Button _replayButtonGameOver;
	
	
	
	
	public void Activate()
	{
		
		_replayButtonGameOver.interactable = true;
		
		_retireButtonGameOver.interactable = true;
		
	}
	
	
}
