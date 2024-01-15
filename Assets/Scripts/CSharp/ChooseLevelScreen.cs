using UnityEngine;

using UnityEngine.UI;

using System.Collections;



public class ChooseLevelScreen : MonoBehaviour
{

	[ SerializeField ] private Button[] _buttons;
	
	private LoadingScreen _loadingScreen;
	
	
	[ SerializeField ] private Button _blueButton;
	
	[ SerializeField ] private Button _yellowButton;
	
	[ SerializeField ] private Button _redButton;
	
	[ SerializeField ] private Button _greenButton;
	
	[ SerializeField ] private Button _purpleButton;
	
	[ SerializeField ] private Button _orangeButton;
	
	
	
	private bool _yellowChosen;
	
	private bool _redChosen;
	
	private bool _greenChosen;
	
	private bool _purpleChosen;
	
	private bool _orangeChosen;
	
	
	private Options _options;
	
	
	void Start()
	{
		
		GameObject loadingScreenGameObject = GameObject.Find( "LoadingScreen" );
		
		if( loadingScreenGameObject != null )
		{
			
			//Debug.Log( "loadingScreen found" );
			
			_loadingScreen = loadingScreenGameObject.GetComponent< LoadingScreen >();
			
			_options = _loadingScreen.GetOptions();
			
			
			bool[] chosen = _options.GetChosen();
			
			Debug.Log( "yellow" + chosen[0] + "   red" + chosen[1] + "   green" + chosen[2] + "   purple" + chosen[3] + "   orange" + chosen[4] );
			
			Debug.Log( "!yellow" + !chosen[0] + "   !red" + !chosen[1] + "   !green" + !chosen[2] + "   !purple" + !chosen[3] + "   !orange" + !chosen[4] );
			
			_yellowChosen = ! chosen[0];
			
			YellowButton();
			
			_redChosen = ! chosen[1];
			
			RedButton();
			
			_greenChosen = ! chosen[2];
			
			GreenButton();
			
			_purpleChosen = ! chosen[3];
			
			PurpleButton();
			
			_orangeChosen = ! chosen[4];
			
			OrangeButton();
	
			
		}
		
		
	}
	
	
	
	void DeactivateAllButtons()
	{
		
		for( int i = 0; i < _buttons.Length; i ++ )
		{
			
			_buttons[i].interactable = false;
			
		}
		
	}
	
	
	
	public void Back()
	{
		
		DeactivateAllButtons();
		
		_options.SetChosen( _yellowChosen, _redChosen, _greenChosen, _purpleChosen, _orangeChosen );
		
		_options.SaveColourChoices();
		
		_loadingScreen.Activate( "Instructions" );
		
	}
	
	
	
	public void Begin()
	{
		
		DeactivateAllButtons();
		
		_options.SetChosen( _yellowChosen, _redChosen, _greenChosen, _purpleChosen, _orangeChosen );
		
		_options.SaveColourChoices();
		
		_loadingScreen.Activate( "Level" );
		
	}
	
	
	
	public void YellowButton()
	{
		
		if( _yellowChosen )
		{
			
			Debug.Log( "YellowButton() _yellowChosen = false" );
			
			_yellowChosen = false;
			
			ColorBlock cb = _yellowButton.colors;
			
			cb.colorMultiplier = 1.0f;
			
			_yellowButton.colors = cb;
			
			_greenChosen = true;
			
			GreenButton();
			
			_greenButton.interactable = false;
			
			_orangeChosen = true;
			
			OrangeButton();
			
			_orangeButton.interactable = false;
			
		}
		else
		{
			
			Debug.Log( "YellowButton() _yellowChosen = true" );
			
			_yellowChosen = true;
			
			ColorBlock cb = _yellowButton.colors;
			
			cb.colorMultiplier = 1.9f;
			
			_yellowButton.colors = cb;
			
			_greenButton.interactable = true;
			
			if( _redChosen )
			{
				
				_orangeButton.interactable = true;
				
			}
			
		}
		
	}
	
	
	
	public void RedButton()
	{
		
		if( _redChosen )
		{
			
			Debug.Log( "RedButton() _redChosen = false" );
			
			_redChosen = false;
			
			ColorBlock cb = _redButton.colors;
			
			cb.colorMultiplier = 1.0f;
			
			_redButton.colors = cb;
			
			_purpleChosen = true;
			
			PurpleButton();
			
			_purpleButton.interactable = false;
			
			_orangeChosen = true;
			
			OrangeButton();
			
			_orangeButton.interactable = false;
			
		}
		else
		{
			
			Debug.Log( "RedButton() _redChosen = true" );
			
			_redChosen = true;
			
			ColorBlock cb = _redButton.colors;
			
			cb.colorMultiplier = 1.75f;
			
			_redButton.colors = cb;
			
			_purpleButton.interactable = true;
			
			if( _yellowChosen )
			{
				
				_orangeButton.interactable = true;
				
			}
			
		}
		
	}
	
	
	
	public void GreenButton()
	{
		
		if( _greenChosen )
		{
			
			Debug.Log( "GreenButton() _greenChosen = false" );
			
			_greenChosen = false;
			
			ColorBlock cb = _greenButton.colors;
			
			cb.colorMultiplier = 1.0f;
			
			_greenButton.colors = cb;
			
		}
		else
		{
			
			Debug.Log( "GreenButton() _greenChosen = true" );
			
			_greenChosen = true;
			
			ColorBlock cb = _greenButton.colors;
			
			cb.colorMultiplier = 1.75f;
			
			_greenButton.colors = cb;
			
		}
		
	}
	
	
	
	public void PurpleButton()
	{
		
		if( _purpleChosen )
		{
			
			Debug.Log( "PurpleButton() _purpleChosen = false" );
			
			_purpleChosen = false;
			
			ColorBlock cb = _purpleButton.colors;
			
			cb.colorMultiplier = 1.0f;
			
			_purpleButton.colors = cb;
			
		}
		else
		{
			
			Debug.Log( "PurpleButton() _purpleChosen = true" );
			
			_purpleChosen = true;
			
			ColorBlock cb = _purpleButton.colors;
			
			cb.colorMultiplier = 1.75f;
			
			_purpleButton.colors = cb;
			
		}
		
	}
	
	
	
	public void OrangeButton()
	{
		
		if( _orangeChosen )
		{
			
			Debug.Log( "OrangeButton() _orangeChosen = false" );
			
			_orangeChosen = false;
			
			ColorBlock cb = _orangeButton.colors;
			
			cb.colorMultiplier = 1.0f;
			
			_orangeButton.colors = cb;
			
		}
		else
		{
			
			Debug.Log( "OrangeButton() _orangeChosen = true" );
			
			_orangeChosen = true;
			
			ColorBlock cb = _orangeButton.colors;
			
			cb.colorMultiplier = 1.75f;
			
			_orangeButton.colors = cb;
			
		}
		
	}
	
	
}
