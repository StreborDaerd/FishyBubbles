using UnityEngine;

 using UnityEngine.UI;

using System.Collections;



public class Gameplay : MonoBehaviour
{
	
	[ SerializeField ] private Button _startButton;
	
	[ SerializeField ] private Animator _startAnimator;
	
	[ SerializeField ] private Button _pauseButton;
	
	[ SerializeField ] private Image _pauseImage;

	[ SerializeField ] private Button _closeButton;
	
	[ SerializeField ] private Image _closeImage;
	
	[ SerializeField ] private PlayerMove _playerMove;
	
	[ SerializeField ] private Bubbles _bubbles;
	
	[ SerializeField ] private BubbleEffects _bubbleEffects;
	
	[ SerializeField ] private Animator _panelAnimator;
	
	//[ SerializeField ] private AnimationClip _panelAnimationClipIn;
	
	//[ SerializeField ] private AnimationClip _panelAnimationClipOut;
	
	[ SerializeField ] private Button _restartButton;
	
	[ SerializeField ] private Button _resumeButton;
	
	[ SerializeField ] private Button _retireButton;
	
	
	
	[ SerializeField ] private CameraMove _cameraMove;
	
	[ SerializeField ] private BubblesLimits _bubblesLimits;
	
	//private bool _gameOver = false;
	
	
	[ SerializeField ] private Image _pausePanel;
	
	
	
	
	
	[ SerializeField ] private Animator _gameOverAnimator;
	
	[ SerializeField ] private Animator _gameOverButtonsAnimator;
	
	[ SerializeField ] private Animator _scoreAnimator;
	
	[ SerializeField ] private Button _retireButtonGameOver;
	
	[ SerializeField ] private Button _replayButtonGameOver;
	
	
	private LoadingScreen _loadingScreen;
	
	private bool _playerAlive = true;
	
	
	
	void Start()
	{
		
		_closeButton.enabled = false;
		
		_closeImage.enabled = false;
		
		_pauseButton.interactable = false;
		
		
		GameObject loadingScreenGameObject = GameObject.Find( "LoadingScreen" );
		
		if( loadingScreenGameObject != null )
		{
			
			//Debug.Log( "loadingScreen found" );
			
			_loadingScreen = loadingScreenGameObject.GetComponent< LoadingScreen >();
			
		}

		_playerMove.SetInputParams( _loadingScreen.GetOptions().InputSensitivity, _loadingScreen.GetOptions().PlayerDrift, _loadingScreen.GetOptions().JoyStick, _loadingScreen.GetOptions().JoyStickLeft );
		
	}
	
	
	
	void Update()
	{
		
		
		
	}
	
	

	public void BeginGame()
	{
		
		_bubblesLimits.Activate();
		
		_cameraMove.Activate();
		
		//_playerMove.SetInputParams( _loadingScreen.GetOptions().InputSensitivity, _loadingScreen.GetOptions().PlayerDrift, _loadingScreen.GetOptions().JoyStick, _loadingScreen.GetOptions().JoyStickLeft );
		
		_playerMove.Unpause();
		
		_bubbles.Unpause();
		
		_startButton.interactable = true;
		
		_startAnimator.Play( "StartButtonOut" );
		
		_pauseButton.interactable = true;
		
	}
	
	
	
	public void Pause()
	{
		
		Debug.Log( "Gameplay Pause" );
		
		_pauseButton.enabled = false;
		
		_pauseImage.enabled = false;
		
		_bubbles.Pause();
		
		_playerMove.Pause();
		
		Time.timeScale = 0.0f;
		
		//_panelAnimation.clip = _panelAnimationClipIn;
		
		//_panelAnimation.Play( _panelAnimationClipIn.name );
		
		_panelAnimator.Play( "PanelIn" );
		
		//_closeButton.enabled = true;
		
		//_closeImage.enabled = true;
		
	}
	
	
	
