using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // crear referencia de textm pro

public class Dialog_Eins : MonoBehaviour
{  public Image healthBar;
    [SerializeField] private GameObject Fallastetexto;   
    [SerializeField] private GameObject DialogBombi; 
    [SerializeField] private GameObject dialoguePanel; // para activar y desactivar
   [SerializeField] private GameObject dialoguePanel2; // para activar y desactivar
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)] private string [] dialogueLines;
    public GameObject gameover;
    public GameObject juego;
    public GameObject victoria;
     //es para crear las lineas de texto, al parecer el serializeField crea espacios para modificar en el unity
    // el textArea es para delimitar un limite min y max de lineas
   
   private float typingTime = 0.05f; //creamos esta variabla para sarla como parametro
   
   private bool isPlayerInRange; //principalmente creamos un buleano nos indicara si está cerc del npc
   private bool didDialogueStart; // indica si el dialogo comienza si es verdadero que el jugador esta cerca
   private int lineIndex; // para saber que linea de dialogo está mostrando
   float healthAmount= 250;
   bool fallo=false;
   public float next_spawn;
   int x=0;
     void Update() //deteccion para empezar dialogo
    {
       if (isPlayerInRange && Input.GetButtonDown("Fire1")) 
       { 
         if (!didDialogueStart) // para empezar toca determinar que no ha iniciado
         {
            StartDialogue(); // inicia el dialogo
         }
         else if (dialogueText.text == dialogueLines[lineIndex]) // si mostró la linea completa pasa a la otra linea
         {
            NextDialogueLine();
         }
         else //para dejar de tipear y muestre de una las lineas completas
         {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[lineIndex];

         }
         
       }
       var puntos = GameObject.FindGameObjectsWithTag ("punto");
      //verifica derrota
      foreach(var punto in puntos){
         if(punto.transform.position.x>=5.005f &&punto.transform.position.x<=5.03f&&!fallo){
         dialoguePanel2.SetActive(true); 
         Fallastetexto.SetActive(true);
            next_spawn=Time.time+5f;
            healthAmount-=20;
            healthBar.fillAmount =healthAmount/250;
            if (healthAmount <=0){
               gameover.SetActive(true);
               juego.SetActive(false);
            }
            fallo=true;
            Debug.Log(healthAmount);
            Destroy(punto);
            
         }
         if(Time.time>next_spawn&&fallo){
         dialoguePanel2.SetActive(false); 
         Fallastetexto.SetActive(false); 
         fallo=false;         
         }
         
      }
      //verifica victoria
        
        var barcoenemigos = GameObject.FindGameObjectsWithTag ("barcoenemigo");
        foreach(var barcoenemigo in barcoenemigos){
                if(barcoenemigo.GetComponent<Barco>().healthAmount<=0){
                    
                    x++;
                    Debug.Log(x);
                    barcoenemigo.SetActive(false);
                }    
        }
        if(x>=3){
            Debug.Log("MORTIS");
            victoria.SetActive(true);
            juego.SetActive(false);
        }

    }

     private void StartDialogue()
   {
         didDialogueStart = true; //inicia conver
         dialoguePanel.SetActive(true); // muestra el texto del panel
         DialogBombi.SetActive(false); // desactiva la negacion que se usó con el signo de exclamación ya que inicó la conver.
         lineIndex = 0; // para que con cada cambio de linea de texto se muestre la primera linea de cada texto
         Time.timeScale = 0f; //para que cuando inicie el dialogo no se mueva
         StartCoroutine(ShowLine());
   }

   private void NextDialogueLine() // para tipear las lineas siguiente
    {
          lineIndex++; // aunmentamos el indice para tipear la siguiente
          if (lineIndex <dialogueLines.Length) // si el indice es menor a las lineas tendrá que seguir
          {
              StartCoroutine (ShowLine()); //inicia la corutina hasta que no haya más lineas

          }
          else // sino hay mas lineas se detiene
          {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            DialogBombi.SetActive(true);
            Time.timeScale = 1f;
          }
    }

     //con esto que haremos como toca mostrar la primera linea, la haremos tipeada. corrutina
    private IEnumerator ShowLine()
    {
      dialogueText.text = string.Empty;

      foreach (char ch in dialogueLines[lineIndex])
      {
         dialogueText.text += ch;
         yield return new WaitForSecondsRealtime(typingTime);
      }
    }


     private void OnTriggerEnter2D(Collider2D collision)  // si se toca el npc se diaparará el ontritriger
    {
      if (collision.gameObject.CompareTag("Player"))
      {
         isPlayerInRange = true; 
         DialogBombi.SetActive(true); //aparecer bombillo cuando player se hacerque
         Debug.Log("Se puede iniciar un dialogo"); // para comprobar si el jugador toca la franja de npc
      }
      
    }

     private void OnTriggerExit2D(Collider2D collision)  // si se sale del npc el ontritriger
     {
        if(collision.gameObject.CompareTag("Player"))
        {
             isPlayerInRange = false;   
             DialogBombi.SetActive(false); //desaparece cuando el jugador se aleja de box
             Debug.Log("No se puede iniciar un dialogo"); // para comprobar si el jugador sale de la franja de npc
        }
       

     }
}
