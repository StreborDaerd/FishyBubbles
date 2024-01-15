using UnityEngine;

 using UnityEngine.UI;

using System.Collections;



public class Bubbles : MonoBehaviour
{

#if UNITY_EDITOR || UNITY_WEBPLAYER
	[ SerializeField ] private Transform _t;
#endif
	[ SerializeField ] private Transform _target;
	
	[ SerializeField ] private PlayerMove _playerMove;
	
	[ SerializeField ] private Camera _c;
	
	[ SerializeField ] private int _amount;
	
	[ SerializeField ] private int _nowBubble;
	
	private Bubble[] _a;
	
	[ SerializeField ] private GameObject _bubbleGo;
	
	[ SerializeField ] private Vector3 _hidePo;
	
	[ SerializeField ] private float _bubbleInterval = 0.25f;
	
	private float _dtLastBubble = 0.0f;
	
	[ SerializeField ] private float _maxXSpawn;
	
	[ SerializeField ] private float _minXSpawn;
	
	[ SerializeField ] private float _maxYSpawn;
	
	[ SerializeField ] private float _minYSpawn;
	
	private delegate void OnUpdate();
	
	private OnUpdate _onUpdate;
	
	private float _score = 0f;
	
	[ SerializeField ] private Text _scoreText;
	
	private Score _scoreData;
	
	private bool[] _chosen;
	
	private int[] _chosenPrimary;
	
	
	//[ SerializeField ]
	private int _sizeMax = 10;
	
	//bubble sizes popped
	
	private int[][] _sizesPopped;
	
	
	//bad bubbles popped
	
	private int[][] _sizesBadPopped;
	
	
	private int _difficulty;
	
	
	private float[] _startingVelocities = new float[]{ 2.0f, 5.0f, 8.0f };
	
	private int[] _percentBad = new int[]{ 25, 50, 75 };
	
	
	
	private int _percentOfBad = 25;
	
	private float _startVelocity = 0.5f;
	
	
	
	private Options _options;
	
	
	
	void Start()
	{
		
		_sizesPopped = new int[6][];
		
		_sizesBadPopped = new int[3][];
		
		for( int i = 0; i < 6; i ++ )
		{
			
			_sizesPopped[i] = new int[ _sizeMax ];
			
			if( i < 3 )
			{
				
				_sizesBadPopped[i] = new int[ _sizeMax ];
				
			}
			
		}
		
		//
		
		
		GameObject loadingScreenGameObject = GameObject.Find( "LoadingScreen" );
		
		LoadingScreen loadingScreen;
		
		if( loadingScreenGameObject != null )
		{
			
			//Debug.Log( "loadingScreen found" );
			
			loadingScreen = loadingScreenGameObject.GetComponent< LoadingScreen >();
			
			_options = loadingScreen.GetOptions();
			
			_scoreData = loadingScreen.GetScore();
			
			_chosen = _options.GetChosen();
			
			SetCustomDifficulty( loadingScreen.GetOptions().PercentOfBad, loadingScreen.GetOptions().StartVelocity );
			
		}
		
		
		
		
		
		
		
		if( _chosen[0] && _chosen[1] )
		{
			
			_chosenPrimary = new int[]{ 0, 1, 2 };
			
		}
		else if( _chosen[0] && ! _chosen[1] )
		{
			
			_chosenPrimary = new int[]{ 0, 1 };
			
		}
		else if( ! _chosen[0] && _chosen[1] )
		{
			
			_chosenPrimary = new int[]{ 0, 2 };
			
		}
		else
		{
			
			_chosenPrimary = new int[]{ 0 };
			
		}
		
		
		StartBubbles();
		
		Pause();
		
	}
	
	
	
	public void StartBubbles()
	{
		
		_a = new Bubble[ _amount ];
		
		for( int i = 0; i < _amount; i ++ )
		{

			GameObject go = Instantiate( _bubbleGo, _hidePo, Quaternion.identity ) as GameObject;
#if UNITY_EDITOR || UNITY_WEBPLAYER
			go.transform.SetParent( _t );
#endif
			_a[i] = go.GetComponent< Bubble >();
			
			_a[i].StateLimits( _chosen[2], _chosen[3], _chosen[4] );
			
			_a[i].SetMaxDistanceFromTarget( _options.MaxBubbleDistance * 100.0f );
			
			
		}
		
		
	}
	
	
	
	void Update()
	{
		
		_onUpdate();
		
	}
	
	
	
