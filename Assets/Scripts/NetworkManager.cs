using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour//comentario prueba
{
public void CreateUser(string userName,string email,string pass, Action <Response> response)
 {
  StartCoroutine( CO_CreateUser( userName , email , pass , response ) );
}
private IEnumerator CO_CreateUser(string userName,string email,string pass, Action <Response> response)//inicio de COrrutina para esperar al sevidor (por q no c hace en un frame)
 {
   WWWForm form = new WWWForm();
   form.AddField("username",userName);
   form.AddField("email",email);
   form.AddField("pass",pass);
   WWW w = new WWW("http://localhost/Game/createuser.php",form);

   yield return w;

   response (JsonUtility.FromJson<Response>(w.text) );//trasforma la respuesta en linea por una tipo response
  }

}
 [Serializable]
public class Response
{
    public bool done = false;
    public string message = "usuario no registrado";
}