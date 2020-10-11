using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject draggedItem;
    [SerializeField] private GameObject spawnItem;
	[SerializeField] private GameObject gameManager;
	[SerializeField] private GameObject reloadText;
	[SerializeField] private GameObject damageText;
	[SerializeField] private GameObject rangeText;
	[SerializeField] private GameObject energyText;
	[SerializeField] private GameObject Circle;
	private gameManager manager;
	private towerScript tower;
	private Vector3 _uiPosition;
	private bool _canDrag;
	private GameObject _circle;

	void Start()
	{
		_uiPosition = draggedItem.transform.position;
		manager = gameManager.GetComponent<gameManager>();
		tower = spawnItem.GetComponent<towerScript>();
		reloadText.GetComponent<Text>().text = tower.fireRate.ToString();
		damageText.GetComponent<Text>().text = tower.damage.ToString();
		rangeText.GetComponent<Text>().text = tower.range.ToString();
		energyText.GetComponent<Text>().text = tower.energy.ToString();
		_canDrag = false;
	}

	public void OnBeginDrag(PointerEventData eventData) 
    {
		_circle = Instantiate(Circle, _uiPosition, Quaternion.identity);		           
		_circle.transform.localScale *= tower.range;
		_circle.transform.SetParent(draggedItem.transform);
	}

    public void OnDrag(PointerEventData eventData) 
    {
		if (_canDrag)
		{
	 	   draggedItem.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
		}
	}

	public void OnEndDrag(PointerEventData eventData) 
    {
		if (_canDrag)
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit && hit.collider.tag == "empty") 
	        {
			 	manager.playerEnergy -= tower.energy;

				Instantiate(spawnItem, hit.collider.gameObject.transform.position, Quaternion.identity);		           
				Destroy(hit.collider.gameObject);
			}

			draggedItem.transform.position = _uiPosition;
			Destroy(_circle);
		}
	}

	void Update()
	{
		if (manager.playerEnergy < tower.energy)
		{
			_canDrag = false;
			GetComponent<Image>().color = Color.red;
		}
		else
		{
			_canDrag = true;
			GetComponent<Image>().color = Color.white;
		}
	}
}
