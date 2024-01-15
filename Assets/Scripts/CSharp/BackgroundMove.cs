using UnityEngine;

using System.Collections;



public class BackgroundMove : MonoBehaviour
{

	[ SerializeField ] private Transform _t;
	
	[ SerializeField ] private float _offset = 160.0f;
	
	
	
	void OnTriggerEnter2D( Collider2D c )
	{
		
		_t.Translate( _offset, 0, 0 );
		
	}
		
	
}
