using UnityEngine;

using System.Collections;

using System.Text.RegularExpressions;



public class Options : MonoBehaviour
{

	//control options
	
	private float _inputSensitivity = 0.5f;
	
	private bool _joyStick = false;
	
	private bool _joyStickLeft = false;
	
	//gameplay options
	
	private int _difficulty = 0;
	
	//development options, remove for production
	
	private float _percentOfBad = 0.25f;
	
	private float _startVelocity = 0.55f;
	
	private float _drift = 0.5f;
	
	private float _maxBubbleDistance = 0.5f;
	
	//chosen bubble colours, blue is always chosen
	
	private bool[] _chosen = new bool[5];
	
	private bool _yellowChosen;
	
	private bool _redChosen;
	
	private bool _greenChosen;
	
	private bool _purpleChosen;
	
	private bool _orangeChosen;
	
	
	
	void Start()
	{
		
		LoadOptions();
		
		LoadColourChoices();
		
	}
	
	
	
	void LoadOptions()
	{
		
		//control options
		
		_inputSensitivity = PlayerPrefs.GetFloat( "inputSensitivity", 0.5f );
		
		int b = PlayerPrefs.GetInt( "joyStick", 0 );
		
		if( b == 1 )
		{
			
			_joyStick = true;
			
		}
		
		Debug.Log( "b = " + b );
		
		int l = PlayerPrefs.GetInt( "joyStickLeft", 0 );
		
		if( l == 1 )
		{
			
			_joyStickLeft = true;
			
		}

		//gameplay options
		
		_difficulty = PlayerPrefs.GetInt( "difficulty", 0 );
		
		
		//development options
		
		_percentOfBad = PlayerPrefs.GetFloat( "percentOfBad", 0.25f );
		
		_startVelocity = PlayerPrefs.GetFloat( "startVelocity", 0.55f );
		
		_drift = PlayerPrefs.GetFloat( "drift", 0.5f );
		
		_maxBubbleDistance = PlayerPrefs.GetFloat( "bubbleDistance", 0.5f );
		
	}
	
	
	
	public void SaveOptions()
	{
		
		PlayerPrefs.SetFloat( "inputSensitivity", _inputSensitivity );
		
		int b = 0;
		
		if( _joyStick )
		{
			
			b = 1;
			
		}
		
		Debug.Log( "b = " + b );
		
		PlayerPrefs.SetInt( "joyStick", b );

		int l = 0;
		
		if( _joyStickLeft )
		{
			
			l = 1;
			
		}
		
		Debug.Log( "l = " + l );
		
		PlayerPrefs.SetInt( "joyStickLeft", l );
		
		
		//development options
		
		PlayerPrefs.SetFloat( "percentOfBad", _percentOfBad );
		
		PlayerPrefs.SetFloat( "startVelocity", _startVelocity );
		
		PlayerPrefs.SetFloat( "drift", _drift );
		
		PlayerPrefs.SetFloat( "bubbleDistance", _maxBubbleDistance );
		
	}
	
	
	
	public void SaveGameplayOptions()
	{
		
		//gameplay options
		
		PlayerPrefs.SetInt( "difficulty", _difficulty );
		
	}
	
	
	
	public float InputSensitivity
	{
		
		set
		{
			
			_inputSensitivity = value;
			
		}
		
		get
		{
			
			return _inputSensitivity;
			
		}
		
	}
	
	
	
	public void LoadColourChoices()
	{
		
		string chosen = PlayerPrefs.GetString( "chosen", "000000" );
		
		//Debug.Log( "chosen = " + chosen );
		
		string[] strings = Regex.Split( chosen, string.Empty );
		
		for( int i = 1; i < 6; i ++ )//need to start at 1 not 0 because of some crazy shit where it looks like if you have an empty assignment to a string eg string s = "";
			//the emtpy assignment counts as a separate character that the string can be split into, or maybe it's because I am using string.Empty
		{
			//Debug.Log( "i = " + i + "   strings[i] = " + strings[i] );
			if( strings[i] == "1" )
			{
				
				_chosen[ i - 1 ] = true;
				
			}
			
		}
		
	}
	
	
	
	public void SaveColourChoices()
	{
		
		string chosen = "";
		
		//string[] strings = Regex.Split( chosen, string.Empty );
		
		for( int i = 0; i < 5; i ++ )
		{
			
			if( _chosen[i] )
			{
				
				chosen += "1";
				
			}
			else
			{
				
				chosen += "0";
				
			}
			
		}
		
		PlayerPrefs.SetString( "chosen", chosen );
		
		Debug.Log( "chosen = " + chosen );
		
	}
	
	
	
	public bool JoyStick
	{
		
		set
		{
			
			_joyStick = value;
			
		}
		
		get
		{
			
			return _joyStick;
			
		}
		
	}
	
	
	
	public bool JoyStickLeft
	{
		
		set
		{
			
			_joyStickLeft = value;
			
		}
		
		get
		{
			
			return _joyStickLeft;
			
		}
		
	}
	
	
	
	public float PercentOfBad
	{
		
		set
		{
			
			_percentOfBad = value;
			
		}
		
		get
		{
			
			return _percentOfBad;
			
		}
		
	}
	
	
	
	public float StartVelocity
	{
		
		set
		{
			
			_startVelocity = value;
			
		}
		
		get
		{
			
			return _startVelocity;
			
		}
		
	}
	
	
	
	public float PlayerDrift
	{
		
		set
		{
			
			_drift = value;
			
		}
		
		get
		{
			
			return _drift;
			
		}
		
	}
	
	
	
	public float MaxBubbleDistance
	{
		
		set
		{
			
			_maxBubbleDistance = value;
			
		}
		
		get
		{
			
			return _maxBubbleDistance;
			
		}
		
	}
	
	
	
	public void SetChosen( bool y, bool r, bool g, bool p, bool o )
	{
		
		_chosen = new bool[]{ y, r, g, p, o };
		
		/*
		_yellowChosen = y;
		
		_redChosen = r;
		
		_greenChosen = g;
		
		_purpleChosen = p;
		
		_orangeChosen = o;
		*/
		
	}
	
	
	
	public bool[] GetChosen()
	{
		
		//Debug.Log( "yellow" + _chosen[0] + "   red" + _chosen[1] + "   green" + _chosen[2] + "   purple" + _chosen[3] + "   orange" + _chosen[4] );
		
		return _chosen;
		
		//return new bool[]{ _yellowChosen, _redChosen, _greenChosen, _purpleChosen, _orangeChosen };
		
	}
	
	
}
