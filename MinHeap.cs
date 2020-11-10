using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/**
 * A synchronized MinHeap implementation using a dynamically sized ArrayList and Java's synchronized keyword
 */
public class MinHeap {
    /**
     * Data storage for elements in the MinHeap
     */
    public readonly List<Node> data = new List<Node>();

    /**
     * Helper function to return the priority of a node at the given index
     * @param index Index of the node
     * @return The given node's priority
     */
    private int getPriority(int index) {
        return data[index].getPriority();
    }

    /**
     * Helper function to return the parent of a given node
     * @param index Index of the child node
     * @return Index of the parent node
     */
    private int parent(int index) {
        return index / 2;
    }

    /**
     * Helper function to return the left child of a given node
     * @param index Index of the parent node
     * @return Index of the left child node
     */
    private int left(int index) {
        return 2 * index;
    }

    /**
     * Helper function to return the right child of a given node
     * @param index Index of the parent node
     * @return Index of the right child node
     */
    private int right(int index) {
        return 2 * index + 1;
    }

    /**
     * Helper function to determine if a node is a leaf
     * @param index Index of the node
     * @return Boolean indicating leaf status
     */
    private bool isLeaf(int index) {
        int size = data.Count;

        return index >= (size / 2) && index <= size;
    }

    private void swap(int a, int b) {
      Node temp = data[a];
      data[a] = data[b];
      data[b] = temp;
    }

    /**
     * Reorganizes the heap after an element is added, brings lowest value to the root
     * @param index Index to perform reheap operation from
     */
    private void reheap(int index) {
        if (!isLeaf(index)) {
            if (getPriority(index) > getPriority(left(index)) || getPriority(index) > getPriority(right(index))) {
                if (getPriority(left(index)) < getPriority(right(index))) {
                    swap(index, left(index));
                    reheap(left(index));
                } else {
                    swap(index, right(index));
                    reheap(right(index));
                }
            }
        }
    }

    /**
     * Synchronized method for reading root node
     * @return Root node
     */
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Node read() {
        return data[0];
    }

    /**
     * Synchronized method for adding a node to the minheap
     * @param node Node to be added
     */
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void push(Node node) {
        data.Add(node);

        int position = data.Count - 1;

        while (getPriority(position) < getPriority(parent(position))) {
            swap(position, parent(position));
            position = parent(position);
        }
    }

    /**
     * Synchronized method for reading and removing root node
     * @return Root node
     */
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Node pop() {
        Node node = data[0];
        Node last = data[data.Count - 1];
        data.RemoveAt(data.Count - 1);
        if (data.Count > 0) {
            data[0] = last;
        }

        reheap(0);
        return node;
    }

    /**
     * Synchronized method to determine if minheap is empty
     * @return Whether minheap is empty
     */
    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool isEmpty() {
        return data.Count == 0;
    }
}