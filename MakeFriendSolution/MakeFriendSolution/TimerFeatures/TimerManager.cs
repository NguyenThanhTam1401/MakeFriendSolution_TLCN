using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MakeFriendSolution.TimerFeatures
{
    public class TimerManager
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;

        public DateTime TimeStarted { get; set; }

        public TimerManager(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
            TimeStarted = DateTime.Now;
        }

        public void Execute(object startInfo)
        {
            _action();
            if ((DateTime.Now - TimeStarted).Seconds > 60)
            {
                _timer.Dispose();
            }
        }
    }
}