using UnityEngine;

using System.Collections;



public class BubbleEffects : MonoBehaviour
{

	[ SerializeField ] private Transform _t;
	
	[ SerializeField ] private PlayerMove _playerMove;
	
	[ SerializeField ] private PlayerHealth _playerHealth;
	
	[ SerializeField ] private Bubbles _bubbles;
	
	private bool _lightningTime = false;
	
	[ SerializeField ] private float _lightningBaseDuration;
	
	[ SerializeField ] private float _lightningSpeed;
	
	
	/*
	void Start()
	{
		
		
		
	}
	
	
	
	void Update()
	{
		
		
		
	}
	*/
	
	
	void OnTriggerEnter2D( Collider2D bubbleCollider )
	{
		
		Bubble bubble = bubbleCollider.gameObject.GetComponent< Bubble >();
		
		if( bubble.IsBad() )
		{
			
			//Debug.Log( "bubbleCollider..." );
			
			BadBubbleEffect( bubble.GetState(), bubble.GetSize() );
			
			bubble.Deactivate();
			
		}
		else
		{
			
			if( Input.GetMouseButton(0) )
			{
				
				bubble.SetTarget( _t );
				
				bubble.ActivateChase();
				
			}
			
		}
		
	}
	
	
	
	void OnTriggerStay2D( Collider2D bubbleCollider )//needed for when a bubble transforms to a bad bubble while said bubble is in the trigger area
	{
		
		Bubble bubble = bubbleCollider.gameObject.GetComponent< Bubble >();
		
		if( bubble.IsBad() )
		{
			
			BadBubbleEffect( bubble.GetState(), bubble.GetSize() );
			
			bubble.Deactivate();
			
		}
		
	}
	
	
	
	void BadBubbleEffect( int state, int size )
	{
		
		switch( state )
		{
			case 0:
				Skull( size );
				break;
			
			case 1:
				Lightning( size );
				break;
			
			case 2:
				Swapper( size );
				break;
			
		}
		
	}
	
	
	
	void Skull( int size )
	{
		
		//Debug.Log( "Skull " + size );
		
		_playerHealth.Decrease();
		
	}
	
	
	
	void Lightning( int size )
	{
		
		//Debug.Log( "Lightning " + size );
		
		if( ! _lightningTime )
		{
			
			_lightningTime = true;
			
			StartCoroutine( LightningEffect(  size * _lightningBaseDuration ) );
			
		}
		
	}
	
	
	
	IEnumerator LightningEffect( float time )
	{
		
		//Debug.Log( "LightningEffect " + time );
		
		Time.timeScale = _lightningSpeed;
		
		yield return new WaitForSeconds( time );
		
		Time.timeScale = 1.0f;
		
		_lightningTime = false;
		
	}
	
	
	
	void Swapper( int size )
	{
		
		//Debug.Log( "Swapper " + size );
		
		//_playerHealth.Increase();
		
		_bubbles.Swapper();
		
		
	}
	
	
	
	public bool IsLightningTime
	{
		
		get{ return _lightningTime; }
		
	}
	
	
	/*
	public void StopLightening()
	{
		
		StopAllCoroutines();
		
	}*/
	
}
