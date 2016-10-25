using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AsteroidPopManager : MonoBehaviour {
	
	public int gridLength, gridHeight;
	public GameObject[] asteroids;
	public GameObject[,] _grid;
	public float timeToMoveDown;
	public float moveSpeedModifier;
	public List<GameObject> asteroidsToShoot;
	private GameObject _tempObj;
	private float _timer;
	private Vector3 _beginningPos;
	private GameObject _player;
	private GameObject _shootableAsteroidTemp;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player");
		createGrid ();
		_shootableAsteroidTemp = (GameObject)Instantiate(asteroids[Random.Range(0,asteroids.Length)], _player.transform.position,Quaternion.identity);
		_shootableAsteroidTemp.GetComponent<AsteroidPopAsteroid> ().isShootable = true;
		_shootableAsteroidTemp.GetComponent<AsteroidPopAsteroid> ().nextInLine = true;
		asteroidsToShoot.Add(_shootableAsteroidTemp);
		_shootableAsteroidTemp = (GameObject)Instantiate (asteroids [Random.Range (0, asteroids.Length)], new Vector3 (_player.transform.position.x, _player.transform.position.y - 6, 0), Quaternion.identity);
		_shootableAsteroidTemp.GetComponent<AsteroidPopAsteroid> ().isShootable = true;
		asteroidsToShoot.Add(_shootableAsteroidTemp);
	}
	
	// Update is called once per frame
	void Update () {
	
		_timer += Time.deltaTime;
		if (_timer >= timeToMoveDown) 
		{
			_timer = 0;
			StartCoroutine ("SmoothMoveDown");
			//transform.position = new Vector3 (transform.position.x, transform.position.y - 4, 0);
		}
	}

	void createGrid()
	{
		_grid = new GameObject[gridLength, gridHeight];
		//_grid = new GameObject[gridLength] [gridHeight];
		for(int i = 0; i < gridLength; i++)
		{
			for(int j = 0; j < gridHeight; j++)
			{
				_grid [i, j] = asteroids [Random.Range (0, asteroids.Length)];
				if(j%2==0)//if even
					_tempObj = (GameObject)Instantiate (_grid [i, j], new Vector3(i*4+2,j*4-1,0),Quaternion.identity);
				else
					_tempObj = (GameObject)Instantiate (_grid [i, j], new Vector3(i*4,j*4-1,0),Quaternion.identity);
				_tempObj.transform.parent = this.transform;


				/*
				if (j == 0) 
				{
					//tempObj = (GameObject)Instantiate (_grid [i, j], new Vector3(i*8+4,j*8,0),Quaternion.identity);
					if(j%2==0)//if even
						tempObj = (GameObject)Instantiate (_grid [i, j], new Vector3(i*8+4,j*8,0),Quaternion.identity);
					else
						tempObj = (GameObject)Instantiate (_grid [i, j], new Vector3(i*8,j*8,0),Quaternion.identity);
				}
				else
				{
					if(j%2==0)//if even
						tempObj = (GameObject)Instantiate (_grid [i, j], new Vector3(i*8+4,j*8-1,0),Quaternion.identity);
					else
						tempObj = (GameObject)Instantiate (_grid [i, j], new Vector3(i*8,j*8-1,0),Quaternion.identity);
				}*/
				
			}
		}
	}

	IEnumerator SmoothMoveDown()
	{
		_beginningPos = transform.position;
		while (transform.position.y > _beginningPos.y - 4) 
		{			
			transform.position = new Vector3 (transform.position.x, transform.position.y - (Time.deltaTime*moveSpeedModifier), transform.position.z);
			yield return new WaitForSeconds (0.1f);
		}
	}
	public void AsteroidFired(GameObject asteroid)
	{
		//delete from list, move next asteroid up,set next asteroid bool nextToShoot to true, spawn new random colored asteroid
		asteroidsToShoot.Remove(asteroid);
		//moving up and settings bools handled in the object
		asteroidsToShoot[0].GetComponent<AsteroidPopAsteroid> ().prepareAsteroid (_player.transform.position);
		_shootableAsteroidTemp = (GameObject)Instantiate (asteroids [Random.Range (0, asteroids.Length)], new Vector3 (_player.transform.position.x, _player.transform.position.y - 4, 0), Quaternion.identity);
		_shootableAsteroidTemp.GetComponent<AsteroidPopAsteroid> ().isShootable = true;
		asteroidsToShoot.Add(_shootableAsteroidTemp);

	}
}
