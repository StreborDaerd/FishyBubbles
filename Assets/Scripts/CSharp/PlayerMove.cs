using UnityEngine;

using System.Collections;



public class PlayerMove : MonoBehaviour
{

	
	[ SerializeField ] private float _smoothX = 0.5f;
	
	[ SerializeField ] private float _smoothY = 0.5f;
	
	private float _velocityX = 0.0f;
	
	private float _velocityY = 0.0f;
	
	private Vector3 _lastTouchPo;
	
	private delegate void Movement();
	
	private Movement _movement;
	
	
	[ SerializeField ] private Transform _t;
	
	[ SerializeField ] private Camera _c;
	
	[ SerializeField ] private Renderer _left;
	
	[ SerializeField ] private Renderer _right;
	
	private bool _useJoyStick = true;
	
	[ SerializeField ] private JoyStick _joyStick;
	
	private float _joyStickSensitivity = 2.0f;
	
	private float _drift = 0.5f;

	[ SerializeField ] Collider2D _collider2D;
	
	
	void Start()
	{
		
		_lastTouchPo = _t.position;
		
		_movement = PauseUpdate;
		
	}
	
	
	
	public void SetInputParams( float sensitivity, float drift, bool useJoyStick, bool joystickLeftSide )
	{
		
		_smoothX = 1.5f - sensitivity;// / 0.9f;
		
		_smoothY = _smoothX;
		
		_drift = drift;
		
		_useJoyStick = useJoyStick;

		_joyStick.StartThis( joystickLeftSide );
		
	}
	
	
	
	void Update()
	{
		
		//Debug.Log( "Update " +_t.position.y );
		
		_movement();
		
	}
	
	
	
	public void Deactivate()
	{
		
		Pause();
		
		_collider2D.enabled = false;
		
		_left.enabled = false;
		
		_right.enabled = false;
		
	}
	
	
	
	public void Pause()
	{
		
		//Debug.Log( "Pause" );
		
		_joyStick.Deactivate();
		
		_movement = PauseUpdate;
		
	}
	
	
	
	public void Unpause()
	{
		
		//Debug.Log( "Unpause" );
		
		if( _useJoyStick )
		{
			
			Debug.Log( "Unpause useJoyStick" );
			
			_joyStick.Activate();
			
			_movement = MovementJoyStickRelease;
			
		}
		else
		{
			
			Debug.Log( "Unpause touchScreen" );
			
			_movement = MovementRelease;
			
		}
		
	}
	
	
	
	void PauseUpdate()
	{
		
		//Debug.Log( "PauseUpdate" );
		
	}
	
	
	
	void MovementTouch()
	{
//#if UNITY_EDITOR || UNITY_WEBPLAYER
		
		
		
		float x = Mathf.Clamp( Input.mousePosition.x / Screen.width, 0.05f, 0.95f );
		
		float y = Mathf.Clamp( Input.mousePosition.y / Screen.height, 0.1f, 0.9f );
		
		//Debug.Log( "x = " + x + " y = " + y );
		
		Vector3 nowMousePo = _c.ViewportToWorldPoint( new Vector3( x, y, 10.0f ) );
		
		if( Input.GetMouseButton(0) )
		{
			
			float newPoX = Mathf.SmoothDamp( _t.position.x, nowMousePo.x, ref _velocityX, _smoothX );
			
			float newPoY = Mathf.SmoothDamp( _t.position.y, nowMousePo.y, ref _velocityY, _smoothY );

			_t.position = new Vector3( newPoX, newPoY, 0.0f );
			
			if( _velocityX > 0.0f )
			{
				
				_right.enabled = true;
				
				_left.enabled = false;
				
			}
			else
			{
				
				_right.enabled = false;
				
				_left.enabled = true;
				
			}
			
		}
		else
		{
			
			_movement = MovementRelease;
			
		}
		
		//_lastTouchPo = nowMousePo;
		
		_lastTouchPo = new Vector3( _t.position.x  + ( nowMousePo.x - _t.position.x ) * _drift, _t.position.y + ( nowMousePo.y - _t.position.y ) * _drift, 0.0f );
		
		
//#endif
		
	}
	
	
	
