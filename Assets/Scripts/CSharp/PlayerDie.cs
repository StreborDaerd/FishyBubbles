using UnityEngine;

using System.Collections;



public class PlayerDie : MonoBehaviour
{
	
	
	
	[ SerializeField ] private Rigidbody2D _rigidBody;
	
	[ SerializeField ] private Renderer _left;
	
	[ SerializeField ] private Renderer _right;
	
	[ SerializeField ] private Transform _transform;
	
	
	
	void Start()
	{
		
		Deactivate();
		
	}
	
	
	
	public void Activate( Vector3 po, float velocityX )
	{
		
		if( velocityX > 0.0f )
		{
			
			_left.enabled = false;
			
			_right.enabled = true;
			
		}
		
		_transform.position = po;
		
		_rigidBody.isKinematic = false;
		
	}
	
	
	
	void Deactivate()
	{
		
		_rigidBody.isKinematic = true;
		
		
	}
	
	
	
	void FixedUpdate()
	{
		
		_rigidBody.AddTorque( Random.Range( -10.0f, 10.0f ) );
		
		if( _transform.position.y > 50.0f )
		{
			
			Destroy( _transform.gameObject );
			
		}
		
	}
	
	
}
