using UnityEngine;

using UnityEngine.UI;

using System.Collections;



public class ScoreScreen : MonoBehaviour
{
	
	private LoadingScreen _loadingScreen;
	
	private Score _score;
	
	[ SerializeField ] private ScoreLine[] _scores;
	
	//[ SerializeField ] private Button _backButton;
	
	//[ SerializeField ] private Button _moreScoresButton;
	
	[ SerializeField ] private Text _moreScoresButtonText;
	
	[ SerializeField ] private Text _title;
	
	
	private int _scoreMode = 0;
	
	
	
	void Start()
	{
		
		GameObject loadingScreenGameObject = GameObject.Find( "LoadingScreen" );
		
		if( loadingScreenGameObject != null )
		{
			
			//Debug.Log( "loadingScreen found" );
			
			_loadingScreen = loadingScreenGameObject.GetComponent< LoadingScreen >();
			
			_score = _loadingScreen.GetScore();
			
			DisplayRecentScores();
			
			//DisplayTopScores();
			
		}
		
	}
	
	
	
	public void Back()
	{
		
		//DeactivateAllButtons();
		
		//_loadingScreen.GetScore().SetChosen( _yellowChosen, _redChosen, _greenChosen, _purpleChosen, _orangeChosen );
		
		_loadingScreen.Activate( "MainMenu" );
		
	}
	
	
	
	public void SwitchScoreMode()
	{
		
		if( _scoreMode == 0 )
		{
			
			_title.text = "Your High Scores!";
			
			_moreScoresButtonText.text = "Recent!";
			
			_scoreMode = 1;
			
			DisplayTopScores();
			
		}
		else if( _scoreMode == 1 )
		{
			
			_title.text = "Your Recent Scores!";
			
			_moreScoresButtonText.text = "High!";
			
			_scoreMode = 0;
			
			DisplayRecentScores();
			
		}
		
	}
	
	
	
	public void DisplayTopScores()
	{
		
		int[] topScores = _score.GetTopScores();
		
		
		//int[] recentScores = _score.GetRecentScores();
		
		int[][][] bubblesPopped = _score.GetTopSizesPopped();
		
		
		for( int i = 0; i < topScores.Length; i ++ )
		{
			
			int[] c = new int[6];
			
			for( int j = 0; j < 6; j ++ )
			{
				
				int d = 0;
				
				for( int k = 0; k < 10; k ++ )
				{
					
					d += bubblesPopped[i][j][k];
					
				}
				
				c[j] = d;
				
			}
			
			_scores[i].SetScore( topScores[i], c );
			
		}
		
	}
	
	
	
	public void DisplayRecentScores()
	{
		
		int[] recentScores = _score.GetRecentScores();
		
		int[][][] bubblesPopped = _score.GetRecentSizesPopped();
		
		//Debug.Log( "bubblesPopped" + bubblesPopped[0][0][0] + bubblesPopped[0][0][1] + bubblesPopped[0][0][2] + bubblesPopped[0][0][3] + bubblesPopped[0][0][4] + bubblesPopped[0][0][5] + bubblesPopped[0][0][6] + bubblesPopped[0][0][7] + bubblesPopped[0][0][8] + bubblesPopped[0][0][9] );
		
		for( int i = 0; i < recentScores.Length; i ++ )
		{
			
			int[] c = new int[6];
			
			for( int j = 0; j < 6; j ++ )
			{
				
				int d = 0;
				
				for( int k = 0; k < 10; k ++ )
				{
					
					d += bubblesPopped[i][j][k];
					
				}
				
				c[j] = d;
				
			}
			
			_scores[i].SetScore( recentScores[i], c );
			
		}
		
	}
	
	
	
	public void DisplayColourScores()
	{
		
		
		
	}
	
}
