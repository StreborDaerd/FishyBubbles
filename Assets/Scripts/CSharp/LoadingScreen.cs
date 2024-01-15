using UnityEngine;

using UnityEngine.UI;

using System.Collections;



public class LoadingScreen : MonoBehaviour
{
	
	[ SerializeField ] private Canvas _canvas;
	
	[ SerializeField ] private RectTransform _loadingImageTransform;
	
	[ SerializeField ] private GameObject _gameObject;
	
	[ SerializeField ] private float _loadingTime;
	
	[ SerializeField ] private Animator _animator;
	
	[ SerializeField ] private Score _score;
	
	[ SerializeField ] private Options _options;
	
	
	private string _levelToLoad = "MainMenu";
	
	
	[ SerializeField ] private bool _testingScene;
	
	
	
	void Start()
	{
		
		DontDestroyOnLoad( _gameObject );
		
		
		
		AnimateIn();
		
	}
	
	
	
	void AnimateIn()
	{
		
		StartCoroutine( "BubbleAnimation" );
		
		StartCoroutine( "Deactivate" );
		
		if( _testingScene )
		{
			
			return;
			
		}
		
		Application.LoadLevel( _levelToLoad );
		
	}
	
	
	
	public void Activate( string levelToLoad )
	{
		
		_levelToLoad = levelToLoad;
		
		_loadingImageTransform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );
		
		_canvas.enabled = true;
		
		_animator.Play( "LoadingIn" );
		
	}
	
	
	
	IEnumerator BubbleAnimation()
	{
		
		yield return new WaitForSeconds( _loadingTime / 5.0f );
		
		_loadingImageTransform.localScale = new Vector3( _loadingImageTransform.localScale.x * 2.0f, _loadingImageTransform.localScale.y * 2.0f, 1.0f );
		
		StartCoroutine( "BubbleAnimation" );
		
	}
	
	
	
	IEnumerator Deactivate()
	{
		
		yield return new WaitForSeconds( _loadingTime );
		
		StopCoroutine( "BubbleAnimation" );
		
		_animator.Play( "LoadingOut" );
		
		
	}
	
	
	
	void DisableCanvas()
	{
		
		_canvas.enabled = false;
		
	}
	
	
	
	public Score GetScore()
	{
		
		return _score;
		
	}
	
	
	
	public Options GetOptions()
	{
		
		return _options;
		
	}
	
	
}
