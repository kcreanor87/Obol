[System.Serializable]
public class Armour {

	public int _id;
	public string _name;
	public int _armourBonus;
	public float _weight;
	public int _cost;
	public int _levelReq;
	public bool _bought;

	public Armour(int id, string name, int armBonus, float weight, int cost, int levelReq, bool bought){
		_id = id;
		_name = name;
		_armourBonus = armBonus;
		_weight = weight;
		_cost = cost;
		_levelReq = levelReq;
		_bought = bought;
	}
}
