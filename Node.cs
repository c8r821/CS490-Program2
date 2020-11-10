using System;

/**
 * Node class simulating a process
 */
public class Node {
    /**
     * The process id - a unique identifier for the process
     */
    private readonly int pid;

    /**
     * The priority indicates the order in which processes should be executed
     */
    private readonly int priority;

    /**
     * The busy wait time used to simulate process execution
     */
    private readonly int timeslice;

    /**
     * Default constructor
     */
    public Node() {
        pid = -1;
        priority = -1;
        timeslice = -1;
    }

    /**
     * Parameterized constructor
     */
    public Node(int pid, int priority, int timeslice) {
        this.pid = pid;
        this.priority = priority;
        this.timeslice = timeslice;
    }

    /**
     * PID Getter
     * @return Process id
     */
    public int getPid() {
        return pid;
    }

    /**
     * Priority getter
     * @return Process priority
     */
    public int getPriority() {
        return priority;
    }

    /**
     * Timeslice getter
     * @return Process timeslice
     */
    public int getTimeslice() {
        return timeslice;
    }

    /**
     * Outputs process details
     * @return A string representing the node
     */
    public string toString() {
        return String.Format("{{ pid: {0}, priority: {1}, timeslice: {2} }}", pid, priority, timeslice);
    }
}