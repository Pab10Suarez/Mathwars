using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Graph : MonoBehaviour
{
	[SerializeField]
	Transform PointPrefab;
    //crea un slider ( por el rango) en la interfaz del script que modifica la resolucion
    //en otras palabras la cantidad de cuadrados que se dibujasn
    [SerializeField,Range(10,500)]
    int resolution = 10;
    [SerializeField]
    float dominio = 6f;
    public String funcion="seno";
    // void Awake(){
    //     Graficar();
    // }
    void onEnable(){
        
        Graficar(this.funcion);
    }
    public void Graficar(String funcion){
                //3f es el dominio de la funcion , la funcion se muestra de 
        float step = dominio/resolution;
        //vector.one= (1,1,1)
        var scale = Vector3.one*step;
        var position= Vector3.zero;
        //cantidad de cubos
        for (int i = 0; i<resolution;i++){
            
            Transform point=  Instantiate(PointPrefab);
            //vector3.right mirando a la derecha (1,0,0) dividido pro 5 f para que se junten, +0.5 para llenar y -1f para moverlo en el eje x
            //  si a step se le resta la mitad del dominio ej:-1.5f grafica a la izquierda y derecha del orgigen, sino grafica desde ese punto a la derecha
            position.x = (i + 0.5f)*step ; 

            // hace una funcion lineal f(x)=y=x 
            //nota mental= aca se puede poder un metodo que modifique la funcion dependiendo de un parametro
            if(funcion=="seno"){
                    position.y= seno(position.x)+1;
            }
            if(funcion=="coseno"){
                    position.y= coseno(position.x);
            }
            if(funcion=="cuadratica"){
                position.y= (( (cuadratica(position.x ) )/5)-1)*-1;
            }
            if(funcion=="tangente"){
                position.y= (( (tangente(position.x ) )));
            }           
            //position.y=position.x; // funcion lineal y=x

            //asigna el vector posicion a el punto 
            point.localPosition = position;
            //modifica la escala del punto *2f hace que sea mas grandes los cubos
            point.localScale = scale*3f;
            //hace que el padre sea el objeto graph (atra vez del atributo transform)
            point.SetParent(transform,false);
 
        }
    }
    //hacer objeto de cada uno con sus atributos
    public float seno(double x){
        return (float) Math.Sin(Math.PI*x);
    }
    public float cuadratica(double x){
        return (float) Math.Pow(x,2);
    }
    public float coseno(double x){
        return (float) Math.Cos(Math.PI*x);
    }
    public float tangente(double x){
        return (float) Math.Tan(Math.PI*x);
    }
}
