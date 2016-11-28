using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour {

	public ParticleSystem _explosion;
	public ParticleSystem _trail;
	public SphereCollider _col;
	public MeshRenderer _projectile;
	public PlayerControls_Combat _player;
	public int _damage = 10;
	public float _radius = 6.5f;
	public bool _hit;
	public bool _enemyShot;
	public bool _exploder;
	public float _timer = 5.0f;
	
	void Start(){
		_explosion = transform.FindChild("Explosion").GetComponent<ParticleSystem>();
		_trail = transform.FindChild("Trail").GetComponent<ParticleSystem>();
		_projectile = gameObject.GetComponent<MeshRenderer>();
		if (_enemyShot){
			_player = GameObject.FindWithTag("Player").GetComponent<PlayerControls_Combat>();
			if (!_exploder) gameObject.layer = 13;
		}
		else{
			_damage = _CombatManager._rangedDam;
		}		
	}
	void OnTriggerEnter(Collider col){
		if (!_enemyShot){
			switch(col.tag){
				case "Ground":
				if (!_hit) Explode();
				break;
				case "Enemy":
				if (!_hit) Explode();
				if (col.gameObject.name == "Warden_Parent"){
						var wardenScript = col.GetComponentInParent<WardenAI>();
						if (wardenScript._health > 0){
						wardenScript.BeenHit(_damage);
						}
				}
				else{
					var enemyScript = col.GetComponentInParent<EnemyAI>();
					if (enemyScript._health > 0){
						enemyScript.BeenHit(_damage);
					}
				}				
				break;
				case "Destructible":
				if (!_hit) Explode();
				var destScript = col.GetComponent<Destructibles>();
				destScript.BeenHit(_damage);	
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
				case "Enemy":
				if (_exploder){
					Explode();
					var enemyScript = col.GetComponentInParent<EnemyAI>();
					if (enemyScript._health > 0){
						enemyScript.BeenHit(_damage);
					}
				}
				break;
			}
		}
		
	}

	void Explode(){
		gameObject.layer = 2;
		_explosion.Play();
		_trail.Stop();
		_projectile.enabled = false;
		_hit = true;
		_col.radius = _CombatManager._equipRanged._radius;		
	}

	void Update(){
		if (_hit){
			if (!_explosion.IsAlive()){
				Destroy(gameObject);
			}			
		}
		else{
			_timer -= Time.deltaTime;
			if (_timer <= 0.0f) Explode();
		}		
	}
}
