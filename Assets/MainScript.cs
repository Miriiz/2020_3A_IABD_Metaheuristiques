using UnityEngine;

public class MainScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] cubes;
    
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Hello les 3A IABD1 !!! :)");
        Debug.Log($"Il y a {cubes.Length} cubes dans la scene !!! :)");
    }

    // Update is called once per frame
    private void Update()
    {
    }
}