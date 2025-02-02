using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    private int currWave = 0;

    public WaveManager FireWaveManager;
    public WaveManager WaterWaveManager;

    public void NextWave()
    {
        if (currWave >= waves.Count) return;

        FireWaveManager.BeginWave(waves[currWave].fire_enemies);
        WaterWaveManager.BeginWave(waves[currWave].water_enemies);
        currWave++;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                hitObject.GetComponent<Soldier>().TakeDamage(10f);
            }
        }
    }
}
