using System;
using System.IO;
using System.Threading;

namespace Task_4._1._1
{
    public class Watcher
    {
        private readonly FileSystemWatcher watcher;
        private bool Watching = true;
        readonly object _lockObject = new object();
        private readonly string _loggingFile;
        private readonly string _storageDir;
        private readonly string _backupsDir;
        private readonly INotificationChannel _notificationChannel;
        private readonly IInputChannel _inputChannel;
        public Watcher(string storageDir, string backupDir, string loggingDir, INotificationChannel notificationChannel, IInputChannel inputChannel)
        {
            _storageDir = storageDir;
            _backupsDir = backupDir;
            _notificationChannel = notificationChannel;
            _inputChannel = inputChannel;
            _loggingFile = loggingDir + "\\WatcherLog.txt";
            CreateFolder();
            ChangeMode();
            if (Watching)
            {
                _notificationChannel.Notify("Tracking changes...");
                watcher = new FileSystemWatcher(storageDir);

                watcher.Deleted += new FileSystemEventHandler(OnDelete);
                watcher.Renamed += new RenamedEventHandler(OnRenamed);
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Created += new FileSystemEventHandler(OnCreate);
                watcher.Error += OnError;

                watcher.Filter = "*.txt";
                watcher.IncludeSubdirectories = true;
            }
            else
            {
                Rollback();
            }
        }
        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (Watching)
            {
                Thread.Sleep(1000);
            }
        }

        private void Rollback()
        {
            var format = "yyyy.MM.dd_HH-mm-ss";
            _notificationChannel.Notify($"Enter backup's date and time, as \"{format}\" :");
            var dateTimeRollback = _inputChannel.GetStringInput();

            foreach (var s1 in Directory.GetFiles(_storageDir))
            {
                if (File.Exists(s1) & !File.Exists(_backupsDir + dateTimeRollback))
                {
                    File.Delete(s1);
                }
            }

            CopyDir(_backupsDir + dateTimeRollback, _storageDir);
            _notificationChannel.Notify("Rollback complete");
            Environment.Exit(0);
        }

        private void ChangeMode()
        {
            while (true)
            {
                _notificationChannel.Notify("Choose mode: \n1.Watching \n2.Roolback");
                var changeMode = _inputChannel.GetStringInput();

                if (changeMode == "1")
                {

                    _notificationChannel.Notify("Watching mode set");
                    break;
                }
                else if (changeMode == "2")
                {
                    Watching = false;
                    _notificationChannel.Notify("Rollback mode on");
                    break;
                }
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            _notificationChannel.Notify($"File: {e.FullPath} {e.ChangeType.ToString()}");
            var fileEvent = "changed";
            var filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
            CopyDir(_storageDir, _backupsDir);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            _notificationChannel.Notify($"File {e.OldFullPath}  {e.FullPath}");
            var fileEvent = "renamed in " + e.FullPath;
            var filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
            CreateFolder();

        }

        private void OnDelete(object source, FileSystemEventArgs e)
        {
            _notificationChannel.Notify($"File: {e.FullPath} removed");
            var fileEvent = "removed";
            var filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
            CreateFolder();
        }
        private void OnCreate(object source, FileSystemEventArgs e)
        {
            _notificationChannel.Notify($"File: {e.FullPath} created");
            var fileEvent = "created";
            var filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
            CreateFolder();
        }

        private void OnError(object source, ErrorEventArgs e)
        {
            PrintException(e.GetException());
        }

        private void PrintException(Exception ex)
        {
            if (ex != null)
            {
                _notificationChannel.Notify($"Message: {ex.Message}");
                _notificationChannel.Notify("Stacktrace:");
                _notificationChannel.Notify(ex.StackTrace);
                PrintException(ex.InnerException);
            }
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (_lockObject)
            {
                using (var writer = new StreamWriter(_loggingFile, true))
                {
                    writer.WriteLine($"{DateTime.Now:dd/MM/yyyy hh:mm:ss} file {filePath} was {fileEvent}");
                    writer.Flush();
                }
            }
        }

        private static void CopyDir(string inDir, string outDir)
        {
            Directory.CreateDirectory(outDir);
            foreach (var s1 in Directory.GetFiles(inDir))
            {
                var s2 = outDir + "\\" + Path.GetFileName(s1);
                File.Copy(s1, s2, true);
            }
            foreach (var s in Directory.GetDirectories(inDir))
            {
                CopyDir(s, outDir + "\\" + Path.GetFileName(s));
            }
        }

        private void CreateFolder()
        {
            var currentTime = DateTime.Now.ToString("yyyy.MM.dd_HH-mm-ss");
            var folder = Path.Combine(_backupsDir, currentTime);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            CopyDir(_storageDir, folder);
        }
    }
}
