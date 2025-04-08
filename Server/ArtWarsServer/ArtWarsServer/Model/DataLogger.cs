using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;

namespace ArtWarsServer.Model
{

    class DataLogger
    {
        private static readonly Lazy<DataLogger> _instance = new Lazy<DataLogger>(()=> new DataLogger());
        public static DataLogger Instance => _instance.Value;

        bool writing;
        ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
        string LogFilePath = "file.log";

        private DataLogger()
        {
            writing = false;

        }

        public void log(string message) {
            //add the message to the queue
            logQueue.Enqueue($"[{DateTime.Now.ToString()}]" + message);
            saveToFile();


        }
        private void saveToFile()
        {
            //if writing is true, nothing happens
            if (!writing) {
                writing = true;
                //loop until the queue is empty
                while (!logQueue.IsEmpty)
                {
                    //save the string to a file
                    string ?message;
                    if (!logQueue.TryDequeue(out message) || message == null)
                    {
                        //skip if it fails to dequeue
                        continue;
                    }

                    //append the message to the log file
                    File.AppendAllText(LogFilePath, message + "\n");

                }
            
                //allow the file to be opened again
                writing = false;
            }
        }

    }

}
