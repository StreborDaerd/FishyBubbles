using UnityEngine;

using System.Collections;



public class PlayerHealth : MonoBehaviour
{

	[ SerializeField ] private int _healthMax;
	
	[ SerializeField ] private int _healthStart;
	
	private int _healthNow;
	
	[ SerializeField ] private GameObject _heart;
	
	private Heart[] _hearts1;
	
	[ SerializeField ] private Heart[] _hearts;
	
	[ SerializeField ] private bool _usePremadeHearts;
	
	[ SerializeField ] private Vector2 _startPo;
	
	[ SerializeField ] private Vector2 _offset;
	
	[ SerializeField ] private Transform _parent;
	
	[ SerializeField ] private PlayerDie _playerDie;
	
	[ SerializeField ] private PlayerMove _playerMove;
	
	
	[ SerializeField ] private Gameplay _gameplay;
	
	
	
	
	void Start()
	{
		
		_healthNow = _healthStart;
		
		/*
		if( ! _usePremadeHearts )
		{
			_hearts1 =  new Heart[ _healthMax ];
			
			for( int i = 0; i < _healthMax; i ++ )
			{
				
				GameObject go = Instantiate( _heart, Vector3.zero, Quaternion.identity ) as GameObject;
				
				go.transform.SetParent( _parent );
				
				_hearts1[i] = go.GetComponent< Heart >();
				
				_hearts1[i].StartPo( _startPo + i * _offset );
				
				if( i >= _healthStart )
				{
					
					_hearts1[i].Deactivate();
					
					
				}
				
			}
			
		}
		else
		{*/
			
			for( int i = 0; i < _healthMax; i ++ )
			{
				
				if( i >= _healthStart )
				{
					
					_hearts[i].Deactivate();
					
					
				}
				
			}
			
			
		//}
		
	}
	
	
	
	void Update()
	{
		
		
		
	}
	
	
	
	public void Increase()
	{
		
		if( _healthNow < _healthMax )
		{
			
			_hearts[ _healthNow ].Activate();
			
			_healthNow ++;
			
		}
		
	}
	
	
	
	public void Decrease()
	{
		
		if( _healthNow > 0 )
		{
			
			_healthNow --;
			
			_hearts[ _healthNow ].StartAnimation();
			
			
		}
		
		
		if( _healthNow == 0 )
		{
			
			PlayerDie();
			
		}
		
	}
	
	
	
	void PlayerDie()
	{
		
		//collider2D.enabled = false;
		
		_gameplay.PlayerKilled();
		
		_playerMove.Deactivate();
		
		_playerDie.Activate( transform.position, _playerMove.Velocity() );
		
		//GameObject go = Instantiate( _playerDie, transform.position, Quaternion.identity ) as GameObject;
		
		
		//if( _playerMove.Velocity() > 0.0f )
		//{
			
		//	_playerDie.GetComponent< PlayerDie >().Right();
			
		//}
		
		//_playerDie.transform.position = transform.position;
		
		
		//_gameplay.PlayerKilled();
		
		//_playerMove.Deactivate();
		
		//Destroy( gameObject );
		
	}
	
	
}
