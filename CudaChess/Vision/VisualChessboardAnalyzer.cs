using System;
using System.Runtime.InteropServices;

//using Emgu.CV;
//using Emgu.CV.Structure;
//using Emgu.CV.CvEnum;

namespace Chess.Vision
{
   /*
    class VisualChessboardAnalyzer
    {
        public static int    cannyLowThr  = 150;
        public static int    cannyHighThr = 220;
        public static int    houghThr     = 100;
        public static double houghRho     = 0.9;
        public static double houghTheta   = Math.PI / 180;
        public const string  WINDOW       = "Edge detection";

        public delegate void CallbackDelegate(int pos);

        [DllImport("opencv_highgui310d.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "cvCreateTrackbar")]
        public static extern int CvCreateTrackbar(
            [MarshalAs(UnmanagedType.LPStr)      ] String trackbar_name, 
            [MarshalAs(UnmanagedType.LPStr)      ] String window_name,
            [In, Out                             ] ref int value, 
                                                   int count, 
            [MarshalAs(UnmanagedType.FunctionPtr)] CallbackDelegate callbackPtr);

        public static IntPtr cap = IntPtr.Zero;

        public static void myTrackbarCallback(int pos) { }


        public static void start()
        {

            CvInvoke.NamedWindow(WINDOW);
            //CallbackDelegate callback = myTrackbarCallback;
            //CvCreateTrackbar("trackbar", WINDOW, ref cannyLowThr, 255, callback);
            Capture capture = new Capture();   //new Capture(); //create a camera captue
           
            while(true)
            {  //run this until application closed (close button click on image viewer)
                Mat frame = capture.QueryFrame(); //draw the image obtained from camera
                Mat canny = new Mat();
                CvInvoke.Canny(frame, canny, cannyLowThr, cannyHighThr);

                CvInvoke.Dilate(canny, canny, new Mat(), new System.Drawing.Point(-1, -1), 1, BorderType.Default, new MCvScalar(1));

                var lines = CvInvoke.HoughLinesP(canny, houghRho, houghTheta, houghThr, 30, 20); //TODO: fix parameters
                Console.WriteLine(lines.Length);

                foreach (LineSegment2D seg in lines)
                    CvInvoke.Line(frame, seg.P1, seg.P2, new MCvScalar(0, 0, 255), 2);

                var segSet = new SegmentSet(lines);
                var maxParallSet = segSet.GetMaxParallelSet();

                foreach (LineSegment2D seg in maxParallSet) {
                    CvInvoke.Line(frame, seg.P1, seg.P2, new MCvScalar(0, 255, 0), 2);
                }

                var orthSet = segSet.GetOrthogonalSet(maxParallSet[0]); //FIX: argumentOutOfRangeException
                foreach (LineSegment2D seg in orthSet) {
                    CvInvoke.Line(frame, seg.P1, seg.P2, new MCvScalar(255, 0, 0), 2);
                }

                CvInvoke.Imshow(WINDOW, frame);
                if (CvInvoke.WaitKey(20) == 27)
                    break;
              
            }
        }
    }
    */
}
