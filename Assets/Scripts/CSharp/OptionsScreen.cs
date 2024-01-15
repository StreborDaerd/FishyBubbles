using UnityEngine;

using System.Collections;

using UnityEngine.UI;



public class OptionsScreen : MonoBehaviour
{

	private LoadingScreen _loadingScreen;
	
	[ SerializeField ] private Slider _inputSensitivitySlider;
	
	[ SerializeField ] private Slider _percentBadSlider;
	
	[ SerializeField ] private Slider _bubbleSpeedSlider;
	
	[ SerializeField ] private Slider _playerDriftSlider;
	
	[ SerializeField ] private Slider _bubbleDistanceSlider;
	
	
	
	[ SerializeField ] private Toggle _joyStickToggle;
	
	[ SerializeField ] private Toggle _touchScreenToggle;
	
	
	
	[ SerializeField ] private Toggle _joyStickLeftToggle;
	
	[ SerializeField ] private Toggle _joyStickRightToggle;
	
	
	private Options _options;
	
	
	
	
	void Start()
	{
		
		_inputSensitivitySlider.onValueChanged.AddListener( SetInputSensitivity );
		
		_percentBadSlider.onValueChanged.AddListener( SetPercentBad );
		
		_bubbleSpeedSlider.onValueChanged.AddListener( SetBubblesSpeed );
		
		_playerDriftSlider.onValueChanged.AddListener( SetPlayerDrift );
		
		_bubbleDistanceSlider.onValueChanged.AddListener( SetMaxBubbleDistance );
		
		
		_joyStickToggle.onValueChanged.AddListener( SetJoyStick );
		
		_touchScreenToggle.onValueChanged.AddListener( SetTouchScreen );
		
		
		_joyStickLeftToggle.onValueChanged.AddListener( SetJoyStickLeft );
		
		_joyStickRightToggle.onValueChanged.AddListener( SetJoyStickRight );
		
		
		GameObject loadingScreenGameObject = GameObject.Find( "LoadingScreen" );
		
		if( loadingScreenGameObject != null )
		{
			
			Debug.Log( "loadingScreen found" );
			
			_loadingScreen = loadingScreenGameObject.GetComponent< LoadingScreen >();
			
			_options = _loadingScreen.GetOptions();
			
			
			_inputSensitivitySlider.normalizedValue = _options.InputSensitivity;
			
			_percentBadSlider.normalizedValue = _options.PercentOfBad;
			
			_bubbleSpeedSlider.normalizedValue = _options.StartVelocity;
			
			_playerDriftSlider.normalizedValue = _options.PlayerDrift;
			
			_bubbleDistanceSlider.normalizedValue = _options.MaxBubbleDistance;
			
			
			if( _options.JoyStick )
			{
				
				_joyStickToggle.isOn = true;
				
				//SetJoyStick( true );
				
			}
			else
			{
				
				_touchScreenToggle.isOn = true;
				
				//SetTouchScreen( true );
				
			}
			
			if( _options.JoyStickLeft )
			{
				
				_joyStickLeftToggle.isOn = true;
				
			}
			else
			{
				
				_joyStickRightToggle.isOn = true;
				
			}
			
		}
		/*else
		{
			
			Debug.Log( "Why the fuck was this just working before?" );
			
			Debug.Log( "And why the fuck does this just work as soon as I put in an \"else\" statement?" );
			
		}
		
		Debug.Log( "Hello this is the Start function. Apparently references that have been set in me can't be used in the Awake function. Why the FUCK not do you think?" );*/
		
	}
	
	
	
	/*void Awake()
	{
		
		Debug.Log( "Hello this is the Awake function. Apparently I can't use references that have been set in the Start function. Why the FUCK not do you think?" );
		
		Debug.Log( "Well apperntly Awake comes before Start. Why the FUCK do I keep forgetting that? Probably because I never use Awake, unless I really have to." );
		
	}*/
	
	
	
	public void Back()
	{
		
		_options.SaveOptions();
		
		_loadingScreen.Activate( "MainMenu" );
		
		
	}
	
	
	
	public void SetInputSensitivity( float f )
	{
		
		Debug.Log( "SetInputSensitivity = " + f );
		
		_options.InputSensitivity = f;
		
		
	}
	
	
	
	public void SetPercentBad( float f )
	{
		
		Debug.Log( "SetPercentBad = " + f );
		
		_options.PercentOfBad = f;
		
		
	}
	
	
	
	public void SetBubblesSpeed( float f )
	{
		
		Debug.Log( "SetBubblesSpeed = " + f );
		
		_options.StartVelocity = f;
		
		
	}
	
	
	
	public void SetPlayerDrift( float f )
	{
		
		Debug.Log( "SetPlayerDrift = " + f );
		
		_options.PlayerDrift = f;
		
		
	}
	
	
	
	public void SetMaxBubbleDistance( float f )
	{
		
		Debug.Log( "SetMaxBubbleDistance = " + f );
		
		_options.MaxBubbleDistance = f;
		
		
	}
	
	
	
	public void SetJoyStick( bool b )
	{
		
		
		
		if( b )
		{
			
			Debug.Log( "SetJoyStick = " + b );
			
			_options.JoyStick = true;
			
		}
		
		
	}
	
	
	
	public void SetTouchScreen( bool b )
	{
		
		if( b )
		{
			
			Debug.Log( "SetTouchScreen = " + b );
			
			_options.JoyStick = false;
			
		}
		
	}

/*
	public void SetJoyStickLeft()
	{
		
		Debug.Log( "SetJoyStickLeft = true" );

		_options.JoyStickLeft = true;
		
	}
	
	
	
	public void SetJoyStickRight()
	{
		
		Debug.Log( "SetJoyStickLeft = false" );

		_options.JoyStickLeft = false;
		
	}
*/
	
	public void SetJoyStickLeft( bool b )
	{
		
		if( b )
		{
			
			Debug.Log( "SetJoyStickLeft = " + b );
			
			_options.JoyStickLeft = true;
			
		}
		
	}
	
	public void SetJoyStickRight( bool b )
	{
		
		if( b )
		{
			
			Debug.Log( "SetJoyStickRight = " + b );
			
			_options.JoyStickLeft = false;
			
		}
		
	}
	
	
}
