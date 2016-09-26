using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour {

	public ParticleSystem _explosion;
	public SphereCollider _col;
	public int _damage = 10;
	public float _radius = 6.5f;
	public bool _hit;
	
	void Start(){
		_explosion = transform.FindChild("Explosion").GetComponent<ParticleSystem>();
		_col = gameObject.GetComponentInParent<SphereCollider>();
	}
	void OnTriggerEnter(Collider col){
		if (col.tag == "Ground"){
			_explosion.Play();
			_hit = true;
			_col.radius = _radius;
		}
		else if (col.tag == "Enemy"){
			print("Enemy Hit");
		}
	}

	void Update(){
		if (_hit){
			if (!_explosion.IsAlive()){
				Destroy(gameObject);
			}			
		}
	}
}
