using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float scale;

    [field: SerializeField]
    public bool AutoUpdate {
        get;
        private set;
    }

    public void GenerateMap() {
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, scale);
        MapDisplay mapDisplay = MapDisplay.Instance;
        mapDisplay.DrawNoiseMap(noiseMap);
    }
}
