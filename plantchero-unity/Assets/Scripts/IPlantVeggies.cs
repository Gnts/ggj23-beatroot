using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlantVeggies : MonoBehaviour
{
    public Transform[] PlantPots;
    public GameObject[] Plants;
    
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if(Vegetable.count > 5) continue;
            var index = Random.Range(0, Plants.Length);
            var obj = Instantiate(Plants[index]);
            obj.transform.position = PlantPots[Random.Range(0, PlantPots.Length)].position;
        }
    }
}
