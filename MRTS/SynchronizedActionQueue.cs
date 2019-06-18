using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRTS
{
    public class ScheduledSynchronizedAction
    {
        public Action Action { get; set; }
        public int DelayMilliseconds { get; set; }
        public bool IsPriority { get; set; }
    }

    public class SynchronizedActionQueue
    {
        private ConcurrentQueue<Action> SynchronizedActions { get; set; }
        private ConcurrentQueue<Action> PrioritySynchronizedActions { get; set; }
        private ConcurrentBag<ScheduledSynchronizedAction> ScheduledSynchronizedActions { get; set; }

        public SynchronizedActionQueue()
        {
            SynchronizedActions = new ConcurrentQueue<Action>();
            PrioritySynchronizedActions = new ConcurrentQueue<Action>();
            ScheduledSynchronizedActions = new ConcurrentBag<ScheduledSynchronizedAction>();
        }

        public void Enqueue(Action Action, bool IsPriority = false)
        {
            if (IsPriority)
                PrioritySynchronizedActions.Enqueue(Action);
            else
                SynchronizedActions.Enqueue(Action);
        }

        public void Schedule(Action Action, int DelayMilliseconds, bool IsPriority = false) => ScheduledSynchronizedActions.Add(new ScheduledSynchronizedAction { Action = Action, DelayMilliseconds = DelayMilliseconds, IsPriority = IsPriority });

        public void Update(int Diff)
        {
            Action DummyAction;
            ScheduledSynchronizedAction DummyScheduledAction;
            while (!SynchronizedActions.IsEmpty || !PrioritySynchronizedActions.IsEmpty)
            {
                while (!PrioritySynchronizedActions.IsEmpty)
                    if (PrioritySynchronizedActions.TryDequeue(out DummyAction))
                        DummyAction.Invoke();

                if (SynchronizedActions.TryDequeue(out DummyAction))
                    DummyAction.Invoke();
            }

            Queue<ScheduledSynchronizedAction> TempScheduledSynchronizedActions = new Queue<ScheduledSynchronizedAction>();
            while (!ScheduledSynchronizedActions.IsEmpty)
            {
                if (ScheduledSynchronizedActions.TryTake(out DummyScheduledAction))
                {
                    DummyScheduledAction.DelayMilliseconds -= Diff;

                    if (DummyScheduledAction.DelayMilliseconds <= 0)
                        Enqueue(DummyScheduledAction.Action, DummyScheduledAction.IsPriority);
                    else
                        TempScheduledSynchronizedActions.Enqueue(DummyScheduledAction);
                }
            }

            while (TempScheduledSynchronizedActions.Count > 0)
                ScheduledSynchronizedActions.Add(TempScheduledSynchronizedActions.Dequeue());
        }

        public void Run()
        {
            while (true)
            {
                Update(50);
                Thread.Sleep(50); // todo: implement this properly
            }
        }
    }
}