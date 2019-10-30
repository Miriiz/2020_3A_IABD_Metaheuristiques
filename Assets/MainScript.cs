using System.Collections;
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
        StartCoroutine(ShuffleCubePositions(cubes));
    }

    private static IEnumerator ShuffleCubePositions(Transform[] cubes)
    {
        for (var i = 0; i < cubes.Length; i++)
        {
            var rdmIdx = Random.Range(i, cubes.Length);
            SwapPositions(cubes[i], cubes[rdmIdx]);
            yield return null;
        }
    }

    private static void SwapPositions(Transform cube1, Transform cube2)
    {
        var tmp = cube1.position;
        cube1.position = cube2.position;
        cube2.position = tmp;
    }
}