	void PlayUpdate()
	{
		
		PopBubble();
		
		RandomBubbles();
		
	}
	
	
	
	public void GameOver()
	{
		
		_onUpdate = GameOverUpdate;
		
		//_scoreData.SaveScore( _score, _sizesPopped, _sizesBadPopped );
		
	}
	
	
	
	public void SaveScore()
	{
		
		_scoreData.SaveScore( _score, _sizesPopped, _sizesBadPopped );
		
	}
	
	
	
	void GameOverUpdate()
	{
		
		//PopBubble();
		
		RandomBubbles();
		
	}
	
	
	
	public void Pause()
	{
		
		_onUpdate = PauseUpdate;
		
	}
	
	
	
	public void Unpause()
	{
		
		_onUpdate = PlayUpdate;
		
	}
	
	
	
	void PauseUpdate()
	{
		
		
		
	}
	
	
	
	void RandomBubbles()
	{
		
		_dtLastBubble += Time.deltaTime;
		
		if( _dtLastBubble < _bubbleInterval )
		{
			
			return;
			
		}
		
		if( Random.Range( 0.0f, 2000.0f ) < ( 200 + Mathf.Floor( Time.time * 0.01f ) ) )
		{
			
			//int r = Random.Range( 0, 10 ) / 4 + 1;
			
			//Vector3 position = new Vector3( r * 24.0f, 20.0f, -5.0f );
			
			Vector3 po = new Vector3( Random.Range( _minXSpawn, _maxXSpawn ) + _target.position.x + _playerMove.Velocity(), Random.Range( _minYSpawn, _maxYSpawn ), 0.0f );
			
			Bubble bubble = NextBubble();
			
			bubble.Position = po;
			
			bubble.Activate( _startVelocity );
			//bubble.Activate( _startingVelocities[ _difficulty ] );
			
			
			if( Random.Range( 0, 100 ) < _percentOfBad )
			//if( Random.Range( 0, 100 ) < _percentBad[ _difficulty ] )
			{
				
				bubble.SetState( _chosenPrimary[ Random.Range( 0, _chosenPrimary.Length ) ], true );
				
			}
			else
			{
				
				bubble.SetState( _chosenPrimary[ Random.Range( 0, _chosenPrimary.Length ) ], false );
				
			}
			
			
		}
		
		
	}
	
	
	
	Bubble NextBubble()
	{
		
		int lR = 0;
		
		do
		{
			_nowBubble += 1;
			
			if( _nowBubble > _amount - 1 )
			{
				
				_nowBubble = 0;
				
				lR ++;
				
			}
			
		}
		while( lR < 2 && _a[ _nowBubble ].Active );
		
		return _a[ _nowBubble ];
		
	}
	
	
	
	void PopBubble()
	{
		
		if( Input.GetMouseButtonDown(0) )
		{
			//Debug.Log( "Clicked" );
			
			int layerMask = 1 << 9;
			
			RaycastHit2D hit = Physics2D.Raycast( _c.ScreenToWorldPoint( Input.mousePosition ), Vector2.zero, 2.0f, layerMask, -1.0f, 1.0f );
			
			if( hit.collider != null )
			{
				
				Transform tHit = hit.rigidbody.transform;
				
				Bubble bHit = tHit.GetComponent< Bubble >();
				
				float scored = Mathf.Pow( bHit.GetSize(), 2.0f ) * ( bHit.GetState() + 1 );
				
				if( bHit.IsBad() )
				{
					
					scored = scored * 2.0f;
					
					_sizesBadPopped[ bHit.GetState() ][ bHit.GetSize() - 1 ] ++;
					
				}
				else
				{
					
					_sizesPopped[ bHit.GetState() ][ bHit.GetSize() - 1 ] ++;
					
				}
				
				_score += scored;
				
				_scoreText.text = _score.ToString();
				
				bHit.Deactivate();
				
				bHit.Position = _hidePo;
				
			}
			
		}
		
	}
	
	
	
	public void Swapper()
	{
		
		for( int i = 0; i < _amount; i ++ )
		{
			
			_a[i].Swap();
			
			
		}
		
	}
	
	
	
	public void SetCustomDifficulty( float bad, float speed )
	{
		
		Debug.Log( "bad = " + bad );
		
		_percentOfBad = ( int )( bad * 100.0f );
		
		Debug.Log( "_percentOfBad = " + _percentOfBad );
		
		_startVelocity = speed * 9.0f + 1.0f;
		
	}
	
	
}
