using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;
public class GameManagerPrority : MonoBehaviour
{

    private enum PriorityType
    {
        MayorVelocidad,
        MenorID
    }
    private PriorityQueue<Entity> queue;
    private PriorityType priorityType;
    public TMP_Text orderText;
    public Transform startPoint;
    public float spacing = 2f;



    [Button]
    public void CrearCola()
    {
        var entities = FindObjectsByType<Entity>(FindObjectsSortMode.None);

        queue = new PriorityQueue<Entity>(Compare);

        foreach (var e in entities)
            queue.Enqueue(e);

        Debug.Log("Cola creada");
        ActualizarUI();
         ActualizarPosiciones();
    }

    bool Compare(Entity a, Entity b)
    {
        if (priorityType == PriorityType.MayorVelocidad)
            return a.Speed > b.Speed;
        else
            return a.ID < b.ID;
    }

    [Button]
    public void CambiarAPrioridadLevel()
    {
        priorityType = PriorityType.MayorVelocidad;
        ReordenarCola();
        ActualizarPosiciones();
    }

    [Button]
    public void CambiarAPrioridadID()
    {
        priorityType = PriorityType.MenorID;
        ReordenarCola();
        ActualizarPosiciones();

    }

    void ReordenarCola()
    {
        if (queue == null) return;

        queue.SetComparator(Compare);
        ActualizarUI();
        ActualizarPosiciones();
    }


    [Button] // Mostrar orden
    public void MostrarOrden()
    {
        if (queue == null) return;

        var list = queue.ToList();

        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log((i + 1) + ". " + list[i]);
        }
    }

    [Button] // Siguiente turno
    public void SiguienteTurno()
    {
        if (queue == null || queue.Count == 0) return;

        var turno = queue.Dequeue();
        Debug.Log("Turno: " + turno);
        ActualizarPosiciones();
        
    }
    void ActualizarUI() // Mostrar orden en UI
    {
        if (queue == null) return;

        var list = queue.ToList();

        string texto = "";
        texto += "Caso: " + priorityType + "\n\n";

        for (int i = 0; i < list.Count; i++)
        {
            var e = list[i];
            texto += (i + 1) + ". " + e.gameObject.name + "\n" + e.entityName                                   
               + " / ID: " + e.ID
               + " / Level: " + e.Speed
               + "\n";
        }

        orderText.text = texto;
    }

    void ActualizarPosiciones()
    {
        if (queue == null) return;

        var list = queue.ToList();

        for (int i = 0; i < list.Count; i++)
        {
            Vector3 pos = startPoint.position + new Vector3(0, -i * spacing, 0);
            list[i].transform.position = pos;
        }
    }

    [Button]
    public void MostrarOrdenUI()
    {
        ActualizarUI();
    }

}
