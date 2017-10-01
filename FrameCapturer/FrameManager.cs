using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FrameCapturer
{
    /// <summary>
    /// Single frame data structure
    /// </summary>
    public struct Frame
    {
        //public int frameNum;
        public int frameId;
        public string frameResource;
    }

    /// <summary>
    /// A Frame Manager is used to manage captured frames in games
    /// </summary>
    public class FrameManager
    {

        #region properties
        public int TargetLength { get; set; }
        public int StoredFrameCount
        {
            get
            {
                return frames.Count;
            }
        }
        private int targetLengthMin
        {
            get
            {
                return TargetLength * 2 / 3;
            }
        }
        private int targetLengthMax
        {
            get
            {
                return TargetLength * 4 / 3;
            }
        }
        #endregion

        #region private fields
        // storing local frames
        private LinkedList<Frame> frames;
        // the next frame need to be deleted
        private LinkedListNode<Frame> curDeleteFrame;
        // current interval between two continuous frames, 
        private int curInterval;
        // the last added frame's ordinal number
        private int lastFrameNumber;
        // the incoming frame'd ordinal number, aka total frames (not total frames recorded, but total frames passed), stars from 1
        private int curFrameNumber;
        #endregion

        // Cosntructor: Have to specify the targetLength
        private FrameManager() { }
        public FrameManager(int targetLength)
        {
            Debug.Assert(targetLength >= 1, "targetLength has to be larger than 1.");

            TargetLength = targetLength;
            frames = new LinkedList<Frame>();
            curDeleteFrame = null;
            curInterval = 1;
            lastFrameNumber = 0;
            curFrameNumber = 0;
        }

        /// <summary>
        /// Add a new Frame to FrameManager
        /// </summary>
        /// <param name="newFrame"> frame to store </param>
        public void AddFrame(Frame newFrame)
        {
            curFrameNumber++;
            // frame 
            if (frames.Count >= targetLengthMax)
            {
                //  delete some frames such we can add a new frame
                DeleteFrames();
            }

            // if comming frame won't be selected by GetFrame, then no need to record this frame
            if (curFrameNumber - lastFrameNumber < curInterval) return;

            frames.AddLast(newFrame);
            lastFrameNumber = curFrameNumber;
        }

        /// <summary>
        /// Get a list of frames
        /// </summary>
        /// <returns> return a list of frames </Frame> </returns>
        public IEnumerable<Frame> GetFrame()
        {
            // Later used
            Frame[] results = new Frame[frames.Count];
            int i = 0;
            foreach (var f in frames)
            {
                results[i++] = new Frame { frameId = f.frameId, frameResource = f.frameResource };
            }

            return results;
        }

        // Delete some frames and downgrade the stored gameplay
        private void DeleteFrames()
        {
            // delete node starting from the next of first frame
            curDeleteFrame = frames.First.Next;
            curInterval *= 2;

            // iteratively delete deprecated frames
            LinkedListNode<Frame> nextDeleteFrame = null;
            while (curDeleteFrame != null)
            {
                nextDeleteFrame = curDeleteFrame.Next == null ? null : curDeleteFrame.Next.Next;
                frames.Remove(curDeleteFrame);
                curDeleteFrame = nextDeleteFrame;
            }

            lastFrameNumber = (frames.Count - 1) * curInterval + 1;
        }



    }
}
