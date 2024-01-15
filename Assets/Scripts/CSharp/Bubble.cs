using UnityEngine;

using System.Collections;



public class Bubble : MonoBehaviour
{

	[ SerializeField ] private Transform _t;
	
	[ SerializeField ] private Rigidbody2D _r;
	
	[ SerializeField ] private Collider2D _c;
	
	[ SerializeField ] private Transform _target;
	
	private bool _active;
	
	private int _state;
	
	private bool _badState;
	
	private bool _pulled;
	
	[ SerializeField ] private Renderer[] _states;
	
	[ SerializeField ] private Renderer[] _badStates;
	
	private int _size;
	
	[ SerializeField ] private int _sizeMax = 5;
	
	private delegate void Movement();
	
	private Movement _movement;
	
	
	
	private bool _green;
	
	private bool _purple;
	
	private bool _orange;
	
	
	
	private float _maxDistanceFromTarget = 50.0f;
	
	private float _maxDistanceSquared = 2500.0f;
	
	
	
	void Start()
	{
		
		_maxDistanceSquared = _maxDistanceFromTarget * _maxDistanceFromTarget;
		
		Deactivate();
		
	}
	
	
	
	void Update()
	{
		
		_movement();
		
	}
	
	
	
	public void StateLimits( bool green, bool purple, bool orange )
	{
		
		_green = green;
		
		_purple = purple;
		
		_orange = orange;
		
	}
	
	
	
	public void SetMaxDistanceFromTarget( float distance )
	{
		
		_maxDistanceFromTarget = distance;
		
	}
	
	
	
	public void Activate( float yV )
	{
		
		_c.enabled = true;
		
		_r.isKinematic = false;
		
		_active = true;
		
		DeactivateChase();
		
		_r.velocity = new Vector2( 0.0f, yV );
		
	}
	
	
	
	public void Activate(  )
	{
		
		_c.enabled = true;
		
		_r.isKinematic = false;
		
		_active = true;
		
		DeactivateChase();
		
		_r.velocity = new Vector2( 0.0f, 5.0f );
		
	}
	
	
	
	public bool Active
	{
		
		get{ return _active; }
		
	}
	
	
	
	public void Deactivate()
	{
		
		DeactivateChase();
		
		_active = false;
		
		_r.isKinematic = true;
		
		_r.velocity = Vector2.zero;
		
		_c.enabled = false;
		
		SetSize(1);
		
		SetState( -1, false );
		
	}
	
	
	
	public int GetState()
	{
		
		return _state;
		
	}
	
	
	
	public bool IsBad()
	{
		
		return _badState;
		
	}
	
	
	
	public bool IsPulled()
	{
		
		return _pulled;
		
	}
	
	
	
	public void Swap()
	{
		
		if( _active && _state < 3 && ! _badState )
		{
			
			SetState( _state, true );
			
		}
		
	}
	
	
	
	public void SetState( int state, bool isBad )
	{
		
		if( _state != -1 )
		{
			
			if( _badState )
			{
				
				_badStates[ _state ].enabled = false;
				
			}
			else
			{
				
				_states[ _state ].enabled = false;
				
			}
			
		}
		
		
		_state = state;
		
		_badState = isBad;
		
		
		if( _state != -1 )
		{
			
			if( _badState )
			{
				
				_badStates[ _state ].enabled = true;
				
			}
			else
			{
				
				_states[ _state ].enabled = true;
				
			}
			
		}
		
	}
	
	
	
	public int GetSize()
	{
		
		return _size;
		
	}
	
	
	
	public void SetSize( int size )
	{
		
		_size = size;
		
		float s = _size + 1.5f;
		
		_t.localScale = new Vector3( s, s, s );
		
	}
	
	
	
	public Vector3 Position
	{
		
		get{ return _t.position; }
		
		set{ _t.position = value; }
		
	}
	
	
	
	public void SetTarget( Transform target )
	{
		
		_target = target;
		
	}
	
	
	
	void DoNothing(){}
	
	
	
