using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CheckProcess
{
    public partial class Service1 : ServiceBase
    {
        System.Threading.Thread processThread;
        System.Timers.Timer timer;
        private Boolean Cancel;


        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Debug.WriteLine("Demaarage");

            timer = new System.Timers.Timer(5000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Tick);
            timer.Start();

            Cancel = false;
            // Get all processes running on the local computer.
            Process[] localAll = Process.GetProcesses();
        }
        protected override void OnContinue()
        {
            timer.Start();
        }

        protected override void OnPause()
        {
            timer.Stop();
        }

        protected override void OnStop()
        {
            if (processThread.ThreadState == System.Threading.ThreadState.Running)
            {
                Cancel = true;
                // Give thread a chance to stop
                processThread.Join(500);
                processThread.Abort();
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            processThread = new System.Threading.Thread(new System.Threading.ThreadStart(DoWork));
            processThread.Start();
        }

        private void DoWork()
        {
            try
            {
                while (!Cancel)
                {

                    if (Cancel) { return; }
                    // Do General Work

                    System.Threading.Thread.BeginCriticalRegion();
                    {
                        Debug.WriteLine("The product name is " );
                    }
                    System.Threading.Thread.EndCriticalRegion();
                }

            }
            catch (System.Threading.ThreadAbortException tae)
            {
                // Clean up correctly to leave program in stable state.
            }
        }
    }


}
