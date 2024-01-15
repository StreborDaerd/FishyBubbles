using UnityEngine;

using System.Collections;



public class Bubble1 : MonoBehaviour
{

	bool isPulled;
	
	bool isAnEffectBubble;//is the bubble an effect bubble( 0 skull, 1 lightning, 2 switcharoo, 3 health )?
	
	int thisColour;//colour of this bubble( 0 blue, 1 yellow, 2 red, 3 green, 4 purple, 5 orange )
	
	[ SerializeField ] int percentChanceOfEffect;
	
	//float thisSize;
	
	[ SerializeField ] float startSize;
	
	[ SerializeField ] float maxSize;
	
	[ SerializeField ] float maxForce;
	
	[ SerializeField ] float forceDTCoefficient;
	
	float smoothTime;
	
	float currentAccelerationX;
	
	float currentAccelerationY;
	
	[ SerializeField ] Vector2[] colourCoords;//the texture coordinates of each colour( 0 blue, 1 yellow, 2 red, 3 green, 4 purple, 5 orange )
	
	[ SerializeField ] Vector2[] effectCoords;//the texture coordinates of each effect( 0 blue, 1 yellow, 2 red, 3 green, 4 purple, 5 orange )
	
	[ SerializeField ] Vector3 hidePo;
	
	Vector3 lastMousePo;
	
	[ SerializeField ] Transform thisTransform;
	
	//Transform target;
	
	[ SerializeField ] SphereCollider thisCollider;
	
	[ SerializeField ] Rigidbody thisRigidBody;
	
	[ SerializeField ] ImagePlane thisImagePlane;
	
	
	
	
	void Start()
	{
		
		
		
	}
	
	
	
	void Update()
	{
		
		if( isPulled )
		{
			
			Pull();
			
			if( Input.GetMouseButtonDown(0) )
			{
				
				thisRigidBody.useGravity = false;
				
			}
			
		}
		else if( transform.position.y > 100 )
		{
			
			Deactivate();
			
		}
		
		
	}
	
	
	
	void Pull()
	{
		
		if( Input.GetMouseButton(0) )
		{
			
			Movement();
			
		}
		
		if( Input.GetMouseButtonUp(0) )
		{
			
			isPulled = false;
			
			thisRigidBody.useGravity = true;
			
		}
		
		
	}
	
	
	
	void Movement()
	{
		
		float dT = Time.deltaTime;
		
		Vector3 tPo = transform.position;//target.position;
		
		float foX = ( tPo.x - transform.position.x ) * dT * forceDTCoefficient;
		
		float foY = ( tPo.y - transform.position.y ) * dT * forceDTCoefficient;
		
		thisRigidBody.AddForce( new Vector3( Mathf.Clamp( foX, - maxForce, maxForce ), Mathf.Clamp( foY, - maxForce, maxForce ), 0.0f ) );
		
		
	}
	
	
	
	void Activate()
	{
		
		ChooseRandomBubble();
		
		//thisSize = 2.0f;
		
		//Resize();
		
		thisCollider.enabled = true;
		
		thisRigidBody.useGravity = true;
		
		
	}
	
	
	
	void Deactivate()
	{
		
		isPulled = false;
		
		//thisSize = 2.0f;
		
		//Resize();
		
		thisTransform.position = hidePo;
		
		thisRigidBody.useGravity = false;
		
		thisRigidBody.Sleep();
		
		thisCollider.enabled = false;
		
		
	}
	
	
	
	void ChooseRandomBubble()
	{
		
		int r = Random.Range( 0, 100 );
		
		if( r < percentChanceOfEffect )
		{
			
			isAnEffectBubble = true;
			
			thisColour = Random.Range( 0, 3 );
			
		}
		else
		{
			
			thisColour = Random.Range( 0, 3 );
			
			isAnEffectBubble = false;
			
		}
		
		//Resize();
		
	}
	
	
	
	void OnTriggerEnter( Collider c )
	{
		
		if( c.gameObject.tag == "Player" )
		{
			
			if( isAnEffectBubble )
			{
				
				switch( thisColour )
				{
					
					case 0 :
						//Skull();
						break;
					
					case 1 :
						//Lightning();
						break;
					
					case 2 :
						//SwitchARoo();
						break;
					
					/*case 3 :
						Life();
						break;*/
					
				}
				
			}
			else
			{
				
				isPulled = true;
				
			}
			
		}
		
		
	}
	
	
	
}
