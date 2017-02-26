using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Timer
{
    public delegate void TimerEnds(Timer timer);

    private float _timeToWait;
    public float TimeToWait
    {
        get { return _timeToWait; }
    }

    private float lastTime;

    private TimerEnds timerEndsDelegate;

    private TimerManager _manager;

    private bool done = false;

    public TimerManager Manager
    {
        get { return _manager; }
    }

    public Timer(float time, TimerEnds timerEndsDelegate, TimerManager manager = null)
    {
        this._timeToWait = time;
        this.timerEndsDelegate = timerEndsDelegate;
        this._manager = manager;
        if(Manager != null) Manager.Add(this);
        lastTime = Time.time;
    }

    public Timer(float time, Task task, TimerManager manager = null)
    {
        this._timeToWait = time;
        this.timerEndsDelegate = new TimerEnds(task.DoTask);
        this._manager = manager;
        if (Manager != null) Manager.Add(this);
        lastTime = Time.time;
    }
    public void Update()
    {
        if(done) return;
        if (Time.time - lastTime > TimeToWait)
        {
            timerEndsDelegate(this);
            if (Manager != null)
            {
                Manager.Delete(this);
            }

            done = true;
        }
    }
}

public interface Task
{
    void DoTask(Timer timer);
}

public class ChangeValueToTask<T> : Task
{
    private Wrapper<T> obj;
    private T[] changeToObject = new T[1];
    private int lastIndex = -1;

    public int Length
    {
        get { return changeToObject.Length; }
    }

    public ChangeValueToTask(Wrapper<T> refObject, T changeToObject)
    {
        this.obj = refObject;
        this.changeToObject[0] = changeToObject;
    }

    public ChangeValueToTask(Wrapper<T> refObject, T[] changeToObject)
    {
        this.obj = refObject;
        this.changeToObject = changeToObject;
    }

    public void DoTask(Timer timer)
    {
        lastIndex++;
        if (lastIndex > changeToObject.Length)
        {
            lastIndex = 0;
        }
        obj.Value = changeToObject[lastIndex];
        Debug.Log("haysdefa" + obj.Value);
    }
}

public class Wrapper<T>
{
    public Wrapper(T value)
    {
        this.Value = value;
    }
    public T Value { get; set; }
}

public class TimerManager
{
    public static TimerManager STimerManager = new TimerManager();

    private List<Timer> timers = new List<Timer>();
    private List<Timer> toDeleteTimers = new List<Timer>();

    public Timer CreateTimer(float time, Timer.TimerEnds timerEndsDelegate)
    {
        Timer t = new Timer(time, timerEndsDelegate, this);
        return t;
    }

    public Timer CreateTimer(float time, Task task)
    {
        Timer t = new Timer(time, task, this);
        return t;
    }

    public void Update()
    {
        foreach (var t in timers)
        { 
            t.Update();
        }
        foreach (var t in toDeleteTimers)
        {
            timers.Remove(t);
            Debug.Log("Remove Timer with TimeToWait = " + t.TimeToWait);
        }
        toDeleteTimers.Clear();
    }

    internal void Add(Timer t)
    {
        timers.Add(t);
    }

    internal void Delete(Timer t)
    {
        toDeleteTimers.Add(t);
    }

    public static TimerManager operator +(TimerManager manager, Timer timer)
    {
        manager.Add(timer);
        return manager;
    }
}

