using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    private int currentCount;

    public Heap(int MaxSize)
    {
        items = new T[MaxSize];
        currentCount = 0;
    }

    public void AddItem(T item)
    {
        if (item!=null)
        {
                    items[currentCount] = item;
            item.HeapIndex = currentCount;
            SortUp(item);
            currentCount++;
        }

    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentCount--;
        items[0] = items[currentCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;

    }

    public bool Contains(T item)
    {
        return Equals(item, items[item.HeapIndex]);
    }

    public int Count
    {
        get
        {
            return currentCount;
        }
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    private void SortDown(T item)
    {
        while (true)
        {
            int leftIndex = item.HeapIndex * 2 + 1;
            int rightIndex = item.HeapIndex * 2 + 2;
            int SwapIndex = 0;

            if (leftIndex < currentCount)
            {
                SwapIndex = leftIndex;
                if (rightIndex < currentCount)
                {
                    if (items[leftIndex].CompareTo(items[rightIndex]) < 0)
                    {
                        SwapIndex = rightIndex;
                    }
                }
            }
            else return;

            if (item.CompareTo(items[SwapIndex]) < 0)
            {
                SwapItem(item, items[SwapIndex]);
            }
            else return;
        }

    }

    private void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                SwapItem(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    private void SwapItem(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;

        int tempItemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = tempItemAIndex;
    }

}
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
