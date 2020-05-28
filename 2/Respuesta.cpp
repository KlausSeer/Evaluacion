#include <iostream>

using namespace std;

/* Para este problema, planteamos un algoritmo que devolvera verdadero cuando dos rectangulos colisionen entre si.
Para ello estamos considerando una colision, cuando uno de los triangulos está dentro del otro o ambos bordes se tocan.
Como la colision en base a los bordes del rectangulo, no afecta si usamos las coordenadas al centro de la figura en su esquina*/

bool Colision(float x1, float y1, float w1, float h1,
              float x2, float y2, float w2, float h2)
{

    return (
        x1 + w1 >= x2 &&
        y1 + h1 >= y2 &&
        x2 + w2 >= x1 &&
        y2 + h2 >= y1);
}

//Lo probamos con un ejemplo

void main()
{
    int xa = 2, ya = 2, wa = 1, ha = 2;
    int xb = 3, yb = 3, wb = 2, hb = 2;
    int xc = 4, yc = 4, wc = 1, hc = 2;

    cout << Colision(xa, ya, wa, ha, xb, yb, wb, hb) << endl;
    cout << Colision(xa, ya, wa, ha, xc, yc, wc, hc) << endl;
    cout << Colision(xc, yc, wc, hc, xb, yb, wb, hb) << endl;

    cin.get();
}
