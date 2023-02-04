using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IPlantVeggies : MonoBehaviour
{
    public Transform[] PlantPots;
    public GameObject[] Plants;
    public static List<int> occupied = new List<int>();
    
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if(Vegetable.count > 5) continue;
            var index = Random.Range(0, Plants.Length);
            var potIndex = Random.Range(0, PlantPots.Length);
            if (occupied.Any(occ => occ == potIndex)) continue;
            var obj = Instantiate(Plants[index]);
            obj.GetComponent<Vegetable>().potIndex = potIndex;
            occupied.Add(potIndex);
            obj.transform.position = PlantPots[potIndex].position;
        }
    }
}
