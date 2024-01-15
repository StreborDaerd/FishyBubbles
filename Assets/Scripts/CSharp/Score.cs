using UnityEngine;

using System.Collections;



public class Score : MonoBehaviour
{

	private int[] _recentScores;
	
	private int[] _topScores;
	
	//unlocked bubble colours
	
	private bool _yellow;
	
	private bool _red;
	
	private bool _green;
	
	private bool _purple;
	
	private bool _orange;
	
	
	
	
	//bubble max sizes
	
	private int _blueSize;
	
	private int _yellowSize;
	
	private int _redSize;
	
	private int _greenSize;
	
	private int _purpleSize;
	
	private int _orangeSize;
	
	
	//bubble sizes popped
	
	private int[][][] _sizesPopped;
	
	private int[][][] _topSizesPopped;
	
	//private int[] _bluePopped;
	
	//private int[] _yellowPopped;
	
	//private int[] _redPopped;
	
	//private int[] _greenPopped;
	
	//private int[] _purplePopped;
	
	//private int[] _orangePopped;
	
	
	//bad bubbles popped
	
	private int[][][] _sizesBadPopped;
	
	private int[][][] _topSizesBadPopped;
	
	//private int[] _blueBadPopped;
	
	//private int[] _yellowBadPopped;
	
	//private int[] _redBadPopped;
	
	//private int[] _greenBadPopped;
	
	//private int[] _purpleBadPopped;
	
	//private int[] _orangeBadPopped;
	
	private int _scoresRecorded = 20;
	
	
	
	void Start()
	{
		
		//PlayerPrefs.DeleteAll();
		
		LoadScore();
		
	}
	
	
	
	public void SaveScore( float score, int[][] sizesPopped, int[][] sizesBadPopped )
	{
		
		StartCoroutine( SaveTopScores( (int)score, sizesPopped, sizesBadPopped ) );
		
		StartCoroutine( SaveRecentScores( (int)score, sizesPopped, sizesBadPopped ) );
		
	}
	
	
	
	public void LoadScore1()
	{
		
		_topScores = new int[ _scoresRecorded ];
		
		_recentScores = new int[ _scoresRecorded ];
		
		_sizesPopped = new int[ _scoresRecorded ][][];
		
		_sizesBadPopped = new int[ _scoresRecorded ][][];
		
		_topSizesPopped = new int[ _scoresRecorded ][][];
		
		_topSizesBadPopped = new int[ _scoresRecorded ][][];
		
		
		//if( ! PlayerPrefs.HasKey( "topScore_0" ) )
		//{
			
		//	return;
			
		//}
		
		/*
		for( int i = 0; i < 20; i ++ )
		{
			
			_topScores[i] = PlayerPrefs.GetInt( "topScore_" + i );
			
			//_recentScores[i] = PlayerPrefs.GetInt( "recentScore_" + i );
			
			Debug.Log( "LoadScore _topScores[" + i + "] = " + _topScores[i] + "  _recentScores[" + i + "] = " + _recentScores[i] );
			
		}
		*/
		//LoadRecentScores();
		
	}
	
	
	
	void LoadScore()
	{
		
		_topScores = new int[ _scoresRecorded ];
		
		_recentScores = new int[ _scoresRecorded ];
		
		_sizesPopped = new int[ _scoresRecorded ][][];
		
		_sizesBadPopped = new int[ _scoresRecorded ][][];
		
		_topSizesPopped = new int[ _scoresRecorded ][][];
		
		_topSizesBadPopped = new int[ _scoresRecorded ][][];
		
		
		for( int i = 0; i <  _scoresRecorded ; i ++ )
		{
			
			_recentScores[i] = PlayerPrefs.GetInt( "recentScore_" + i );
			
			_topScores[i] = PlayerPrefs.GetInt( "topScore_" + i );
			
			_sizesPopped[i] = new int[6][];
			
			_topSizesPopped[i] = new int[6][];
			
			
			
			for( int j = 0; j < 6; j ++ )
			{
				
				_sizesPopped[i][j] = new int[10];
				
				_topSizesPopped[i][j] = new int[10];
				
				for( int j1 = 0; j1 < 10; j1 ++ )
				{
					
					_sizesPopped[i][j][j1] = PlayerPrefs.GetInt( "recentScoreSP_" + i + j + j1, 0 );
					
					_topSizesPopped[i][j][j1] = PlayerPrefs.GetInt( "topScoreSP_" + i + j + j1, 0 );
					
				}
				
			}
			
			_sizesBadPopped[i] = new int[3][];
			
			_topSizesBadPopped[i] = new int[3][];
			
			for( int k = 0; k < 3; k ++ )
			{
				
				_sizesBadPopped[i][k] = new int[10];
				
				_topSizesBadPopped[i][k] = new int[10];
				
				for( int k1 = 0; k1 < 10; k1 ++ )
				{
					
					_sizesBadPopped[i][k][k1] = PlayerPrefs.GetInt( "recentScoreSBP_" + i + k + k1, 0 );
					
					_topSizesBadPopped[i][k][k1] = PlayerPrefs.GetInt( "topScoreSBP_" + i + k + k1, 0 );
					
					
				}
				
			}
			
		}
		
		//Debug.Log( "_sizesPopped" + _sizesPopped[0][0][0] + _sizesPopped[0][0][1] + _sizesPopped[0][0][2] + _sizesPopped[0][0][3] + _sizesPopped[0][0][4] + _sizesPopped[0][0][5] + _sizesPopped[0][0][6] + _sizesPopped[0][0][7] + _sizesPopped[0][0][8] + _sizesPopped[0][0][9] );
		
		
		
	}
	
	
	