	public void PauseAnimation()
	{
		
		Debug.Log( "Gameplay PauseAnimation" );
		
		//_pauseButton.enabled = false;
		
		//_pauseImage.enabled = false;
		
		//_bubbles.Pause();
		
		//_playerMove.Pause();
		
		//Time.timeScale = 0.0f;
		
		_closeButton.enabled = true;
		
		_closeImage.enabled = true;
		
		_restartButton.interactable = true;
		
		_resumeButton.interactable = true;
		
		_retireButton.interactable = true;
		
		
	}
	
	
	
	public void Unpause()
	{
		
		Debug.Log( "Gameplay Unpause" );
		
		_closeButton.enabled = false;
		
		_closeImage.enabled = false;
		
		_restartButton.interactable = false;
		
		_resumeButton.interactable = false;
		
		_retireButton.interactable = false;
		
		_panelAnimator.Play( "PanelOut" );
		
	}
	
	
	
	public void UnpauseAnimation()
	{
		
		Debug.Log( "Gameplay UnpauseAnimation" );
		
		
		
		if( _bubbleEffects.IsLightningTime )
		{
			
			Time.timeScale = 2.0f;
			
		}
		else
		{
			
			Time.timeScale = 1.0f;
			
		}
		
		_bubbles.Unpause();
		
		_playerMove.Unpause();
		
		_pauseButton.enabled = true;
		
		_pauseImage.enabled = true;
		
	}
	
	
	
	public void PlayerKilled()
	{
		
		//_bubblesLimits.Deactivate();
		
		//_cameraMove.Deactivate();
		
		_playerAlive = false;
		
		_bubbleEffects.StopAllCoroutines();
		
		Time.timeScale = 1.0f;
		
		
		_bubbles.GameOver();
		
		_pauseButton.enabled = false;
		
		_pauseImage.enabled = false;
		
		_pausePanel.enabled = false;
		
		_gameOverAnimator.Play( "GameOverIn" );
		
		_scoreAnimator.Play( "SteazyGameOver" );
		
		_gameOverButtonsAnimator.Play( "GameOverButtonsIn" );
		
	}
	
	
	
	public void Retire()
	{
		
		SaveScore();
		
		_loadingScreen.Activate( "MainMenu" );
		
		_replayButtonGameOver.interactable = false;
		
		_retireButtonGameOver.interactable = false;
		
		_gameOverAnimator.Play( "GameOverOut" );
		
		_scoreAnimator.Play( "SteazyOut" );
		
		_gameOverButtonsAnimator.Play( "GameOverButtonsOut" );
		
	}
	
	
	
	public void Replay()
	{
		
		if( ! _playerAlive )
		{
			
			SaveScore();
			
		}
		
		_loadingScreen.Activate( "Level" );
		
		_replayButtonGameOver.interactable = false;
		
		_retireButtonGameOver.interactable = false;
		
		_gameOverAnimator.Play( "GameOverOut" );
		
		_scoreAnimator.Play( "SteazyOut" );
		
		_gameOverButtonsAnimator.Play( "GameOverButtonsOut" );
		
		
		
	}
	
	
	
	void SaveScore()
	{
		
		_bubbles.SaveScore();
		
	}
	
	
	
	public void PauseRetire()
	{
		
		_closeButton.enabled = false;
		
		_restartButton.interactable = false;
		
		_resumeButton.interactable = false;
		
		_retireButton.interactable = false;
		
		_bubbleEffects.StopAllCoroutines();
		
		Time.timeScale = 1.0f;
		
		_loadingScreen.Activate( "MainMenu" );
		
		
	}
	
	
	
	public void PauseRestart()
	{
		
		_closeButton.enabled = false;
		
		_restartButton.interactable = false;
		
		_resumeButton.interactable = false;
		
		_retireButton.interactable = false;
		
		_bubbleEffects.StopAllCoroutines();
		
		Time.timeScale = 1.0f;
		
		_loadingScreen.Activate( "Level" );
		
		
	}
	
	
}
