using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form

    {
        //PathUpdater add = new PathUpdater();       
        private string master_path;
        private FormWindowState _OldFormState;
        SynchronizationController controller = new SynchronizationController();
        Thread demoThread;

        delegate void ConstructionCallback();
        

        public SynchronizationController Controller 
        {
            set { controller = value; }
            get { return controller; }
        }
        

        public MainForm()
        {            
            InitializeComponent();
            controller.SynchronizationCompletedEventHandler += create_tree_unsafe;
            controller.SynchronizationCompletedEventHandler += MainForm_UpdateLastSyncCompletedTimeOnMainForm_OnSynchronizationCompletedEventHandler;
            controller.SynchronizationCompletedEventHandler += MainForm_UpdateSynchronizationStatusOnMainForm_OnSynchronizationCompletedEventHandler;
            controller.SynchronizationStartedEventHandler += MainForm_UpdateSynchronizationsStatusOnMainForm_OnSynchronizationStartedEventHandler;
            
        }

        delegate void MainForm_SynchronizationStatusComplettedValueLabelCallBack(object sender, SynchronizationController.SynchronizationEventArgs e);
        delegate void MainForm_SynchronizationStatusInProgressValueLabelCallBack(object sender, SynchronizationController.SynchronizationEventArgs e);
        delegate void MainForm_LastSyncCompletedTimeLabelCallBack(object sender, SynchronizationController.SynchronizationEventArgs e);


        private void MainForm_UpdateSynchronizationStatusOnMainForm_OnSynchronizationCompletedEventHandler(object sender, SynchronizationController.SynchronizationEventArgs e)
        {
            
            if (this.SynchronizationStatusValueLabel.InvokeRequired)
            {
                MainForm_SynchronizationStatusComplettedValueLabelCallBack d = new MainForm_SynchronizationStatusComplettedValueLabelCallBack(MainForm_UpdateSynchronizationStatusOnMainForm_OnSynchronizationCompletedEventHandler);
                this.Invoke(d, new object[] {sender, e });
            }
            else
            {
                SynchronizationStatusValueLabel.Text = "Completed";
            }
            
        }

        private void MainForm_UpdateSynchronizationsStatusOnMainForm_OnSynchronizationStartedEventHandler(object sender, SynchronizationController.SynchronizationEventArgs e)
        {
            
            if (this.SynchronizationStatusValueLabel.InvokeRequired)
            {
                MainForm_SynchronizationStatusInProgressValueLabelCallBack d = new MainForm_SynchronizationStatusInProgressValueLabelCallBack(MainForm_UpdateSynchronizationsStatusOnMainForm_OnSynchronizationStartedEventHandler);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                SynchronizationStatusValueLabel.Text = "In progress";
            } 
                       
        }

        private void MainForm_UpdateLastSyncCompletedTimeOnMainForm_OnSynchronizationCompletedEventHandler(object sender, SynchronizationController.SynchronizationEventArgs e)
        {
            
            if (this.LastSyncCompletedTimeLabel.InvokeRequired)
            {
                MainForm_LastSyncCompletedTimeLabelCallBack d = new MainForm_LastSyncCompletedTimeLabelCallBack(MainForm_UpdateLastSyncCompletedTimeOnMainForm_OnSynchronizationCompletedEventHandler);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                LastSyncCompletedTimeLabel.Text = Convert.ToString(e.Time);
            }

        }

        public void UpdateFilters(object sender, WindowsFormsApplication1.FileMaskFrom.FilterChangeEventArgs Args) 
        {
            Controller.Blacklist = Args.Black;
            controller.Whitelist = Args.White;
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            FileMaskFrom FileMask = new FileMaskFrom(Controller.Blacklist, controller.Whitelist);
            FileMask.FilterUpdateEventHendler += this.UpdateFilters;
            FileMask.ShowDialog();      
        }

        private void Schedule_Button_Click(object sender, EventArgs e)
        {
            
            ScheduleForm SchForm = new ScheduleForm(controller.Schedule);
            SchForm.ScheduleEventHendler += Schedule_handler;
            SchForm.ShowDialog();
        }

        public void Schedule_handler(object sender, WindowsFormsApplication1.ScheduleForm.ScheduleEventArgs Args) 
        {
            controller.Schedule = Args.timer;
        }
        
        

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {                
                if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
                {                    
                    _OldFormState = WindowState;                   
                    WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = false;
                   
                }
                else
                {                    
                    Show();                    
                    WindowState = _OldFormState;
                    this.ShowInTaskbar = true;
                }
            }
        }

        
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                Hide();

            }
        }

       

       

        private void Master_select_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog AddFolder = new FolderBrowserDialog();
            AddFolder.ShowDialog();
            master_path = AddFolder.SelectedPath;
            if (String.IsNullOrEmpty(master_path))
            {
                return;
            }           
        }

       

        private void ChangeMaster_Button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog AddFolder = new FolderBrowserDialog();
            AddFolder.ShowDialog();
            master_path = AddFolder.SelectedPath;
        }

        private void MasterContent_Button_Click(object sender, EventArgs e)
        {
            FolderContent FC = new FolderContent(master_path);
            FC.Show();
        }

        private void AddNode_Client_Click(object sender, EventArgs e)
        {
            controller.TreeConstrucktForForm += Construction;            
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            string path = fbd.SelectedPath;
            controller.AddPath(path);  

        }

        private void RemoveNode_Client_button_Click(object sender, EventArgs e)
        {
            controller.TreeConstrucktForForm += Construction;
                        
           try
            {
                controller.RemPath(treeView_Client.SelectedNode.Text);
                if (treeView_Client.SelectedNode.Parent == null)
                {
                    controller.TreeConstrucktForForm += Construction;
                    controller.RemPath(treeView_Client.SelectedNode.Text);
                }
                else
                {
                    Exception ex = new Exception();
                    throw ex;
                }

            }
           catch (Exception)
           {
               return;

           }
        }
                        

        private void Sync_Button_Click(object sender, EventArgs e)
        {
            controller.RunSyncronization();            
        }

        private void create_tree_unsafe(object sender,EventArgs e)
        {
            this.demoThread = new Thread(new ThreadStart(this.ThreadProcUnsafe));

            this.demoThread.Start();
        }

        private void ThreadProcUnsafe()
        {
            this.Construction();
        }

        

        private void Construction(object obj, EventArgs arg) 
        {
            
            treeView_Client.Nodes.Clear();
            List<string> path = controller.Path;
            TreeAddNode createTree = new TreeAddNode(path);
            createTree.create_tree(treeView_Client);
        }
        
        private void Construction()
        {
            if (this.treeView_Client.InvokeRequired)
            {
                ConstructionCallback d = new ConstructionCallback(Construction);
                this.Invoke(d, new object[] { });
            }
            else
            {
                treeView_Client.Nodes.Clear();
                List<string> path = controller.Path;
                TreeAddNode createTree = new TreeAddNode(path);
                createTree.create_tree(treeView_Client);
            }
        }
                
    }
}
