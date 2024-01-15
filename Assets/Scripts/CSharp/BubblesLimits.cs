using UnityEngine;

using System.Collections;



public class BubblesLimits : MonoBehaviour
{

	[ SerializeField ] private Transform _t;
	
	[ SerializeField ] private Collider2D _c;
	
	[ SerializeField ] private Transform _target;
	
	//[ SerializeField ] private Bubbles _bubbles;
	
	private delegate void OnUpdate();
	
	private OnUpdate _onUpdate;
	
	
	
	
	void Start()
	{
		
		_onUpdate = DoNothing;
		
	}
	
	
	
	void Update()
	{
		
		_onUpdate();
		
	}
	
	
	
	void DoNothing()
	{
		
		
		
	}
	
	
	
	void UpdateBubblesLimits()
	{
		
		_t.position = new Vector3( _target.position.x, 0.0f, 0.0f );
		
	}
	
	
	
	void OnTriggerEnter2D( Collider2D bubbleCollider )
	{
		
		Bubble bubble = bubbleCollider.gameObject.GetComponent< Bubble >();
		
		if( ! bubble.IsPulled() )
		{
			
			bubble.Deactivate();
			
		}
		
	}
	
	
	
	public void Activate()
	{
		
		_onUpdate = UpdateBubblesLimits;
		
	}
	
	
	
	public void Deactivate()
	{
		
		_onUpdate = DoNothing;
		
	}
	
	
}
