using UnityEngine;

using UnityEngine.UI;

using System.Collections;



public class ScoreLine : MonoBehaviour
{

	[ SerializeField ] private Text _total;
	
	[ SerializeField ] private Text[] _totalColours;
	
	//[ SerializeField ] private Text _blue;
	
	//[ SerializeField ] private Text _yellow;
	
	//[ SerializeField ] private Text _red;
	
	//[ SerializeField ] private Text _green;
	
	//[ SerializeField ] private Text _purple;
	
	//[ SerializeField ] private Text _orange;
	
	//private int[] _coloursTotal;
	
	
	
	public void SetScore( int total, int[] coloursTotal )
	{
		
		_total.text = total.ToString();
		
		for( int i = 0; i < 6; i ++ )
		{
			
			_totalColours[i].text = coloursTotal[i].ToString();
			
		}
		
	}
	
	
	
	void Update()
	{
		
		
		
	}
	
}
