using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggerLibrary.Writers
{
    public class FileWriterQueue
    {
        private Queue<InQueue> _queue = new Queue<InQueue>();
        public FileWriterQueue()
        {
            Task.Run(() => {
                QueueProcessing();
            });
        }

        private void QueueProcessing()
        {
            while(true)
            {
                while(_queue.Count != 0)
                {
                    InQueue inQueue = _queue.Dequeue();
                    if(inQueue != null)
                    {
                        inQueue.stream.WriteLine(inQueue.text);
                        inQueue.stream.Flush();
                    }
                }

                Thread.Sleep(100);
            }
        }

        public void Add(InQueue inQueue)
        {
            _queue.Enqueue(inQueue);
        }
    }

    public class InQueue
    {
        public StreamWriter stream;
        public string text;
    }
}
