using UnityEngine;

using UnityEngine.UI;

using System.Collections;


public class Heart : MonoBehaviour
{

	[ SerializeField ] private CanvasRenderer _canvasRenderer;
	
	[ SerializeField ] private Image _image;
	
	[ SerializeField ] private RectTransform _rectTransform;
	
	[ SerializeField ] private float _interval;
	
	private Vector2 _startPo;
	
	[ SerializeField ] private Vector2 _offset;
	
	[ SerializeField ] private int _steps;
	
	private int _stepNow = 0;
	
	
	//if heart is added while animation is playing heart is deactivated and gap appears in screen ui array of hearts
	//player should flash and be immune to other bubbles for the length of the animation
	
	
	public void Start()
	{
		
		//_startPo = _rectTransform.anchoredPosition;
		
		//StartAnimation();
		
		//Debug.Log( "Heart Start" );
		
		//_image.enabled = false;
		
	}
	
	
	
	public void StartAnimation()
	{
		
		StartCoroutine( UpdateAnimation() );
		
	}
	
	
	
	IEnumerator UpdateAnimation()
	{
		
		yield return new WaitForSeconds( _interval );
		
		_canvasRenderer.SetAlpha( _canvasRenderer.GetAlpha() - 0.15f );
		
		_rectTransform.anchoredPosition = _offset + _rectTransform.anchoredPosition;
		
		_stepNow ++;
		
		if( _stepNow < _steps )
		{
			
			StartCoroutine( UpdateAnimation() );
			
		}
		else
		{
			
			Deactivate();
			
		}
		
		
	}
	
	
	
	public void StartPo( Vector2 startPo )
	{
		
		_startPo = startPo;
		
		_rectTransform.anchoredPosition = _startPo;
		
	}
	
	
	
	public void Activate()
	{
		
		_image.enabled = true;
		
	}
	
	
	
	public void Deactivate()
	{
		
		_image.enabled = false;
		
		_stepNow = 0;
		
		_canvasRenderer.SetAlpha( 1.0f );
		
		_rectTransform.anchoredPosition = _startPo;
		
	}
	
	
}
