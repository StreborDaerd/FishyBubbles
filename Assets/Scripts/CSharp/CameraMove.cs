using UnityEngine;

using System.Collections;



public class CameraMove : MonoBehaviour
{

	[ SerializeField ] private Transform _t;
	
	[ SerializeField ] private Transform _target;
	
	[ SerializeField ] private float _smoothX = 0.5f;
	
	[ SerializeField ] private float _smoothY = 0.5f;
	
	private float _accelerationX = 0.0f;
	
	private float _accelerationY = 0.0f;
	
	[ SerializeField ] private float _minY = -10.0f;
	
	[ SerializeField ] private float _maxY = 10.0f;
	
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
	
	
	
	void UpdateCamera()
	{
		
		float newPoX = Mathf.SmoothDamp( _t.position.x, _target.position.x, ref _accelerationX, _smoothX );
		
		float newPoY = Mathf.Clamp( Mathf.SmoothDamp( _t.position.y, _target.position.y, ref _accelerationY, _smoothY ), _minY, _maxY );

		_t.position = new Vector3( newPoX, newPoY, -10.0f );
		
	}
	
	
	
	void DoNothing()
	{
		
		
		
	}
	
	
	
	public void Activate()
	{
		
		_onUpdate = UpdateCamera;
		
	}
	
	
	
	public void Deactivate()
	{
		
		_onUpdate = DoNothing;
		
	}
	
	
}