	void MovementRelease()
	{
		
		
		float newPoX = Mathf.SmoothDamp( _t.position.x, _lastTouchPo.x, ref _velocityX, _smoothX );
		
		float newPoY = Mathf.SmoothDamp( _t.position.y, _lastTouchPo.y, ref _velocityY, _smoothY );
		
		//Debug.Log( "MovementRelease newPoX = " + newPoX + "   newPoY = " + newPoY );
		
		_t.position = new Vector3( newPoX, newPoY, 0.0f );
		
		
		
		
		
		if( Input.GetMouseButtonDown(0) )
		{
			
			if( ! UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) )
			{
				
				//Debug.Log( "MovementTouch" );
				
				_movement = MovementTouch;
				
			}
			
		}
		
		
	}
	
	
	
	public void JoyStickMovement()
	{
		
		
		
		//
		
		//Vector3 nowMousePo = _c.ViewportToWorldPoint( new Vector3( x, y, 10.0f ) );
		
		Vector2 joyStickMovement = _joyStick.GetJoystickMovement();
		
		//float x = Mathf.Clamp( Mathf.Pow( joyStickMovement.x + 0.5f, _joyStickSensitivity ), 0.05f, 0.95f );
		
		//float y = Mathf.Clamp( Mathf.Pow( joyStickMovement.y + 0.5f, _joyStickSensitivity ), 0.1f, 0.9f );
		
		float x = Mathf.Clamp( joyStickMovement.x + 0.5f, 0.05f, 0.95f );
		
		float y = Mathf.Clamp( joyStickMovement.y + 0.5f, 0.1f, 0.9f );
		
		//Debug.Log( "x = " + x + " y = " + y );
		
		Vector3 nowMousePo = _c.ViewportToWorldPoint( new Vector3( x, y, 10.0f ) );
		
		if( Input.GetMouseButton(0) )
		{
			
			float newPoX = Mathf.SmoothDamp( _t.position.x, nowMousePo.x, ref _velocityX, _smoothX );
			
			float newPoY = Mathf.SmoothDamp( _t.position.y, nowMousePo.y, ref _velocityY, _smoothY );

			_t.position = new Vector3( newPoX, newPoY, 0.0f );
			
			if( _velocityX > 0.0f )
			{
				
				_right.enabled = true;
				
				_left.enabled = false;
				
			}
			else
			{
				
				_right.enabled = false;
				
				_left.enabled = true;
				
			}
			
			
			
		}
		else
		{
			
			_movement = MovementJoyStickRelease;
			
		}
		
		//_lastTouchPo = new Vector3( ( nowMousePo.x - _t.position.x ) * _drift, ( nowMousePo.y - _t.position.y ) * _drift, 0.0f );
		
		_lastTouchPo = new Vector3( _t.position.x  + ( nowMousePo.x - _t.position.x ) * _drift, _t.position.y + ( nowMousePo.y - _t.position.y ) * _drift, 0.0f );
		
		
		/*
		Debug.Log( "joyStickMovement.x = " + joyStickMovement.x + "   joyStickMovement.y = " + joyStickMovement.y );
		
		float newPoX = joyStickMovement.x + _t.position.x;
		
		float newPoY = joyStickMovement.y + _t.position.y;
		
		newPoX = Mathf.SmoothDamp( _t.position.x, newPoX, ref _velocityX, _smoothX );
		
		newPoY = Mathf.SmoothDamp( _t.position.y, newPoY, ref _velocityY, _smoothY );
		
		//Debug.Log( "newPoX = " + newPoX + "   newPoY = " + newPoY );
		
		_t.position = new Vector3( newPoX, newPoY, 0.0f );*/
		
	}
	
	
	
	public void JoyStickMoveActivate()
	{
		
		_movement = JoyStickMovement;
		
	}
	
	
	
	void MovementJoyStickRelease()
	{
		
		float newPoX = Mathf.SmoothDamp( _t.position.x, _lastTouchPo.x, ref _velocityX, _smoothX );
		
		float newPoY = Mathf.SmoothDamp( _t.position.y, _lastTouchPo.y, ref _velocityY, _smoothY );
		
		//Debug.Log( "MovementRelease newPoX = " + newPoX + "   newPoY = " + newPoY );
		
		_t.position = new Vector3( newPoX, newPoY, 0.0f );
		
		
		
		/*
		
		if( Input.GetMouseButtonDown(0) )
		{
			
			if( ! UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) )
			{
				
				//Debug.Log( "MovementTouch" );
				
				_movement = JoyStickMovement;
				
			}
			
		}
		*/
		
	}
	
	
	
	public float Velocity()
	{
		
		return _velocityX;
		
	}
	
	
	
	/*
	
	var moIn : boolean = false;
private var life : int = 5;
var hearts : PlayImagePlaneSequence[];
var chaseCamera : Transform;
function Update()
{
	if( moIn )
	{
		if( Input.GetMouseButton(0) )
		{
			Movement();
		}
		else
		{
			MovementRelease();
		}
	}
	else
	{
		if( Input.GetMouseButton(0) )
		{
			Movement();
		}
		else
		{
			MovementRelease();
		}
	}
}

private var smoothTime = 0.5;
private var currentAccelerationX = 0.0;
private var currentAccelerationY = 0.0;
private var lastMousePo : Vector3 = Vector3( 0.0, -15.0, 0.0 );

var fergusGoLeft : Renderer;
var fergusGoRight : Renderer;

function Movement()
{
	var nowMousePo : Vector3 = Camera.main.ViewportToWorldPoint( Vector3( Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0 ) );
	//var nowMousePo : Vector3 = 
	if( moIn )
	{
		nowMousePo = Camera.main.ViewportToWorldPoint( Vector3( Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0 ) );
	}
	else
	{
		nowMousePo = Camera.main.ViewportToWorldPoint( Vector3( Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0 ) );
		
	}
	
	var newPoX : float = Mathf.SmoothDamp( transform.position.x, nowMousePo.x, currentAccelerationX, smoothTime );

	var newPoY : float = Mathf.SmoothDamp( transform.position.y, nowMousePo.y, currentAccelerationY, smoothTime );

	transform.position = Vector3( newPoX, newPoY, 1 );
	if( currentAccelerationX > 0 )
	{
		fergusGoRight.enabled = true;
		fergusGoLeft.enabled = false;
	}
	else
	{
		fergusGoRight.enabled = false;
		fergusGoLeft.enabled = true;
	}
	lastMousePo = nowMousePo;
}

function MovementRelease()
{
	var nowMousePo : Vector3 = lastMousePo;
	
	var newPoX : float = Mathf.SmoothDamp( transform.position.x, nowMousePo.x, currentAccelerationX, smoothTime );

	var newPoY : float = Mathf.SmoothDamp( transform.position.y, nowMousePo.y, currentAccelerationY, smoothTime );

	transform.position = Vector3( newPoX, newPoY, 0 );
	if( currentAccelerationX > 0 )
	{
		fergusGoRight.enabled = true;
		fergusGoLeft.enabled = false;
	}
	else
	{
		fergusGoRight.enabled = false;
		fergusGoLeft.enabled = true;
	}
}
	*/
	
}
