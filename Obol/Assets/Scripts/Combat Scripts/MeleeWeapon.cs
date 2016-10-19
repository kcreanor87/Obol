using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour {

	public PlayerControls_Combat _playerScript;
	public bool _coolDown;
	public int _minDamage;
	public int _maxDamage;
	public float _cooldownTimer = 2.0f;

	void Start(){
		_playerScript = GameObject.Find("Player").GetComponent<PlayerControls_Combat>();
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && !_coolDown){
			int damage = Random.Range(_minDamage, _maxDamage);
			_playerScript.BeenHit(damage);
			_coolDown = true;
			StartCoroutine(CoolDown());
		}
	}

	public IEnumerator CoolDown(){
		yield return new WaitForSeconds(_cooldownTimer);
		_coolDown = false;
	}
}
