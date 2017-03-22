using System.Collections;
using System.Collections.Generic;
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

    private bool _repeat = false;
    public bool Repeat { get { return _repeat;} }

    private int _timesToRepeat = 0;
    public int TimesToRepeat { get { return _timesToRepeat;} }
    private int _repeatCount = 0;
    public int RepeatedCount { get { return _repeatCount;} }

    public float TimerSeconds
    {
        get { return Time.time - lastTime; }
    }

    public TimerManager Manager
    {
        get { return _manager; }
    }

    public Timer(float time, TimerEnds timerEndsDelegate, TimerManager manager = null, bool repeat = false, int timesToRepeat = 0)
    {
        this._timeToWait = time;
        this.timerEndsDelegate = timerEndsDelegate;
        this._manager = manager;
        this._repeat = repeat;
        this._timesToRepeat = timesToRepeat;

        if (Manager != null) Manager.Add(this);
        lastTime = Time.time;
    }

    public Timer(float time, Task task, TimerManager manager = null, bool repeat = false)
    {
        this._timeToWait = time;
        this.timerEndsDelegate = new TimerEnds(task.DoTask);
        this._manager = manager;
        this._repeat = repeat;

        if (Manager != null) Manager.Add(this);
        lastTime = Time.time;
    }
    public void Update()
    {
        if(done) return;
        if (Time.time - lastTime > TimeToWait)
        {
            timerEndsDelegate(this);
            _repeatCount++;

            if (!_repeat)
            {
                Stop();
                return;
            }
            if (TimesToRepeat != 0)
            {
                if (_repeatCount > TimesToRepeat)
                {
                    Stop();
                }
            }
            lastTime = Time.time;
        }
    }

    public void Stop()
    {
        done = true;
        if(Manager != null)
            Manager.Delete(this);
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
        //Debug.Log("haysdefa" + obj.Value);
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

    private Object lockObject = new Object();

    public Timer CreateTimer(float time, Timer.TimerEnds timerEndsDelegate, bool repeat = false, int timesToRepeat = 0)
    {
        Timer t = new Timer(time, timerEndsDelegate, this, repeat, timesToRepeat);
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
        lock (this)
        {
            foreach (var t in toDeleteTimers)
            {
                timers.Remove(t);
                // Debug.Log("Remove Timer with TimeToWait = " + t.TimeToWait);
            }
            toDeleteTimers.Clear();
        }
}

    internal void Add(Timer t)
    {
        timers.Add(t);
    }

    internal void Delete(Timer t)
    {
        lock (this)
        {
            toDeleteTimers.Add(t);   
        }
    }

    public static TimerManager operator +(TimerManager manager, Timer timer)
    {
        manager.Add(timer);
        return manager;
    }
}

