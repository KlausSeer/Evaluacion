//Para graficar nuestro ejercicio haremos uso del lenguaje processing basado en java, el cual podremos descargar aqui
// https://processing.org/download/

// Por el uso de la funcion random(), usaremos floats esta vez
float ec(float h, float x, float k, float y){
  //println(pow((x - h), 2) + pow((y - k), 2));
  return (pow((x - h), 2) + pow((y - k), 2));
}

void Go(float h, float k, float r, int n){
  float rad = r * r;
  for (int i = 0; i < n; i++)
  {
    float x = 0, y = 0;
    do
    {
      x = random((h - r) / 2, (h + r));
      y = random(k, k + r);
    } while (ec(h, x, k, y) > rad);
    point(x,y);
  }
}


void setup(){
  size(440,320);
  Go(200,120,80,600);
}
