using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class JoyStick : MonoBehaviour
{
	[ SerializeField ] private Rect _moveLimits;
	
	[ SerializeField ] private float _moveRange = 1.0f;
	
	private float _moveRangeSquared;
	
	
	[ SerializeField ] private Transform _knob;
	
	private Vector3 _knobStartPo;
	
	private Vector3 _knobHitPo;
	
	
	[ SerializeField ] private Collider _knobCollider;
	
	
	private bool _movingPlayer = false;
	
	[ SerializeField ] Camera _c;

	[ SerializeField ] Camera _mainCamera;
	
	[ SerializeField ] private PlayerMove _playerMove;
	
	
	private float _xMove = 0.0f;
	
	private float _yMove = 0.0f;
	
	
	private delegate void _Update();
	
	private _Update _update;
	
	bool _left = false;
	
	
	public void StartThis( bool left )
	{
		
		Deactivate();
		
		_knobStartPo = _knob.position;
		
		_moveRangeSquared = _moveRange * _moveRange;
		
		float frustumYOffset = _c.transform.position.z - _mainCamera.transform.position.z * Mathf.Tan( _mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad );
		float frustumXOffset = frustumYOffset * _mainCamera.aspect;

		_left = left;

		if( _left )
		{
			_c.transform.position = new Vector3( - frustumXOffset - 0.5f * _moveRange, - frustumYOffset - _moveRange, _c.transform.position.z );
		}
		else
		{
			_c.transform.position = new Vector3( frustumXOffset + 0.5f * _moveRange, - frustumYOffset - _moveRange, _c.transform.position.z );
		}
	}
	
	
	
	void Update()
	{
		
		//Debug.Log( "Update " +_t.position.y );
		
		_update();
		
	}
	
	
	
	public void Activate()
	{
		
		_update = Active;
		
		_c.enabled = true;
		
		_knobCollider.enabled = true;
		
	}
	
	
	
	public void Deactivate()
	{
		
		_update = Inactive;
		
		_c.enabled = false;
		
		_knobCollider.enabled = false;
		
	}
	
	
	
	public void Inactive()
	{
		
		
		
	}
	
	

	void Active2()
	{
		
		if( _movingPlayer )
		{
			
			if( Input.touchCount > 0 && ( Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary ) )
			{
				
				MoveKnob();
				
			}
			else //if( Input.GetMouseButtonUp(0) )
			{
				
				_movingPlayer = false;
				
				_knob.position = _knobStartPo;
				
			}

		}
			
		if( Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began )
		{
        	//DebugLog( "TouchPhase.Began " + Input.GetTouch(0).position );
		
			int layerMask = 1 << 13;
			
			Ray ray = Camera.main.ScreenPointToRay( Input.GetTouch(0).position );
			
			RaycastHit hit;
			
	        //if( Physics.Raycast( ray, 100 ) )
			if( Physics.Raycast( ray, out hit, 100, layerMask ) )
			{
	            
				//DebugLog( "Hit something" );
				
				_playerMove.JoyStickMoveActivate();
				
				_movingPlayer = true;
				
				_knobHitPo = hit.point;
				
			}
			
		}
		
		
	}

#if UNITY_ANDROID
	void Active1()
	{
		int tc = Input.touchCount;

		if( tc > 0 )
		{
			Touch touch = Input.GetTouch(0);

			TouchPhase phase = touch.phase;

			if( phase == TouchPhase.Began )
			{
				int layerMask = 1 << 13;
				Ray ray = Camera.main.ScreenPointToRay( touch.position );
				RaycastHit hit;

				if( Physics.Raycast( ray, out hit, 100, layerMask ) )
				{
	            
					//Debug.Log( "Hit something" );
				
					_playerMove.JoyStickMoveActivate();
				
					_movingPlayer = true;
				
					_knobHitPo = hit.point;
				
				}
			}
			else
			{
				MoveKnob();
			}

		}
		else if( _movingPlayer )
		{
			_movingPlayer = false;

			_knob.position = _knobStartPo;
		}

/*	
		if( _movingPlayer )
		{
			

			if( tc > 0 && Input.GetTouch(0).phase != TouchPhase.Began )
			{
				
				MoveKnob();
				
			}
			else //if( Input.GetMouseButtonUp(0) )
			{
				
				_movingPlayer = false;
				
				_knob.position = _knobStartPo;
				
			}	
		}

		
		if( Input.GetMouseButtonDown(0) )
		{
        	//Debug.Log("Pressed left click.");
		
			int layerMask = 1 << 13;
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			RaycastHit hit;
			
	        //if( Physics.Raycast( ray, 100 ) )
			if( Physics.Raycast( ray, out hit, 100, layerMask ) )
			{
	            
				Debug.Log( "Hit something" );
				
				_playerMove.JoyStickMoveActivate();
				
				_movingPlayer = true;
				
				_knobHitPo = hit.point;
				
			}
			
		}
		*/
	}
	
	#else

	void Active()
	{
		
		if( _movingPlayer )
		{
			
			if( Input.GetMouseButton(0) )
			{
				
				MoveKnob();
				
			}
			else //if( Input.GetMouseButtonUp(0) )
			{
				
				_movingPlayer = false;
				
				_knob.position = _knobStartPo;
				
			}

		}
			
		if( Input.GetMouseButtonDown(0) )
		{
        	//Debug.Log("Pressed left click.");
		
			int layerMask = 1 << 13;
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			RaycastHit hit;
			
	        //if( Physics.Raycast( ray, 100 ) )
			if( Physics.Raycast( ray, out hit, 100, layerMask ) )
			{
	            
				Debug.Log( "Hit something" );
				
				_playerMove.JoyStickMoveActivate();
				
				_movingPlayer = true;
				
				_knobHitPo = hit.point;
				
			}
			
		}
		
	}
	
	#endif
	
	public Vector2 GetJoystickMovement()
	{
		
		return new Vector2( _xMove, _yMove );
		
	}
	
	
	private void MoveKnob()
	{
		float x = Input.GetTouch(0).position.x / Screen.width;
		
		float y = Input.GetTouch(0).position.y / Screen.height;

		Vector3 nowTouchPo = _c.ViewportToWorldPoint( new Vector3( x, y, 8.0f ) );

		Vector2 u = new Vector2( nowTouchPo.x - _knobStartPo.x, nowTouchPo.y - _knobStartPo.y );

		//DebugLog( "MoveKnob() u.x = " + u.x + "   u.y = " + u.y );

		//u = u - new Vector2( _knobStartPo.x, _knobStartPo.y );
		
		//Debug.Log( "u.x = " + u.x + "   u.y = " + u.y );

		float sqrMag = u.sqrMagnitude;

		if( sqrMag > _moveRangeSquared )
		{
			float mag = Mathf.Sqrt( sqrMag );
			u /= mag;
			u *= _moveRange;

			nowTouchPo = new Vector3( u.x + _knobStartPo.x, u.y + _knobStartPo.y, nowTouchPo.z );
		}
		
		//float poX = Mathf.Clamp( nowTouchPo.x, _knobStartPo.x -_moveRange, _knobStartPo.x + _moveRange );

		//float poY = Mathf.Clamp( nowTouchPo.y, _knobStartPo.y -_moveRange, _knobStartPo.y + _moveRange );

		//Vector2 po = new Vector2( nowTouchPo.x, nowTouchPo.y );

		//float poSqrMag = po.SqrMagnitude();
		
		//Vector3 nowTouchPo = new Vector3( u.x + _knobStartPo.x, u.y + _knobStartPo.y, 8.0f );
		



		_xMove = u.x;
		
		_yMove = u.y;
		
		//Debug.Log( "_xMove = " +_xMove + "   _yMove = " + _yMove );
		
		/*
		if( x * x + y * y > _moveRangeSquared )
		{
			
			
			
		}
		*/
		
		_knob.position = nowTouchPo;
		
	}


	private void MoveKnob1()
	{
		//#if UNITY_ANDROID

		float x = Input.GetTouch(0).position.x / Screen.width;
		
		float y = Input.GetTouch(0).position.y / Screen.height;
		/*
		#else

		float x = Input.mousePosition.x / Screen.width;
		
		float y = Input.mousePosition.y / Screen.height;

		#endif
		*/
		Vector3 nowTouchPo = _c.ViewportToWorldPoint( new Vector3( x, y, 8.0f ) );

		Vector2 u = new Vector2( nowTouchPo.x - _knobStartPo.x, nowTouchPo.y - _knobStartPo.y );

		Debug.Log( "u.x = " + u.x + "   u.y = " + u.y );

		//u = u - new Vector2( _knobStartPo.x, _knobStartPo.y );
		
		//Debug.Log( "u.x = " + u.x + "   u.y = " + u.y );

		float sqrMag = u.sqrMagnitude;

		if( sqrMag > _moveRangeSquared )
		{
			float mag = Mathf.Sqrt( sqrMag );
			u /= mag;
			u *= _moveRange;

			nowTouchPo = new Vector3( u.x + _knobStartPo.x, u.y + _knobStartPo.y, nowTouchPo.z );
		}
		
		//float poX = Mathf.Clamp( nowTouchPo.x, _knobStartPo.x -_moveRange, _knobStartPo.x + _moveRange );

		//float poY = Mathf.Clamp( nowTouchPo.y, _knobStartPo.y -_moveRange, _knobStartPo.y + _moveRange );

		//Vector2 po = new Vector2( nowTouchPo.x, nowTouchPo.y );

		//float poSqrMag = po.SqrMagnitude();
		
		//Vector3 nowTouchPo = new Vector3( u.x + _knobStartPo.x, u.y + _knobStartPo.y, 8.0f );
		



		_xMove = u.x;
		
		_yMove = u.y;
		
		//Debug.Log( "_xMove = " +_xMove + "   _yMove = " + _yMove );
		
		/*
		if( x * x + y * y > _moveRangeSquared )
		{
			
			
			
		}
		*/
		
		_knob.position = nowTouchPo;
		
	}
	
	[ SerializeField ] Text _debugText;

	void DebugLog1( string s )
	{
		_debugText.text = s;
	}

	
}
