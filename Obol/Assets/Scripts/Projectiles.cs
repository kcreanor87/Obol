﻿using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour {

	public ParticleSystem _explosion;
	public ParticleSystem _trail;
	public SphereCollider _col;
	public MeshRenderer _projectile;
	public PlayerControls_Combat _player;
	public int _minDam;
	public int _maxDam;
	public int _damage = 10;
	public float _radius = 6.5f;
	public bool _hit;
	public bool _enemyShot;
	
	void Start(){
		_damage = Random.Range(_minDam, _maxDam);
		_explosion = transform.FindChild("Explosion").GetComponent<ParticleSystem>();
		_trail = transform.FindChild("Trail").GetComponent<ParticleSystem>();
		_projectile = gameObject.GetComponent<MeshRenderer>();
		_col = gameObject.GetComponentInParent<SphereCollider>();
		if (_enemyShot) _player = GameObject.FindWithTag("Player").GetComponent<PlayerControls_Combat>();
	}
	void OnTriggerEnter(Collider col){
		if (!_enemyShot){
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
				Explode();
				var enemyScript = col.GetComponentInParent<EnemyAI>();
				if (enemyScript._health > 0){
					enemyScript.BeenHit(_damage);
				}			
				break;
			}
		}
		else{
			switch(col.tag){
				case "Ground":
				if (!_hit) Explode();
				break;
				case "Player":
				Explode();
				_player.BeenHit(_damage);		
				break;
			}
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
