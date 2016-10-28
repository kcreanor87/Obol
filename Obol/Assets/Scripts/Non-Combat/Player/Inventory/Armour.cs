[System.Serializable]
public class Armour {

	public int _id;
	public string _name;
	public int _armourBonus;
	public float _weight;

	public Armour(int id, string name, int armBonus, float weight){
		_id = id;
		_name = name;
		_armourBonus = armBonus;
		_weight = weight;
	}
}
