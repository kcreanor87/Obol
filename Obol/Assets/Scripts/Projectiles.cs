﻿using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour {

	public ParticleSystem _explosion;
	public ParticleSystem _trail;
	public SphereCollider _col;
	public MeshRenderer _projectile;
	public int _damage = 100;
	public float _radius = 6.5f;
	public bool _hit;
	
	void Start(){
		_explosion = transform.FindChild("Explosion").GetComponent<ParticleSystem>();
		_trail = transform.FindChild("Trail").GetComponent<ParticleSystem>();
		_projectile = gameObject.GetComponent<MeshRenderer>();
		_col = gameObject.GetComponentInParent<SphereCollider>();
	}
	void OnTriggerEnter(Collider col){
		switch(col.tag){
			case "Ground":
			if (!_hit) Explode();
			break;
			case "Resource":
			if (!_hit) Explode();
			var resScript = col.GetComponent<ResourceHubs>();
			resScript.BeenHit(_damage);
			break;
			case "Enemy":
			if (!_hit) Explode();
			var enemyScript = col.GetComponentInParent<EnemyWM>();
			enemyScript.BeenHit(_damage);
			break;
		}
	}

	void Explode(){
		_explosion.Play();
		_trail.Stop();
		_projectile.enabled = false;
		_hit = true;
		_col.radius = _radius;
	}

	void Update(){
		if (_hit){
			if (!_explosion.IsAlive()){
				Destroy(gameObject);
			}			
		}
	}
}