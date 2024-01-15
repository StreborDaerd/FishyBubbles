using UnityEngine;

using System.Collections;



public class TextureButton : MonoBehaviour
{
	
	
	[ SerializeField ] public Collider c;
	
	[ SerializeField ] public Renderer[] renderers;
	
	public int state = 0;
	
	public delegate void UpdateHandler();
	
	public event UpdateHandler OnUpdate;
	
	public delegate void InputHandler();
	
	public event InputHandler OnInput;
	
	[ SerializeField ] public string id;
	
	
	
	//--------//
	
	
	
	void Awake()
	{
	
		Activate();
		
		
	}
	
	
	
	public void Activate()
	{
		
		c.enabled = true;
		
		renderers[0].enabled = true;
		
		renderers[1].enabled = false;
		
		OnUpdate = null;
		
		OnInput = null;
		
		#if UNITY_IPHONE
		
		OnUpdate = TouchOverListener;
		
		#else
		
		OnUpdate = TestOverListener;
		
		#endif
		
		
	}
	
	
	
	public void Deactivate()
	{
	
		state = 0;
		
		c.enabled = false;
		
		renderers[0].enabled = false;
		
		renderers[1].enabled = false;
		
		OnUpdate = null;
		
		OnInput = null;
		
		
	}
	
	
	
	public void PauseInput()
	{
	
		//Debug.Log( "PauseInput" );
		
		OnUpdate = null;
		
		c.enabled = false;
		
	}
	
	
	
	public void ResumeInput()
	{
	
		//Debug.Log( "ResumeInput" );
		
		
		#if UNITY_IPHONE
		
		OnUpdate = TouchOverListener;
		
		#else
		
		OnUpdate = TestOverListener;
		
		#endif
		
		c.enabled = true;
		
	}
	
	
	
	public void RemoveInput()
	{
	
		OnInput = null;
		
		
	}
	
	
	
	void Update()
	{
		
		if( OnUpdate != null )
		{
			
			OnUpdate();
			
			
		}
		
		
	}
	
	
	
	void SetState( int s )
	{
	
		//Debug.Log( "SetState " + s + "   " + Time.time );
		
		renderers[ state ].enabled = false;
		
		state = Mathf.Clamp( s, 0, renderers.Length );
		
		renderers[ state ].enabled = true;
		
		
	}
	
	
	
	void TestOverListener()
	{
		
		Ray ray;
		
		RaycastHit hit;
		
		ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		
		
		if( c.Raycast( ray, out hit, 50.0f ) )
		{
			
			if( state == 0 )
			{
				
				//Debug.Log( "TestOverListener_______________" );
				
				SetState(1);
				
			}
			
			//Debug.Log( "TestOverListener " + Time.time );
			
			if( Input.GetMouseButtonUp(0) )
			{
				
				//Debug.Log( "TestOverListener Input.GetMouseButtonUp(0) " + Time.time );
				
				if( OnInput != null )
				{
					
					OnInput();
					
					
				}
				
			}
			
		}
		else
		{
				
				//SetState( 0 );
				if( state == 1 )
				{
					
					//Debug.Log( "TestOverListener_______________" );
					
					SetState(0);
					
				}
				
		}
		
		
		
	}
	
	
	
	void TouchOverListener()
	{
		
		Touch touch;
		
		
		if( Input.touchCount > 0 )
		{
			
			touch = Input.touches[0];
			
			
		}
		else
		{
			
			SetState( 0 );
			
			return;
			
			
		}
		
		
		Ray ray;
		
		RaycastHit hit;
		
		ray = Camera.main.ScreenPointToRay( touch.position );
		
		
		if( c.Raycast( ray, out hit, 50.0f ) )
		{
			
			SetState( 1 );
			
			if( touch.phase == TouchPhase.Ended )
			{
				
				if( OnInput != null )
				{
					
					OnInput();
					
					
				}
				
			}
			
		}
		else
		{
			
			SetState( 0 );
			
			
		}
		
			
	}
	
	
}
