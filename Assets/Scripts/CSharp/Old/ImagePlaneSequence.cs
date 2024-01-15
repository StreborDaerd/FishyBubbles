using UnityEngine;

using System.Collections;



public class ImagePlaneSequence : MonoBehaviour
{
	
	
	[ SerializeField ] Renderer[] images;
	
	int imagesLength = 0;
	
	int currentImage = 0;
	
	[ SerializeField ] float changeInterval = 0.05f;
	
	float lastChange = 0.0f;
	
	[ SerializeField ] bool playing = false;
	
	[ SerializeField ] bool playAutomatically = true;
	
	[ SerializeField ] bool loop = true;
	
	[ SerializeField ] bool rewind = true;
	
	[ SerializeField ] bool showAfterStop = true;
	
	[ SerializeField ] bool showAtStart = true;
	
	
	
	void Start()
	{
		
		imagesLength = images.Length;
		
		//HideAll();
		
		//this is only for testing
		//usually must use activate to start
		//and must automatically Deactivate
		
		//Deactivate();
		
		if( playAutomatically )
		{
			
			Activate();
			
		}
		else if( showAtStart )
		{
			
			images[ currentImage ].enabled = true;
			
		}
		
	}
	
	
	
	public void Activate()
	{
		
		playing = true;
		
	}
	
	
	
	public void Deactivate()
	{
		
		playing = false;
		
		lastChange = 0.0f;
		
		images[ currentImage ].enabled = false;
		
	}
	
	
	
	/*public void Deactivate()
	{
		
		playing = false;
		
		lastChange = 0.0f;
		
		for( int i = 0; i < imagesLength; i ++ )
		{
			
			images[i].enabled = false;
			
		}
		
	}*/
	
	
	
	void Update()
	{
		
		if( playing )
		{
			
			Playing();
			
		}
		
	}
	
	
	
	public void Play()
	{
		
		playing = true;
		
		lastChange = 0.0f;
		
	}
	
	
	
	void Playing()
	{
		
		if( lastChange < changeInterval )
		{
			
			lastChange += Time.deltaTime;
			
		}
		else
		{
			
			lastChange = 0.0f;
			
			NextImage();
			
		}
		
	}
	
	
	
	public void HideAll()
	{
		
		for( int i = 0; i < imagesLength; i ++ )
		{
			
			images[ currentImage ].enabled = false;
			
		}
		
	}
	
	
	
	void NextImage()
	{
		
		//Debug.Log( "ImagePlaneSequence NextImage" );
		
		if( currentImage < imagesLength - 1 )
		{
			
			images[ currentImage ].enabled = false;
			
			currentImage ++;
			
			images[ currentImage ].enabled = true;
			
		}
		else if( loop )
		{
			
			images[ currentImage ].enabled = false;
			
			currentImage = 0;
			
			images[ currentImage ].enabled = true;
			
		}
		else if( showAfterStop )
		{
			
			playing = false;
			
			if( rewind )
			{
				
				images[ currentImage ].enabled = false;
				
				currentImage = 0;
			
				images[ currentImage ].enabled = true;
				
			}
			
		}
		else
		{
			
			playing = false;
			
			images[ currentImage ].enabled = false;
			
			currentImage = 0;
			
		}
		
		
	}
	
	
}
