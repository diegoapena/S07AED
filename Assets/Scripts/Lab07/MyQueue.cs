using UnityEngine;

public class MyQueue<T>
{
    #region Propeties
    private QueueNode<T> head;
    private QueueNode<T> tail;
    private int count;
    #endregion

    #region Methods
    public void Enqueue(T value)
    {
        QueueNode<T> newNode = new(value);
        count++;

        if (head == null && tail == null)
        {
            head = newNode;
            tail = newNode;
            return;
        }

        tail.SetNext(newNode);
        tail = newNode;
    }



    public T Dequeue()
    {
        if (head == null)
        {
            Clear();
            throw new System.Exception("Queue Empty");
        }



        T value = head.Value;
        head = head.Next;

        count--;
        return value;


    }



    public T Peek()
    {
        if (head == null)
            throw new System.Exception("Empy");

        return head.Value;
    }
    #endregion

    #region clear
    public void Clear()
    {
        head = null;
        tail = null;
        count = 0;
    }

    public int Count => count;
    #endregion
}