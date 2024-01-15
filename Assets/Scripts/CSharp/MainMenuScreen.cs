using UnityEngine;

using UnityEngine.UI;

using System.Collections;



public class MainMenuScreen : MonoBehaviour
{
	
	[ SerializeField ] private Button[] _buttons;
	
	private LoadingScreen _loadingScreen;
	
	
	
	void Start()
	{
		
		GameObject loadingScreenGameObject = GameObject.Find( "LoadingScreen" );
		
		if( loadingScreenGameObject != null )
		{
			
			//Debug.Log( "loadingScreen found" );
			
			_loadingScreen = loadingScreenGameObject.GetComponent< LoadingScreen >();
			
		}
		
	}
	
	
	
	public void Play()
	{
		
		//Debug.Log( "MainMenuScreen Play" );
		
		DeactivateAllButtons();
		
		_loadingScreen.Activate( "Instructions" );
		
		
		
	}
	
	
	
	public void Score()
	{
		
		//Debug.Log( "MainMenuScreen Score" );
		
		DeactivateAllButtons();
		
		_loadingScreen.Activate( "Score" );
		
	}
	
	
	
	public void Options()
	{
		
		//Debug.Log( "MainMenuScreen Options" );
		
		DeactivateAllButtons();
		
		_loadingScreen.Activate( "Options" );
		
	}
	
	
	
	void DeactivateAllButtons()
	{
		
		for( int i = 0; i < _buttons.Length; i ++ )
		{
			
			_buttons[i].interactable = false;
			
		}
		
	}

	public void Exit()
	{
		Application.Quit();
	}
	
}
