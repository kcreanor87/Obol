using UnityEngine;
using System.Collections;

public class Shard : MonoBehaviour {

	public int _amount;
	public int _min;
	public int _max;
	public int _type;
	public Transform _player;
	public Vector3 _chasePos;

	public float _speed = 5.0f;

	public bool _active;

	void Start(){
		_amount = Random.Range(_min, _max);
		_player = GameObject.Find("Player").GetComponent<Transform>();	
	}

	void OnTriggerEnter(Collider col){
		_active |= (col.tag == "Player");
	}

	void Update(){
		if (_active){
			float x = _player.position.x;
			float z = _player.position.z;
			_chasePos = new Vector3(x, 1.0f, z);
			 var step = _speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, _chasePos, step);
	     	var dist = Vector3.Distance(transform.position, _player.position);
			_active |= (dist > 7.0f);
			if (dist <= 2.0f){
				CollectResource();
			}
			_speed += 0.1f;
		}		
	}

	void CollectResource(){
		_manager._resources[_type] += _amount;
		WM_UI.UpdateUI();
		Destroy(gameObject);
	}
}