	IEnumerator SaveRecentScores( int score, int[][] sizesPopped, int[][] sizesBadPopped )
	{
		
		//Debug.Log( "SaveRecentScores score = " + score + "   " + sizesPopped[0][0] + sizesPopped[0][1] + sizesPopped[0][2] + sizesPopped[0][3] + sizesPopped[0][4] + sizesPopped[0][5] + sizesPopped[0][6] + sizesPopped[0][7] + sizesPopped[0][8] + sizesPopped[0][9] );
		
		//string recentScoresString = "Recent Scores = ";
		
		//string[] colourNames = new string[]{ "blue", "yellow", "red", "green", "purple", "orange" };
		
		int s = score;
		
		int[][] sP = sizesPopped;
		
		int[][] sBP = sizesBadPopped;
		
		yield return new WaitForSeconds( 1.0f );
		
		for( int i = 0; i <  _scoresRecorded ; i ++ )
		{
			
			//recentScoresString += i + ") " + _recentScores[i] + ". ";
			
			
			
			int t = _recentScores[i];
			
			int[][] tP = _sizesPopped[i];
			
			int[][] tBP = _sizesBadPopped[i];
			
			
			_recentScores[i] = s;
			
			_sizesPopped[i] = sP;
			
			_sizesBadPopped[i] = sBP;
			
			
			PlayerPrefs.SetInt( "recentScore_" + i, _recentScores[i] );
			
			
			for( int j = 0; j < 6; j ++ )
			{
				
				
				
				//string recentSizesPoppedString = i + ") Recent "+ colourNames[j] +" Sizes Popped = ";
				
				for( int j1 = 0; j1 < 10; j1 ++ )
				{
					
					//recentSizesPoppedString += j1 +") " + _sizesPopped[i][j][j1] + " = ";
					
					PlayerPrefs.SetInt( "recentScoreSP_" + i + j + j1, _sizesPopped[i][j][j1] );
					
					//recentSizesPoppedString += PlayerPrefs.GetInt( "recentScoreSP_" + i + j + j1, -1 ) + ". ";
					
					if( j < 3 )
					{
						
						PlayerPrefs.SetInt( "recentScoreSBP_" + i + j + j1, _sizesBadPopped[i][j][j1] );
						
					}
					
				}
				
				//Debug.Log( recentSizesPoppedString );
				
			}
			
			
			/*
			for( int j = 0; j < 6; j ++ )
			{
				
				for( int j1 = 0; j1 < 10; j1 ++ )
				{
					
					PlayerPrefs.SetInt( "recentScoreSP_" + i + j + j1, _sizesPopped[i,j,j1] );
					
					if( j < 3 )
					{
						
						PlayerPrefs.SetInt( "recentScoreSBP_" + i + j + j1, _sizesBadPopped[i,j,j1] );
						
					}
					
				}
				
			}
			*/
			
			s = t;
			
			sP = tP;
			
			sBP = tBP;
			
		}
		
		//Debug.Log( recentScoresString );
		
	}
	
	
	
