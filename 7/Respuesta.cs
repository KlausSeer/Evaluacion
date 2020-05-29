using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{

    //Haremos uso del producto punto para saber si el target se encuentra dentro de nuestro umbral de vision
    float angleBetween(Vector3 distance, Vector3 forward)
    {
        float cosAngle = Vector3.Dot(distance.normalized, forward.normalized);
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return angle;
    }

    Enemy FindClosestEnemy(Vector3 pos, Vector3 forward, List<Enemy> activeEnemies, float umbralAngle, float efectivity)
    {
        Enemy closest = null;

        //Creamos dos listas, una para los enemigos en nuestro rango de vision y otra para los que estan fuera
        List<Enemy> enemiesInRange = new List<Enemy>();
        List<Enemy> enemiesOutOfRange = new List<Enemy>();

        foreach (Enemy enemy in activeEnemies)
        {
            //Checamos solamente los que son visibles
            if (enemy.GetComponent<Renderer>().isVisible)
            {
                //Calculamos la distancia de posiciones
                Vector3 distance = pos - enemy.transform.position;
                //Y la usamos para saber si dicho target esta en nuestro rango de vision para decidir a que lista mandarlo
                if (angleBetween(distance, forward) < umbralAngle)
                {
                    enemiesInRange.Add(enemy);
                }
                else
                {
                    enemiesOutOfRange.Add(enemy);
                }
            }
        }

        //Si hay enemigos en rango, lo sacamos de ahi, sino de la lista de los que estan fuera de rango
        if (enemiesInRange.Count > 0)
        {
            //Ordenamos la lista en base a que tan cerca están de nosotros
            enemiesInRange.Sort(
               delegate (Enemy a, Enemy b)
               {
                   float dA = Vector3.Distance(pos, a.transform.position);
                   float dB = Vector3.Distance(pos, b.transform.position);

                   if (dA <= dB)
                   {
                       return -1;
                   }
                   else
                   {
                       return 1;
                   }
               });

            //Finalmente, si este no está fuera del rango de efectividad, lo elegimos
            if (Vector3.Distance(enemiesInRange[0].transform.position, pos) < efectivity)
                closest = enemiesInRange[0];
        }
        else if (enemiesOutOfRange.Count > 0)
        {
            //Ordenamos la lista en base a que tan cerca están de nosotros
            enemiesOutOfRange.Sort(delegate (Enemy a, Enemy b)
            {
                float dA = Mathf.Abs(umbralAngle - angleBetween((pos - a.transform.position), forward));
                float dB = Mathf.Abs(umbralAngle - angleBetween((pos - b.transform.position), forward));

                if (dA >= dB)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });

            //Finalmente, si este no está fuera del rango de efectividad, lo elegimos
            if (Vector3.Distance(enemiesOutOfRange[0].transform.position, pos) < efectivity)
                closest = enemiesOutOfRange[0];
        }

        return closest;
    }

    //Adicionalmente para mirarlo, si este no es nulo, usamos la funcion look at del Transform
    void LookAtEnemy()
    {
        Enemy e = FindClosestEnemy(this.transform.position, this.transform.forward, new List<Enemy>(GameObject.FindObjectsOfType<Enemy>()), 45f, 20f);
        if (e != null)
            transform.LookAt(e.transform);
    }
}


/* Para llegar a esta solucion, pense que seria necesario separar los enemigos en dos grupos, para poder darle prioridad al grupo que esta dentro de nuestro rango.
    Depues de esto, ordenamos el grupo visible por distancia y el que está fuera de rango por la diferencia de su angulo con nuestro angulo de umbral.
    De esta forma, los que están mas lejos del angulo estarán al final de la lista (es decir tendran menor prioridad).
    Finalmente escogemos como enemigo al primero de la lista en rango y si esta se encuentra vacia, a la de la otra lista.
*/