using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDurabilityManager : MonoBehaviour 
{
    public GameObject playerCarPrefab;
    public GameObject playerCarSpawnPlace;
    public TextMesh durabilityText;
    public int NrOfLifes;

    private GameObject _playerCar;

    private void Start()
    {
        _playerCar = (GameObject)Instantiate(playerCarPrefab, playerCarSpawnPlace.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (_playerCar.GetComponent<CarControll>().durability <= 0)
        {
            Destroy(_playerCar);
            NrOfLifes--;

            if (NrOfLifes > 0)
                StartCoroutine("SpawnCar");
        }
        else if (_playerCar.GetComponent<CarControll>().durability > _playerCar.GetComponent<CarControll>().maxDurability)
            _playerCar.GetComponent<CarControll>().durability = _playerCar.GetComponent<CarControll>().maxDurability;


        durabilityText.text = "Durability: " + _playerCar.GetComponent<CarControll>().durability + " / " + _playerCar.GetComponent<CarControll>().maxDurability;
    }

    IEnumerator SpawnCar() 
    {
        _playerCar = (GameObject)Instantiate(playerCarPrefab, playerCarSpawnPlace.transform.position, Quaternion.identity);
        _playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        _playerCar.GetComponent<BoxCollider2D>().isTrigger = true;
        _playerCar.tag = "Untouchable"; 

        yield return new WaitForSeconds(4); 
        _playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        _playerCar.GetComponent<BoxCollider2D>().isTrigger = false;
        _playerCar.tag = "Player";
    }
}
