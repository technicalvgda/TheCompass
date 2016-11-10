//Code tested on the "AsteroidCollisionTestScene" scene


using UnityEngine;
using System.Collections;

public class BlackholeBehavior : MonoBehaviour {

	//private int _blackHoleRadius = 100;
	private Rigidbody2D _rb2d;
    //the transform of the debris border
    public RectTransform debrisBorder;
    //the width and height of the debris border
    float borderWidth;
    float borderHeight;

	public int _gravityScale = 10;

	private float _blackHoleEdge = 7f;
	public float _blackHoleBounds = 200f;

	public int _acceleration = 950;

	// Use this for initialization
	void Start () 
	{
        borderWidth =  debrisBorder.rect.width/2;
        borderHeight = debrisBorder.rect.height/2;
        
    }

	// Update is called once per frame
	void Update () 
	{
	}

	void FixedUpdate()
	{
		//Vector2 blackHolePos = transform.position;
		GameObject[] spaceObjects = FindObjectsOfType<GameObject>();

		foreach(GameObject _spaceObject in spaceObjects)
		{
			_rb2d = _spaceObject.GetComponent<Rigidbody2D>();

			if (_spaceObject && _spaceObject.transform != transform && _rb2d)
			{
				Vector2 distance = transform.position - _spaceObject.transform.position;
				float near = Vector2.Distance(_spaceObject.transform.position, transform.position);

				if ( near <= _blackHoleBounds ) //boundary set; if outside the inner boundary of black hole, object is still being pulled
				{ 
					_rb2d.AddForce((_acceleration/near) * distance.normalized * _rb2d.mass * _gravityScale * Time.fixedDeltaTime , ForceMode2D.Force );
				}

				if ( near <= _blackHoleEdge ) // Suck object into black hole 
				{					

					if (_spaceObject.tag == "Debris")
					{
                        //choose a border for each object
                        int border = Random.Range(1, 4);

                        switch(border)
                        {
                            case 1:
                                //set at pos x border and at random position between -height and positive height
                                _spaceObject.transform.position = new Vector2(debrisBorder.position.x + borderWidth, Random.Range(-borderHeight, borderHeight));
                               
                                break;
                            case 2:
                                //set at neg x border and at random position between -height and positive height
                                _spaceObject.transform.position = new Vector2(debrisBorder.position.x - borderWidth, Random.Range(-borderHeight, borderHeight));
                                break;
                            case 3:
                                //set at pos y border and at random position between -width and positive width
                                _spaceObject.transform.position = new Vector2(Random.Range(-borderWidth, borderWidth), debrisBorder.position.y + borderHeight);
                                break;
                            case 4:
                                //set at ned y border and at random position between -width and positive width
                                _spaceObject.transform.position = new Vector2(Random.Range(-borderWidth, borderWidth), debrisBorder.position.y - borderHeight);
                                break;
                            default:
                                //do nothing
                                break;

                        }    

                    }
                    else
                    {
                        Destroy(_spaceObject.gameObject);
                    }
					
				} 
			} 
		}
	}

}
