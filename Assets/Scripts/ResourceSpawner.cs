using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class ResourceSpawner : MonoBehaviour {

    public GameObject resourcePrefab;
    private Transform resourceContainer;
	public Sprite[] womanSprites;
    public float cooldown;

    public float nextCooldown = 0.4f;
    Text text;

	void Start () {
        resourceContainer = GameObject.Find("Resources").transform;
        text = GameObject.Find("Canvas/Cooldown").GetComponent<Text>();

	}
	
	void Update () {
        cooldown -= Time.deltaTime;
        if (cooldown < 0) cooldown = 0;

        text.text = string.Format("Cooldown: {0:0.00}", cooldown);

        if (cooldown > 0)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            var obj = (GameObject)Instantiate(resourcePrefab, pos, Quaternion.identity);
            obj.transform.parent = resourceContainer;
			obj.GetComponentInChildren<SpriteRenderer>().sprite = womanSprites[Random.Range(0,womanSprites.Length)];
            cooldown = nextCooldown;

            nextCooldown += 0.05f;
        }	
	}
}
