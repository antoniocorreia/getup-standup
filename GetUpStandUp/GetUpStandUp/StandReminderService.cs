using System.Drawing;
using System.ServiceProcess;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace GetUpStandUp
{
    public partial class StandReminderService : ServiceBase
    {
        private Timer timer;
        private NotifyIcon notifyIcon;

        public StandReminderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = 60 * 60 * 1000; // 1 hour in milliseconds
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            notifyIcon = new NotifyIcon
            {
                Icon = new Icon(SystemIcons.Information, 40, 40),
                Visible = true
            };
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            notifyIcon.ShowBalloonTip(1000, "Time to Move!", "Stand up and walk around for a few minutes.", ToolTipIcon.Info);
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Dispose();
            notifyIcon.Dispose();
        }
    }
}
