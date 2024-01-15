using UnityEngine;
using System.Collections;

public class ImagePlane : MonoBehaviour
{

	public MeshFilter meshFilter;
	
	public bool centreMesh = true;
	public bool flipH = false;
	public bool flipV = false;
	
	private Material material;
	private Texture texture;
	private Vector2 textureArea;
	
	public Camera cam;
	private Vector2 cameraArea;
	
	public bool sizeInPixels;
	public Vector2 size;
	
	public bool uvAreaAsPercentage = false;
	public bool uvAreaInPixels = false;//ignored if uvAreaAsPercentage is true
	public Rect uvArea;

	[ SerializeField ] Renderer _renderer;

	// Use this for initialization
	void Awake()
	{
		material = _renderer.material;
		texture = material.mainTexture;
		textureArea = new Vector2( texture.width, texture.height );
		if( ! cam )
		{
			if( Camera.main )
			{
				cam = Camera.main;
			}
			else if( Camera.current )
			{
				cam = Camera.current;
			}
			else
			{
				Debug.Log( "There are no cameras in this scene to render this object!" );
				return;
			}
		}
		cameraArea = new Vector2( cam.pixelWidth, cam.pixelHeight );
		if( this.enabled )
		{
			MakeMesh();
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}
	
	// Update is called once per frame
	public void MakeMesh()
	{
		//GetComponent( MeshFilter );
		Mesh mesh;
		//meshFilter.mesh = mesh;
		mesh = GetComponent< MeshFilter >().mesh;
		Vector3[] vertices = new Vector3[4];
	
		if( sizeInPixels )
		{
			//how many pixels per unity unit?
			//get screen height in units
			float camHeight = cam.orthographicSize * 2.0f;
			float unitInPixels = cameraArea.y / camHeight;
			size.x = size.x / unitInPixels;
			size.y = size.y / unitInPixels;
		}
		
		if( centreMesh )
		{
			vertices[0] = new Vector3( - size.x * 0.5f, - size.y * 0.5f, 0.0f );
			vertices[1] = new Vector3( size.x * 0.5f, - size.y * 0.5f, 0.0f );
			vertices[2] = new Vector3( - size.x * 0.5f, size.y * 0.5f, 0.0f );
			vertices[3] = new Vector3( size.x * 0.5f, size.y * 0.5f, 0.0f );
			
		}
		else
		{
			vertices[0] = new Vector3( 0.0f, - size.y, 0.0f );//bottom left
			vertices[1] = new Vector3( size.x, - size.y, 0.0f );//bottom right
			vertices[2] = new Vector3( 0.0f, 0.0f, 0.0f );//top left
			vertices[3] = new Vector3( size.x, 0.0f, 0.0f );//top right
		}
	
		mesh.vertices = vertices;
	
		int[] tri = new int[6];
	
		tri[0] = 0;
		tri[1] = 2;
		tri[2] = 1;
	
		tri[3] = 2;
		tri[4] = 3;
		tri[5] = 1;
	
		mesh.triangles = tri;
	
		Vector3[] normals  = new Vector3[4];
	
		normals[0] = -Vector3.forward;
		normals[1] = -Vector3.forward;
		normals[2] = -Vector3.forward;
		normals[3] = -Vector3.forward;
	
		mesh.normals = normals;
	
		Vector2[] uv = new Vector2[4];
	
		if( uvAreaAsPercentage )
		{
			uvArea = new Rect( uvArea.x * 0.01f, uvArea.y * 0.01f, uvArea.width * 0.01f, uvArea.height * 0.01f );
		}
		else if( uvAreaInPixels )
		{
			uvArea = new Rect( uvArea.x / textureArea.x, uvArea.y / textureArea.y, uvArea.width / textureArea.x, uvArea.height / textureArea.y );
		}
		uvArea.y = 1.0f - uvArea.y;
		if( flipH && flipV )
		{
			uv[3] = new Vector2( uvArea.x, uvArea.y - uvArea.height );//top left
			uv[2] = new Vector2( uvArea.x + uvArea.width, uvArea.y - uvArea.height );//top right
			uv[1] = new Vector2( uvArea.x, uvArea.y );//bottom left
			uv[0] = new Vector2( uvArea.x + uvArea.width, uvArea.y );//bottom right
		}
		else if( flipH )
		{
			uv[1] = new Vector2( uvArea.x, uvArea.y - uvArea.height );//top left
			uv[0] = new Vector2( uvArea.x + uvArea.width, uvArea.y - uvArea.height );//top right
			uv[3] = new Vector2( uvArea.x, uvArea.y );//bottom left
			uv[2] = new Vector2( uvArea.x + uvArea.width, uvArea.y );//bottom right
		}
		else if( flipV )
		{
			uv[2] = new Vector2( uvArea.x, uvArea.y - uvArea.height );//top left
			uv[3] = new Vector2( uvArea.x + uvArea.width, uvArea.y - uvArea.height );//top right
			uv[0] = new Vector2( uvArea.x, uvArea.y );//bottom left
			uv[1] = new Vector2( uvArea.x + uvArea.width, uvArea.y );//bottom right
		}
		else
		{
			uv[0] = new Vector2( uvArea.x, uvArea.y - uvArea.height );//top left
			uv[1] = new Vector2( uvArea.x + uvArea.width, uvArea.y - uvArea.height );//top right
			uv[2] = new Vector2( uvArea.x, uvArea.y );//bottom left
			uv[3] = new Vector2( uvArea.x + uvArea.width, uvArea.y );//bottom right
		}
		
		mesh.uv = uv;
	}
}
