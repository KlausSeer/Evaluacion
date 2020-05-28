#include <iostream>
#include <conio.h>
#include <math.h>

using namespace std;

//Planteamos la formula de la ecuacion de la circunferencia
float ec(int h, int x, int k, int y)
{
    return (pow((x - h), 2) + pow((y - k), 2));
}

/*Con este metodo, vamos a imprimir puntos que se encuentren dentro de la circunferencia.
Lo que haremos sera definir una circunferencia, mediante la ecuacion de la circunferencia. 
Además de un rectangulo en la parte inferior de dicha circunferencia (la mitad)
Nuestra semicircunferencia estará circunscrita con el rectangulo*/
void Draw(int h, int k, int r, int n)
{
    int rad = r * r;
    for (int i = 0; i < n; i++)
    {
        int x = 0, y = 0;
        do
        {
            //Vamos a crear un punto al azar dentro del area del rectangulo
            x = rand() % (2 * r) + (h - r / 2);
            y = rand() % r + k;
            // Descartamos cualquier punto que se encuentre fuera de la semicircunferencia (para esto usamos la ecuacion de la circunferencia)
        } while (ec(h, x, k, y) > rad);

        //Finalmente mostramos los puntos
        cout << "x: " << x << endl;
        cout << "y: " << y << endl;
    }
}

void main()
{
    Draw(200, 120, 80, 100);
    cin.get();
}