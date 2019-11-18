using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] cubes;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        Random.InitState(42);

        Debug.Log("Hello les 3A IABD1 !!! :)");
        Debug.Log($"Il y a {cubes.Length} cubes dans la scene !!! :)");

        Debug.Log($"Erreur avant mélange : {GetError(cubes)}");

        yield return StartCoroutine(ShuffleCubePositions(cubes));

        Debug.Log($"Erreur après mélange : {GetError(cubes)}");

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(NaiveLocalSearch(cubes));
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

    /// <summary>
    /// C'est notre oracle !!! :)
    /// </summary>
    /// <returns></returns>
    private static int GetError(Transform[] cubes)
    {
        var error = 0;

        foreach (var cube1 in cubes)
        {
            foreach (var cube2 in cubes)
            {
                if (cube1.CompareTag(cube2.tag) && Math.Abs(cube1.position.y - cube2.position.y) > float.Epsilon)
                {
                    error++;
                }
            }
        }

        return error;
    }

    public static IEnumerator NaiveLocalSearch(Transform[] cubes)
    {
        var currentError = GetError(cubes);

        while (currentError > 0)
        {
            Transform cube1;
            Transform cube2;

            do
            {
                cube1 = cubes[Random.Range(0, cubes.Length)];
                cube2 = cubes[Random.Range(0, cubes.Length)];
            } while (!AreNeighbours(cube1, cube2));

            SwapPositions(cube1, cube2);

            var newError = GetError(cubes);

            if (newError <= currentError)
            {
                currentError = newError;
            }
            else
            {
                SwapPositions(cube1, cube2);
            }

            yield return null;
        }
    }

    public static bool AreNeighbours(Transform cube1, Transform cube2)
    {
        // L'un au dessus de l'autre
        if (Mathf.Abs(cube1.position.x - cube2.position.x) < 0.0000001f
            && Math.Abs(Mathf.Abs(cube1.position.y - cube2.position.y) - 2f) < 0.0000001f)
        {
            return true;
        }

        // L'un à côté de l'autre
        if (Mathf.Abs(cube1.position.y - cube2.position.y) < 0.0000001f
            && Math.Abs(Mathf.Abs(cube1.position.x - cube2.position.x) - 2f) < 0.0000001f)
        {
            return true;
        }

        return false;
    }
}