	void ChaseTarget()
	{
		
		if( Input.GetMouseButton(0) )
		{
			
			Vector3 tPo = _target.position;
			
			//multiplied by current velocity divided by distance?
			
			//float foX = ( tPo.x - Position.x ) * Time.deltaTime * 50;
			
			//float foY = ( tPo.y - Position.y ) * Time.deltaTime * 50;
			
			float foX = tPo.x - Position.x;
			
			float foY = tPo.y - Position.y;
			
			Vector2 d = new Vector2( -foX, -foY );
			
			
			if( d.sqrMagnitude > _maxDistanceSquared )
			{
				
				Debug.Log( "foX = " + foX + "   foY = " + foY );
				
				_r.velocity = Vector2.zero;
				
				d = Vector2.ClampMagnitude( d, _maxDistanceFromTarget );
				
				Debug.Log( "d.x = " + d.x + "   d.y = " + d.y );
				
				Position = new Vector3( tPo.x + d.x, tPo.y + d.y, 0.0f );
				
				
				//Vector3 po = new Vector3
				
				//Position = 
				
			}
			
			foX = foX * Time.deltaTime * 50;
			
			foY = foY * Time.deltaTime * 50;
			
			_r.AddForce( new Vector3( Mathf.Clamp( foX, -30, 30 ), Mathf.Clamp( foY, -30, 30 ), 0 ) );
			
			
		}
		else
		{
			
			DeactivateChase();
			
			if( Position.y > 30.0f || Position.y < -30.0f  )
			{
				
				Deactivate();
				
				return;
				
			}
			
			float d = Mathf.Abs( Position.x - _target.position.x );
			
			if( d > 60.0f )
			{
				
				Deactivate();
				
			}
			
		}
		
	}
	
	
	
	public void ActivateChase()
	{
		
		_pulled = true;
		
		_r.gravityScale = 0.0f;
		
		_movement = ChaseTarget;
		
	}
	
	
	
	public void DeactivateChase()
	{
		
		_pulled = false;
		
		_r.gravityScale = -0.1f;
		
		_movement = DoNothing;
		
	}
	

	
	void OnCollisionEnter2D( Collision2D bubbleCollision )
	{
		
		Bubble bubble = bubbleCollision.gameObject.GetComponent< Bubble >();
		
		if( bubble == null )
		{
			
			return;
			
		}
		
		int newSize = _size + bubble.GetSize();
		
		if( newSize < _sizeMax )
		{
			
			if( _state == bubble.GetState() )
			{
				
				if( _size > bubble.GetSize() )
				{
					
					if( _badState || ! bubble.IsBad() )
					{
						
						SetSize( newSize );
						
						bubble.Deactivate();
						
					}
					
				}
				else
				{
					
					if( bubble.IsBad() || ! _badState )
					{
						
						bubble.SetSize( newSize );
						
						Deactivate();
						
					}
					
				}
				
			}
			else if( bubble.GetSize() == _size && ! _badState && ! bubble.IsBad() )
			{
				
				if( _state == 0 )//blue
				{
					if( bubble.GetState() == 1  && _green )//yellow
					{
						//green
						SetState( 3, false );
						
						bubble.Deactivate();
						
					}
					else if( bubble.GetState() == 2 && _purple )//red
					{
						//purple
						SetState( 4, false );
						
						bubble.Deactivate();
						
					}
					
				}
				else if( _state == 1 )//yellow
				{
					
					if( bubble.GetState() == 0  && _green )//blue
					{
						//green
						SetState( 3, false );
						
						bubble.Deactivate();
						
					}
					else if( bubble.GetState() == 2 && _orange )//red
					{
						//orange
						SetState( 5, false );
						
						bubble.Deactivate();
						
					}
					
				}
				else if( _state == 2 )//red
				{
					
					if( bubble.GetState() == 0 && _purple )//blue
					{
						//purple
						SetState( 4, false );
						
						bubble.Deactivate();
						
					}
					else if( bubble.GetState() == 1 && _orange )//yellow
					{
						//orange
						SetState( 5, false );
						
						bubble.Deactivate();
						
					}
					
				}
				
			}
			
		}
		
	}
	
	
}