	IEnumerator SaveTopScores( int score, int[][] sizesPopped, int[][] sizesBadPopped )
	{
		
		//Debug.Log( "SaveTopScores score = " + score );
		
		//string topScoresString = "Top Scores = ";
		
		int i;
		
		int s = 0;
		
		int[][] sP = sizesPopped;
		
		int[][] sBP = sizesBadPopped;
		
		
		for( i = 0; i <  _scoresRecorded ; i ++ )
		{
			
			//Debug.Log( "SaveTopScores _topScores[" + i + "] = " + _topScores[i] );
			
			//topScoresString += i + ") " + _topScores[i] + ". ";
			
			if( _topScores[i] < score )
			{
				
				//Debug.Log( "SaveTopScores _topScores[" + i + "] : " + _topScores[i] + " < score : " + score );
				
				s = _topScores[i];
				
				sP = _topSizesPopped[i];
				
				sBP = _topSizesBadPopped[i];
				
				
				_topScores[i] = score;
				
				_topSizesPopped[i] = sizesPopped;
				
				_topSizesBadPopped[i] = sizesBadPopped;
				
				
				PlayerPrefs.SetInt( "topScore_" + i, _topScores[i] );
				
				//Debug.Log( "topScore_" + i + " = " + _topScores[i] );
				
				for( int j = 0; j < 6; j ++ )
				{
					
					for( int j1 = 0; j1 < 10; j1 ++ )
					{
						
						PlayerPrefs.SetInt( "topScoreSP_" + i + j + j1, _topSizesPopped[i][j][j1] );
						
						//Debug.Log( "		topScoreSP_" + i + j + j1 + " = " + _topSizesPopped[i][j][j1] );
						
						if( j < 3 )
						{
							
							PlayerPrefs.SetInt( "topScoreSBP_" + i + j + j1, _topSizesBadPopped[i][j][j1] );
							
							//Debug.Log( "			topScoreSBP_" + i + j + j1 + " = " + _topSizesBadPopped[i][j][j1] );
							
						}
						
					}
					
				}
				
				//i ++;
				
				break;
				
			}
			
			//topScoresString += i + ") " + _topScores[i] + ". ";
			
		}
		
		yield return new WaitForSeconds( 1.5f );
		
		for( i = i + 1; i < _scoresRecorded; i ++ )
		{
			
			//Debug.Log( "SaveTopScores i = " + i + "   _topScores[i] = " + _topScores[i] + "   s = " + s );
			
			//topScoresString += i + ") " + _topScores[i] + ". ";
			
			int t = _topScores[i];
			
			int[][] tP = _topSizesPopped[i];
			
			int[][] tBP = _topSizesBadPopped[i];
			
			
			_topScores[i] = s;
			
			_topSizesPopped[i] = sP;
			
			_topSizesBadPopped[i] = sBP;
			
			
			PlayerPrefs.SetInt( "topScore_" + i, _topScores[i] );
			
			for( int k = 0; k < 6; k ++ )
			{
				
				for( int k1 = 0; k1 < 10; k1 ++ )
				{
					
					PlayerPrefs.SetInt( "topScoreSP_" + i + k + k1, _topSizesPopped[i][k][k1] );
					
					if( k < 3 )
					{
						
						PlayerPrefs.SetInt( "topScoreSBP_" + i + k + k1, _topSizesBadPopped[i][k][k1] );
						
					}
					
				}
				
			}
			
			s = t;
			
			sP = tP;
			
			sBP = tBP;
			
		}
		
		//Debug.Log( topScoresString );
		
	}
	
	
	
	public int[] GetRecentScores()
	{
		
		return _recentScores;
		
	}
	
	
	
	public int[][][] GetRecentSizesPopped()
	{
		
		return _sizesPopped;
		
	}
	
	
	
	public int[][][] GetRecentSizesBadPopped()
	{
		
		return _sizesBadPopped;
		
	}
	
	
	
	public int[] GetTopScores()
	{
		
		return _topScores;
		
	}
	
	
	
	public int[][][] GetTopSizesPopped()
	{
		
		return _topSizesPopped;
		
	}
	
	
	
	public int[][][] GetTopSizesBadPopped()
	{
		
		return _topSizesBadPopped;
		
	}
	
	
	
	public int MaxScoresRecorded()
	{
		
		return  _scoresRecorded;
		
	}
	
	
	//public float _inputSensitivity = 0.75f;
	
	//public float _percentOfBad = 0.25f;
	
	//public float _startVelocity = 0.55f;
	
	//public bool _joyStick = false;
	
	
}
