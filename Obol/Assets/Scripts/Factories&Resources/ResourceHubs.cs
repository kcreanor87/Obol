using UnityEngine;
using System.Collections.Generic;

public class ResourceHubs : MonoBehaviour {

	public int _health = 100;
	public int _type;
	public int _resContained = 10;

	public MeshRenderer _renderer;
	public Collider _col;

	public GameObject _destroyed;

	void Start(){
		_destroyed = transform.FindChild("Bone_Shard").gameObject;
		_destroyed.SetActive(false);
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
		_destroyed.SetActive(true);
	}
}
