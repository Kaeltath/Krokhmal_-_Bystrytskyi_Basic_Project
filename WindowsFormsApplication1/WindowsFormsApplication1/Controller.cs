using Microsoft.Synchronization.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace WindowsFormsApplication1
{
    public class SynchronizationController
    {
        private FileSyncScopeFilter filter = new FileSyncScopeFilter();
        System.Timers.Timer timer = new System.Timers.Timer();
        private bool IsSyncInProgress = false;
        public static string logLocation = "D:\\Folders_Synchronizator_Logs\\";
        public static string logPath = "D:\\Folders_Synchronizator_Logs\\Log.txt";
        PathUpdater Path_to_root = new PathUpdater();
        public List<String> Path;
        private int schedule = 1440;
        private string whitelist = "";
        private string blacklist = "";
        public int Schedule { set { schedule = value; Timer_Synch(); } get { return schedule; } }        
        public string Whitelist
        {
            set { whitelist = value; }
            get { return whitelist;}
        }
        public string Blacklist
        {
            set { blacklist = value; }
            get { return blacklist; }
        }

        public SynchronizationController() 
        {
            Timer_Synch();
        }

        Mutex mute = new Mutex();

        //Events
        public event EventHandler TreeConstrucktForForm;
        public event EventHandler<SyncFiltersEventArgs> SyncFiltersUpdated;
        public event EventHandler<SynchronizationEventArgs> SynchronizationStartedEventHandler;
        public event EventHandler<SynchronizationEventArgs> SynchronizationCompletedEventHandler;
        public event EventHandler<SynchronizationEventArgs> SynchronizationSkippedEventHandler;



        //Methods

        public void AddPath(string inp)
        {
            Path_to_root.TreeConstruckt += this.EventCreate;
            Path_to_root.Add_path(inp);
        }

        public void EventCreate(object o, EventArgs arg)
        {
            this.Path = Path_to_root.path;
            Path_to_root.TreeConstruckt += this.EventCreate;
            TreeConstrucktForForm(this, EventArgs.Empty);
        }

        public void RemPath(string rem)
        {
            Path_to_root.Remove_path(rem);
        }

        //Converts string from text box in UI to string array of dedicated filters, by ';' and ',' separators
        public string[] ParseSyncFiltersFromView(string syncFiltersFromView)
        {
            char[] delimitersForFiltersParsing = new char[] { ',', ';' };
            string[] filtersArray = syncFiltersFromView.Split(delimitersForFiltersParsing, StringSplitOptions.RemoveEmptyEntries);

            Log("Filter for syncronization has been sucessfully parsed");

            return filtersArray;
        }

        //Re-creates includeFiles and excludeFiles filters according to parsed from UI values
        public void UpdateSyncFilters(string[] excludeFiltersArray, string[] includeFilterArray)
        {           

            try
            {
                SyncFiltersUpdated += SyncronizationController_OnSyncFiltersUpdatedEventHandler;

                if (excludeFiltersArray != null && excludeFiltersArray.Length > 0)
                {
                    filter.FileNameExcludes.Clear();

                    Log("'Exclude' filter has been cleared");

                    for (int i = 0; i < excludeFiltersArray.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(excludeFiltersArray[i]))
                        {
                            filter.FileNameExcludes.Add(excludeFiltersArray[i]);
                        }
                    }

                    OnSyncFiltersUpdated("Exclude");
                }
                else
                {
                    Log("'Exclude' filter is empty. Skiping update of 'Exclude' filter");
                }

                if (includeFilterArray != null && includeFilterArray.Length > 0)
                {
                    filter.FileNameIncludes.Clear();

                    Log("'Include' filter has been cleared");

                    for (int i = 0; i < includeFilterArray.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(includeFilterArray[i]))
                        {
                            filter.FileNameIncludes.Add(includeFilterArray[i]);
                        }
                    }

                    OnSyncFiltersUpdated("Include");
                }
                else
                {
                    Log("'Include' filter is empty. Skiping update of 'Include' filter");
                }
            }
            catch (Exception e)
            {
                Log(String.Format(e.Message + " :  " + e.StackTrace));
            }
        }

        public void Timer_Synch() 
        {
            
            int min = 60000;            
            timer.Enabled = true;
            timer.Start();
            timer.AutoReset = true;
            timer.Interval = min* Schedule;
            timer.Elapsed += RunSyncronizationTimer;            
            
            
            

        }

        private void RunSyncronizationTimer(object sender, ElapsedEventArgs e)
        {
            mute.WaitOne();
            RunSyncronization();
            mute.ReleaseMutex();
            OnSynchronizationCompletedEventHandler("Completted");

        }

        //Runs sycronization of folders provided
        public void RunSyncronization()
        {
            SynchronizationStartedEventHandler += SyncronizationController_OnSynchronizationStartedEventHandler;
            SynchronizationCompletedEventHandler += SyncronizationController_OnSynchronizationCompletedEventHandler;
            SynchronizationSkippedEventHandler += SyncronizationController_OnSynchronizationSkippedEventHandler;

            if (!IsSyncInProgress)
            {
                OnSynchronizationStartedEventHandler("Started");

                IsSyncInProgress = true;

                try
                {
                    UpdateSyncFilters(ParseSyncFiltersFromView(Blacklist), ParseSyncFiltersFromView(Whitelist));

                    FoldersSynchronizator folderSyncronizator = new FoldersSynchronizator(Path, filter);

                    folderSyncronizator.AppliedChangeEventEventHandler += SyncronizationController_OnChangeAppliedChangeEventHandler;
                    folderSyncronizator.SkippedChangeEventEventHandler += SyncronizationController_OnChangeSkippedChangeEventHandler;
                    folderSyncronizator.StartingTwoFoldersSynchronizationOneWayEventHandler += SyncronizationController_OnStartingTwoFoldersSynchronizationOneWayEventHandler;

                    folderSyncronizator.DetectChangesInAllRootFolders();

                    folderSyncronizator.syncAllFoldersTwoWays();

                    OnSynchronizationCompletedEventHandler("Completted");

                    folderSyncronizator.AppliedChangeEventEventHandler -= SyncronizationController_OnChangeAppliedChangeEventHandler;
                    folderSyncronizator.SkippedChangeEventEventHandler -= SyncronizationController_OnChangeSkippedChangeEventHandler;
                    folderSyncronizator.StartingTwoFoldersSynchronizationOneWayEventHandler -= SyncronizationController_OnStartingTwoFoldersSynchronizationOneWayEventHandler;

                }
                catch (Exception e)
                {                    
                    Log(String.Format("{0} :  { 1}", e.Message.ToString(), e.StackTrace));
                }
                finally
                {
                    IsSyncInProgress = false;
                }
            }
            else
            {
                OnSynchronizationSkippedEventHandler("Skipped");
            }

            SynchronizationStartedEventHandler -= SyncronizationController_OnSynchronizationStartedEventHandler;
            SynchronizationCompletedEventHandler -= SyncronizationController_OnSynchronizationCompletedEventHandler;
            SynchronizationSkippedEventHandler -= SyncronizationController_OnSynchronizationSkippedEventHandler;

        }

        //Adds lines to log file
        public void Log(string logMessage)
        {
            if (Directory.Exists(logLocation))
            {
                using (StreamWriter streamWriter = File.AppendText(logPath))
                {
                    streamWriter.WriteLine("{0} {1} : {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), logMessage);
                    streamWriter.WriteLine("-------------------------------");
                }
            }
            else 
            {
                Directory.CreateDirectory(logLocation);
                using (StreamWriter streamWriter = File.AppendText(logPath))
                {
                    streamWriter.WriteLine("{0} {1} : {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), logMessage);
                    streamWriter.WriteLine("-------------------------------");
                }
            }
            //Timer_Synch();
        }



        //Events senders
        private void OnSyncFiltersUpdated(string filterType)
        {
            if (SyncFiltersUpdated != null)
            {
                var eventArgs = new SyncFiltersEventArgs(filterType);
                SyncFiltersUpdated(this, eventArgs);
            }
        }

        private void OnSynchronizationStartedEventHandler(string details)
        {
            if (SynchronizationStartedEventHandler != null)
            {
                var eventArgs = new SynchronizationEventArgs(details, DateTime.Now);
                SynchronizationStartedEventHandler(this, eventArgs);
            }
        }

        private void OnSynchronizationCompletedEventHandler(string details)
        {
            if (SynchronizationCompletedEventHandler != null)
            {
                var eventArgs = new SynchronizationEventArgs(details, DateTime.Now);
                SynchronizationCompletedEventHandler(this, eventArgs);
            }
        }

        private void OnSynchronizationSkippedEventHandler(string details)
        {
            if (SynchronizationSkippedEventHandler != null)
            {
                var eventArgs = new SynchronizationEventArgs(details, DateTime.Now);
                SynchronizationSkippedEventHandler(this, eventArgs);
            }
        }



        //Event Handlers
        //Adds 'Synchronization Started' message to log
        private void SyncronizationController_OnSynchronizationStartedEventHandler(object sender, SynchronizationEventArgs eventArgs)
        {
            Log("Synchronization session is started");
        }

        //Adds 'Synchronization Completted' message to log
        private void SyncronizationController_OnSynchronizationCompletedEventHandler(object sender, SynchronizationEventArgs eventArgs)
        {
            Log("Synchronization session is completted");
        }

        //Adds 'Filter Updated' message to log
        private void SyncronizationController_OnSyncFiltersUpdatedEventHandler(object sender, SyncFiltersEventArgs eventArgs)
        {
            Log(String.Format("'{0}' filter has been updated with parsed values", eventArgs.Message));
        }

        //Adds 'Synchronization Skipped' message to log
        private void SyncronizationController_OnSynchronizationSkippedEventHandler(object sender, SynchronizationEventArgs eventArgs)
        {
            Log("Synchronization session is skipped, since previous session is still in progress");
        }

        //Adds to log what folders are now synchronizing
        private void SyncronizationController_OnStartingTwoFoldersSynchronizationOneWayEventHandler(object sender, FoldersSynchronizator.TwoFoldersSynchroniationEventArgs eventArgs)
        {
            Log(String.Format("{0} changes from {1} to {2}", eventArgs.Direction, eventArgs.From, eventArgs.To));
        }

        //Adds applied change to log
        private void SyncronizationController_OnChangeAppliedChangeEventHandler(object sender, SynchronizationEventArgs eventArgs)
        {
            Log(eventArgs.Message);
        }

        //Adds skipped change to log
        private void SyncronizationController_OnChangeSkippedChangeEventHandler(object sender, SynchronizationEventArgs eventArgs)
        {
            Log(eventArgs.Message);
        }



        //Event Args classes
        public class SyncFiltersEventArgs : EventArgs
        {
            public string Message { private set; get; }
            public SyncFiltersEventArgs(string message)
            {
                Message = message;
            }
        }

        public class SynchronizationEventArgs : EventArgs
        {
            public string Message { private set; get; }
            public DateTime Time { private set; get; }
            public SynchronizationEventArgs(string message, DateTime time)
            {
                Message = message;
                Time = time;
            }
        }

    }
}
