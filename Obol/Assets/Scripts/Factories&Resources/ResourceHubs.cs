using UnityEngine;
using System.Collections.Generic;

public class ResourceHubs : MonoBehaviour {

	public int _health = 100;
	public int _type;
	public int _resContained = 10;

	public MeshRenderer _renderer;
	public Collider _col;

	public List <GameObject> _shards = new List <GameObject>();

	void Start(){
		_renderer = gameObject.GetComponent<MeshRenderer>();
		_col = gameObject.GetComponent<Collider>();
	}

	public void BeenHit(int damage){
		_health -= damage;
		if (_health <= 0){
			BlowUp();
		}
	}

	void BlowUp(){
		_renderer.enabled = false;
		_col.enabled = false; 
		for (int i = 0; i < _resContained; i++){
			float x = transform.position.x + Random.Range(-1.5f, 1.5f);
			float y = transform.position.y + Random.Range(0.5f, 6.0f);
			float z = transform.position.z + Random.Range(-1.5f, 1.5f);
			var newPos = new Vector3(x, y, z);
			var type = (3*_type) + Random.Range(0,3);
			var shard = (GameObject) Instantiate(_shards[type], newPos, Quaternion.identity);
			var rb = shard.GetComponent<Rigidbody>();
			rb.AddExplosionForce(120.0f, transform.position, 30.0f, 3.0f);
		}
	}
}
