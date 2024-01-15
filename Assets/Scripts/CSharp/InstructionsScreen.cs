using UnityEngine;
//using UnityEngine.UI;

public class InstructionsScreen : MonoBehaviour
{
	//[ SerializeField ] Button _backButton;

	//[ SerializeField ] Button _chooseButton;

	private LoadingScreen _loadingScreen;
	
	void Start()
	{
		GameObject loadingScreenGameObject = GameObject.Find( "LoadingScreen" );
		
		if( loadingScreenGameObject != null )
		{
			_loadingScreen = loadingScreenGameObject.GetComponent< LoadingScreen >();
		}
	}

	/*
	void Update()
	{
		
	}
	*/

	public void Back()
	{
		_loadingScreen.Activate( "MainMenu" );
	}

	public void ChooseLevel()
	{
		_loadingScreen.Activate( "ChooseLevel" );
	}
}
